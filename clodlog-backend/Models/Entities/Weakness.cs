using System.Text.Json.Serialization;
using clodlog_backend.Enums;

namespace clodlog_backend.Models.Entities;

public class Weakness
{
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PokemonType Type { get; set; }
    [JsonPropertyName("value")]
    public String Value { get; set; }
}