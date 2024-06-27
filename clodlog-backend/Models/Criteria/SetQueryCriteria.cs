using clodlog_backend.Enums;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Models.Criteria;

public class SetQueryCriteria
{
    public string? Name { get; set; }
    public string? Series { get; set; }
    public int? PrintedTotal { get; set; }
    public int? Total { get; set; }
    public string? ReleaseDate { get; set; }
    public string? UpdatedAt { get; set; }
    public Legality? Legality { get; set; }
    public string? PtcgoCode { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    public int? Limit { get; set; }
    public int? Skip { get; set; }
}