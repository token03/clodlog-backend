using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using clodlog_backend.Enums;
using clodlog_backend.Models.Entities;
using clodlog_backend.Utils;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Models;

public class Card
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("supertype")]
    [JsonConverter(typeof(EnumDescriptionConverter<SuperType>))]
    public SuperType SuperType { get; set; }

    [JsonPropertyName("subtypes")]
    [JsonConverter(typeof(EnumDescriptionListConverter<SubType>))]
    public List<SubType> SubTypes { get; set; }

    [JsonPropertyName("rules")]
    public List<String> Rules { get; set; }
    
    [JsonPropertyName("set")]
    public Set Set { get; set; }
    
    [JsonPropertyName("setId")]
    public string SetId { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonPropertyName("artist")]
    public string Artist { get; set; }

    [JsonPropertyName("rarity")]
    [JsonConverter(typeof(EnumDescriptionConverter<Rarity>))]
    public Rarity Rarity { get; set; }

    [JsonPropertyName("flavorText")]
    public string FlavorText { get; set; }

    [JsonPropertyName("legalities")]
    [JsonConverter(typeof(LegalityConverter))]
    public Legality Legalities { get; set; }

    [JsonPropertyName("regulationMark")]
    public string? RegulationMark { get; set; }

    [JsonPropertyName("images")]
    [JsonConverter(typeof(CardImageConverter))]
    public CardImage Image { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("hp")]
    public string? Hp { get; set; }

    [JsonPropertyName("types")]
    [JsonConverter(typeof(EnumDescriptionListConverter<PokemonType>))]
    public List<PokemonType> Types { get; set; }

    [JsonPropertyName("evolvesFrom")]
    public string? EvolvesFrom { get; set; }

    [JsonPropertyName("evolvesTo")]
    public List<String> EvolvesTo { get; set; }

    [JsonPropertyName("ancientTrait")]
    [JsonConverter(typeof(AncientTraitConverter))]
    public AncientTrait AncientTrait { get; set; }


    [JsonPropertyName("abilities")]
    [JsonConverter(typeof(AbilityListConverter))]
    public List<Ability> Abilities { get; set; }

    [JsonPropertyName("attacks")]
    [JsonConverter(typeof(AttackListConverter))]
    public List<Attack> Attacks { get; set; }

    [JsonPropertyName("weaknesses")]
    [JsonConverter(typeof(WeaknessListConverter))]
    public List<Weakness> Weaknesses { get; set; }

    [JsonPropertyName("resistances")]
    [JsonConverter(typeof(ResistanceListConverter))]
    public List<Resistance> Resistances { get; set; }


    [JsonPropertyName("retreatCost")]
    public List<string> RetreatCost { get; set; }

    [JsonPropertyName("convertedRetreatCost")]
    public int ConvertedRetreatCost { get; set; }

    [JsonPropertyName("nationalPokedexNumbers")]
    public List<int> NationalPokedexNumbers { get; set; }
}
