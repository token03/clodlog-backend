using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Utils.Converters;

public class AttackListConverter : JsonConverter<List<Attack>>
{
    public override List<Attack> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var attacks = new List<Attack>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return attacks;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of object.");
            }

            attacks.Add(ReadAttack(ref reader));
        }

        throw new JsonException("Expected end of array.");
    }

    private Attack ReadAttack(ref Utf8JsonReader reader)
    {
        string name = null;
        string text = null;
        string damage = null;
        List<string> cost = null;
        int convertedEnergyCost = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new Attack
                {
                    Name = name,
                    Text = text,
                    Damage = damage,
                    Cost = cost,
                    ConvertedEnergyCost = convertedEnergyCost
                };
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
                    case "damage":
                        damage = reader.GetString();
                        break;
                    case "cost":
                        cost = ReadStringList(ref reader);
                        break;
                    case "convertedEnergyCost":
                        convertedEnergyCost = reader.GetInt32();
                        break;
                }
            }
        }

        throw new JsonException("Expected end of object.");
    }

    private List<string> ReadStringList(ref Utf8JsonReader reader)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var list = new List<string>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                return list;
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                list.Add(reader.GetString());
            }
        }

        throw new JsonException("Expected end of array.");
    }

    public override void Write(Utf8JsonWriter writer, List<Attack> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var attack in value)
        {
            writer.WriteStartObject();
            writer.WriteString("name", attack.Name);
            writer.WriteString("text", attack.Text);
            writer.WriteString("damage", attack.Damage);
            writer.WriteStartArray("cost");
            foreach (var cost in attack.Cost)
            {
                writer.WriteStringValue(cost);
            }
            writer.WriteEndArray();
            writer.WriteNumber("convertedEnergyCost", attack.ConvertedEnergyCost);
            writer.WriteEndObject();
        }

        writer.WriteEndArray();
    }
}

