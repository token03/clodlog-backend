using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class Legality
{
    [JsonPropertyName("standard")]
    public String Standard;
    [JsonPropertyName("expanded")]
    public String Expanded;
    [JsonPropertyName("unlimited")]
    public String Unlimited;
}