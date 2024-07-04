using clodlog_backend.Enums;

namespace clodlog_backend.Models.Criteria;

public class CardQueryCriteria
{
    public string? Name { get; set; }
    public SuperType? SuperType { get; set; }
    public List<SubType>? SubTypes { get; set; }
    public string? SetId { get; set; }
    public string? Series { get; set; }
    public Rarity? Rarity { get; set; }
    public string? Artist { get; set; }
    public List<PokemonType>? Types { get; set; }
    public int? MinHp { get; set; }
    public int? MaxHp { get; set; }
    public List<PokemonType>? Weaknesses { get; set; }
    public List<PokemonType>? Resistances { get; set; }
    public int? MinRetreatCost { get; set; }
    public int? MaxRetreatCost { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    public int? Limit { get; set; }
    public int? Skip { get; set; }
}
