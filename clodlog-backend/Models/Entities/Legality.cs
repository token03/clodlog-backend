using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Legality
{
    [JsonPropertyName("standard")]
    public String Standard { get; set; }
    [JsonPropertyName("expanded")]
    public String Expanded { get; set; }
    [JsonPropertyName("unlimited")]
    public String Unlimited { get; set; }
}