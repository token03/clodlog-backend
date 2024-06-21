using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Utils.Converters;

public class WeaknessListConverter : JsonConverter<List<Weakness>>
{
    public override List<Weakness> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var weaknesses = new List<Weakness>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return weaknesses;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            weaknesses.Add(ReadWeakness(ref reader));
        }

        throw new JsonException("Expected end of array.");
    }

    private Weakness ReadWeakness(ref Utf8JsonReader reader)
    {
        string type = null;
        string value = null;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new Weakness
                {
                    Type = type,
                    Value = value
                };
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "type":
                        type = reader.GetString();
                        break;
                    case "value":
                        value = reader.GetString();
                        break;
                }
            }
        }

        throw new JsonException("Expected end of object.");
    }

    public override void Write(Utf8JsonWriter writer, List<Weakness> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var weakness in value)
        {
            writer.WriteStartObject();
            writer.WriteString("type", weakness.Type);
            writer.WriteString("value", weakness.Value);
            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }
}
