using System.Text.Json;
using clodlog_backend.Models;
using clodlog_backend.Models.DTOs;

namespace clodlog_backend.Services;

public class SetService
{
    private readonly string _dataPath;
    private List<Set> _sets;
    private CardService _cardService;

    public SetService(string dataPath)
    {
        _dataPath = dataPath;
        _sets = new List<Set>();
        LoadSets();
    }
    
    public void SetCardService(CardService cardService)
    {
        _cardService = cardService;
    }

    private void LoadSets()
    {
        Console.WriteLine("Loading sets...");
        foreach (var file in Directory.GetFiles(_dataPath, "*.json"))
        {
            Console.WriteLine($"Reading file: {file}");
            var json = File.ReadAllText(file);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var sets = JsonSerializer.Deserialize<List<Set>>(json, options);
            if (sets != null)
            {
                _sets.AddRange(sets);
                Console.WriteLine($"Added {sets.Count} sets from file: {file}");
            }
        }

        Console.WriteLine($"Total sets loaded: {_sets.Count}");
    }

    public async Task<IEnumerable<Set>> GetAllSetsAsync()
    {
        var result = await Task.FromResult(_sets);
        return result;
    }
    
    public async Task<Set?> GetSetByIdAsync(string id)
    {
        var set = await Task.FromResult(_sets.FirstOrDefault(s => s.Id == id));
        if (set != null)
        {
            IEnumerable<Card>? cards = await _cardService.GetCardsBySetIdAsync(id);
            if (cards != null)
            {
                List<CardDTO> cardDtOs = new List<CardDTO>();
                foreach (var card in cards)
                {
                    CardDTO cardDto = new CardDTO(card);
                    cardDtOs.Add(cardDto);
                }
                set.Cards = cardDtOs;
            }
        }

        return set;
    }
    
    public async Task<Dictionary<string, List<Set>>> GetSeriesSetMapAsync()
    {
        var result = await Task.FromResult(_sets.GroupBy(s => s.Series).ToDictionary(g => g.Key, g => g.ToList()));
        return result;
    }
}
