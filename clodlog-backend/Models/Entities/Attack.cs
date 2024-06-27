using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Attack
{
    [JsonPropertyName("cost")]
    public List<String> Cost { get; set; }
    [JsonPropertyName("name")]
    public String Name { get; set; }
    [JsonPropertyName("text")]
    public String Text { get; set; }
    [JsonPropertyName("damage")]
    public String Damage { get; set; }
    [JsonPropertyName("convertedEnergyCost")]
    public int ConvertedEnergyCost { get; set; }
}