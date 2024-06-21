using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Ability
{
    [JsonPropertyName("name")]
    public String Name;
    [JsonPropertyName("text")]
    public String Text;
    [JsonPropertyName("type")]
    public String Type;
}