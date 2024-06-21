using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class AncientTrait
{
    [JsonPropertyName("name")]
    public String Name;
    [JsonPropertyName("text")]
    public String Text;
}