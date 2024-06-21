using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Utils.Converters;

public class AbilityListConverter : JsonConverter<List<Ability>>
{
    public override List<Ability> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var abilities = new List<Ability>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return abilities;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            string name = null;
            string text = null;
            string type = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "name":
                            name = reader.GetString();
                            break;
                        case "text":
                            text = reader.GetString();
                            break;
                        case "type":
                            type = reader.GetString();
                            break;
                    }
                }
            }

            if (name != null && text != null && type != null)
            {
                abilities.Add(new Ability
                {
                    Name = name,
                    Text = text,
                    Type = type
                });
            }
        }

        throw new JsonException("Expected end of array.");
    }

    public override void Write(Utf8JsonWriter writer, List<Ability> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var ability in value)
        {
            writer.WriteStartObject();
            writer.WriteString("name", ability.Name);
            writer.WriteString("text", ability.Text);
            writer.WriteString("type", ability.Type);
            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }
}
