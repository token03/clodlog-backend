using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Utils.Converters;

public class ResistanceListConverter : JsonConverter<List<Resistance>>
{
    public override List<Resistance> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var resistances = new List<Resistance>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return resistances;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            resistances.Add(ReadResistance(ref reader));
        }

        throw new JsonException("Expected end of array.");
    }

    private Resistance ReadResistance(ref Utf8JsonReader reader)
    {
        string type = null;
        string value = null;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new Resistance
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

    public override void Write(Utf8JsonWriter writer, List<Resistance> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var resistance in value)
        {
            writer.WriteStartObject();
            writer.WriteString("type", resistance.Type);
            writer.WriteString("value", resistance.Value);
            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }
}

