using System.Text.Json;
using System.Text.Json.Serialization;
using clodlog_backend.Enums;
using clodlog_backend.Models;
using clodlog_backend.Models.Entities;
using clodlog_backend.Utils;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Services;

public class CardService
{
    private readonly string _dataPath;
    private List<Card> _cards;

    public CardService(string dataPath)
    {
        _dataPath = dataPath;
        _cards = new List<Card>();
        LoadCards();
    }

    private void LoadCards()
    {
        Console.WriteLine("Loading cards...");
        bool checkedOnce = false;
        foreach (var file in Directory.GetFiles(_dataPath, "*.json"))
        {
            if (checkedOnce)
            {
                break;
            }
            Console.WriteLine($"Reading file: {file}");
            var json = File.ReadAllText(file);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = 
                { 
                    new EnumDescriptionConverter<SuperType>(),
                    new EnumDescriptionConverter<Rarity>(),
                    
                    new EnumDescriptionListConverter<SubType>(),
                    new EnumDescriptionListConverter<PokemonType>(),
                    
                    new LegalityConverter(),
                    new AncientTraitConverter(),
                    new CardImageConverter(),
                    
                    new AbilityListConverter(),
                    new AttackListConverter(),
                    new WeaknessListConverter(),
                    new ResistanceListConverter(),
                }
            };
            var cards = JsonSerializer.Deserialize<List<Card>>(json, options);
        if (cards != null)
        {
            string setId = Path.GetFileNameWithoutExtension(file);
            foreach (var card in cards)
            {
                card.SetId = setId;
            }
            _cards.AddRange(cards);
            Console.WriteLine($"Added {cards.Count} cards from file: {file}");
        }


            checkedOnce = true;
        }

        Console.WriteLine($"Total cards loaded: {_cards.Count}");
    }

    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        var result = await Task.FromResult(_cards);
        return result;
    }

    public async Task<Card> GetCardByIdAsync(string id)
    {
        return await Task.FromResult(_cards.FirstOrDefault(c => c.Id == id));
    }

    public async Task<IEnumerable<Card>> GetCardsBySetAsync(string setId)
    {
        return await Task.FromResult(_cards.Where(c => c.Set.Id == setId));
    }

    public async Task<IEnumerable<Card>> GetCardsByNameAsync(string name)
    {
        return await Task.FromResult(_cards.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }
    
    public async Task<IEnumerable<Card>> GetCardsBySupertypeAsync(SuperType supertype)
    {
        return await Task.FromResult(_cards.Where(c => c.SuperType == supertype));
    }

    // public async Task<IEnumerable<Card>> GetCardsBySubtypeAsync(SubType subtype)
    // {
    //     return await Task.FromResult(_cards.Where(c => c.SubTypes.Contains(subtype)));
    // }

    public async Task<IEnumerable<Card>> GetCardsByRarityAsync(Rarity rarity)
    {
        return await Task.FromResult(_cards.Where(c => c.Rarity == rarity));
    }
}
