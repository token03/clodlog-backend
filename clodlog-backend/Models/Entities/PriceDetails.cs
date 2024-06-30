
using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class PriceDetails
{
    [JsonPropertyName("ungraded")]
    public string Ungraded { get; set; }

    [JsonPropertyName("psa10")]
    public string Psa10 { get; set; }

    [JsonPropertyName("grade9")]
    public string Grade9 { get; set; }
}
