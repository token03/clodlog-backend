using System.Text.Json.Serialization;
using clodlog_backend.Enums;
using clodlog_backend.Models.Entities;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Models.DTOs;

public class CardDTO
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
    public Legality Legalities { get; set; }

    [JsonPropertyName("regulationMark")]
    public string? RegulationMark { get; set; }

    [JsonPropertyName("images")]
    public CardImage Image { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("hp")]
    public string? Hp { get; set; }

    [JsonPropertyName("types")]
    [JsonConverter(typeof(EnumDescriptionListConverter<PokemonType>))]
    public List<PokemonType> Types { get; set; }

    // [JsonPropertyName("evolvesFrom")]
    // public string? EvolvesFrom { get; set; }
    //
    // [JsonPropertyName("evolvesTo")]
    // public List<String> EvolvesTo { get; set; }
    
    [JsonPropertyName("ancientTrait")]
    public AncientTrait AncientTrait { get; set; }

    // [JsonPropertyName("abilities")]
    // public List<Ability> Abilities { get; set; }
    //
    // [JsonPropertyName("attacks")]
    // public List<Attack> Attacks { get; set; }

    [JsonPropertyName("weaknesses")]
    public List<Weakness> Weaknesses { get; set; }

    [JsonPropertyName("resistances")]
    public List<Resistance> Resistances { get; set; }


    [JsonPropertyName("retreatCost")]
    public List<string> RetreatCost { get; set; }

    [JsonPropertyName("convertedRetreatCost")]
    public int ConvertedRetreatCost { get; set; }

    [JsonPropertyName("nationalPokedexNumbers")]
    public List<int> NationalPokedexNumbers { get; set; }
    
    [JsonPropertyName("prices")]
    public Dictionary<string, PriceDetails> Prices { get; set; }
    
    public CardDTO (Card card)
    {
        Id = card.Id;
        Name = card.Name;
        SuperType = card.SuperType;
        SubTypes = card.SubTypes;
        Rules = card.Rules;
        SetId = card.SetId;
        Number = card.Number;
        Artist = card.Artist;
        Rarity = card.Rarity;
        FlavorText = card.FlavorText;
        Legalities = card.Legalities;
        RegulationMark = card.RegulationMark;
        Image = card.Image;
        Level = card.Level;
        Hp = card.Hp;
        Types = card.Types;
        // EvolvesFrom = card.EvolvesFrom;
        // EvolvesTo = card.EvolvesTo;
        AncientTrait = card.AncientTrait;
        // Abilities = card.Abilities;
        // Attacks = card.Attacks;
        Weaknesses = card.Weaknesses;
        Resistances = card.Resistances;
        RetreatCost = card.RetreatCost;
        ConvertedRetreatCost = card.ConvertedRetreatCost;
        NationalPokedexNumbers = card.NationalPokedexNumbers;
        Prices = card.Prices;
    }
}