using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Enums;

namespace clodlog_backend.Utils.Converters;

public class EnumDescriptionConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string jsonValue = reader.GetString();
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == jsonValue)
                {
                    return (T)field.GetValue(null);
                }
            }
            else if (field.Name == jsonValue)
            {
                return (T)field.GetValue(null);
            }
        }
        throw new JsonException($"Unable to convert '{jsonValue}' to enum {typeof(T)}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetDescription());
    }
}