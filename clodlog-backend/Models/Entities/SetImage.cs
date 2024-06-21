using System.Text.Json.Serialization;

namespace clodlog_backend.Models.Entities;

public class SetImage
{
    [JsonPropertyName("logo")]
    public String Logo;
    [JsonPropertyName("symbol")]
    public String Symbol;
}