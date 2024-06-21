using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Attack
{
    [JsonPropertyName("cost")]
    public List<String> Cost;
    [JsonPropertyName("name")]
    public String Name;
    [JsonPropertyName("text")]
    public String Text;
    [JsonPropertyName("damage")]
    public String Damage;
    [JsonPropertyName("convertedEnergyCost")]
    public int ConvertedEnergyCost;
}