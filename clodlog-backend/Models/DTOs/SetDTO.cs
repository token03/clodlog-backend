using System.Text.Json.Serialization;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Models.DTOs;

public class SetDTO
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

    public SetDTO(Set set)
    {
        Id = set.Id;
        Name = set.Name;
        Series = set.Series;
        PrintedTotal = set.PrintedTotal;
        Total = set.Total;
        ReleaseDate = set.ReleaseDate;
        UpdatedAt = set.UpdatedAt;
        Images = set.Images;
        Legality = set.Legality;
        PctgoCode = set.PctgoCode;
    }
}
