using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class SetImage
{
    [JsonPropertyName("logo")]
    public String Logo { get; set; }
    [JsonPropertyName("symbol")]
    public String Symbol { get; set; }
}