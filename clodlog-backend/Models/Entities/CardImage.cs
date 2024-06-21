using System.Text.Json.Serialization;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Models.Entities;

public class CardImage
{   
    [JsonPropertyName("small")]
    public String Small;
    [JsonPropertyName("large")]
    public String Large;
}