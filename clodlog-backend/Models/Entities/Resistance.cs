using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Resistance
{
    [JsonPropertyName("type")]
    public String Type;
    [JsonPropertyName("value")]
    public String Value;
}