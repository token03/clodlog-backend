using System.ComponentModel.DataAnnotations;
using clodlog_backend.Models.Entities;

namespace clodlog_backend.Models;

public class Set
{
    public string Id { get; }
    public string Name { get; }
    public string Series { get; }
    public int PrintedTotal { get; }
    public int Total { get; }
    public string ReleaseDate { get; }
    public string UpdatedAt { get; }
    public SetImage Images { get; }

    public Legality? Legality { get; }
    public string? PctgoCode { get; }
}