using System.Text.Json;
using System.Text.Json.Serialization;

namespace clodlog_backend.Utils.Converters;

public class EnumDescriptionListConverter<T> : JsonConverter<List<T>> where T : struct, Enum
{
    private readonly EnumDescriptionConverter<T> _enumConverter = new EnumDescriptionConverter<T>();

    public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var list = new List<T>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return list;
            }

            list.Add(_enumConverter.Read(ref reader, typeof(T), options));
        }

        throw new JsonException("Expected end of array.");
    }

    public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var item in value)
        {
            _enumConverter.Write(writer, item, options);
        }

        writer.WriteEndArray();
    }
}
