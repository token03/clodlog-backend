using System.Text.Json.Serialization;
using clodlog_backend.Models.DTOs;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Models;

public class Set
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set;  }
    
    [JsonPropertyName("series")]
    public string Series { get; set;  }
    
    [JsonPropertyName("printedTotal")]
    public int PrintedTotal { get; set; }
    
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    [JsonPropertyName("releaseDate")]
    public string ReleaseDate { get; set; }
    
    [JsonPropertyName("updatedAt")]
    public string UpdatedAt { get; set;  }
    
    [JsonPropertyName("images")]
    public SetImage Images { get; set;  }

    [JsonPropertyName("legalities")]
    public Legality? Legality { get; set;  }
    
    [JsonPropertyName("ptcgoCode")]
    public string? PctgoCode { get; set;  }
    
    [JsonPropertyName("cards")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<CardDTO>? Cards { get; set;  }
}