using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Ability
{
    [JsonPropertyName("name")]
    public String Name { get; set; }
    [JsonPropertyName("text")]
    public String Text { get; set; }
    [JsonPropertyName("type")]
    public String Type { get; set; }
}