using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace clodlog_backend.Utils.Converters;

public delegate void PropertyReader<T>(ref Utf8JsonReader reader, T instance);

// TODO: Create a base list converter
public abstract class BaseConverter<T> : JsonConverter<T> where T : new()
{
    protected abstract Dictionary<string, PropertyReader<T>> PropertyMap { get; }
    protected abstract void WriteProperties(Utf8JsonWriter writer, T value);

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected start of object.");
        }

        var instance = new T();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return instance;
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();

                if (PropertyMap.ContainsKey(propertyName))
                {
                    PropertyMap[propertyName](ref reader, instance);
                }
                else
                {
                    reader.Skip();
                }
            }
        }

        throw new JsonException("Expected end of object.");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        WriteProperties(writer, value);
        writer.WriteEndObject();
    }
}


