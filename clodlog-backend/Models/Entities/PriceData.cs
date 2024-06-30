using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class PriceData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("priceChartingIds")]
    public Dictionary<string, string> PriceChartingIds { get; set; }

    [JsonPropertyName("priceHistory")]
    public Dictionary<string, Dictionary<string, PriceDetails>>? PriceHistory { get; set; }
}
