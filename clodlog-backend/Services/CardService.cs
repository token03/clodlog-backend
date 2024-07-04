using System.Text.Json;
using clodlog_backend.Enums;
using clodlog_backend.Models;
using clodlog_backend.Models.Criteria;
using clodlog_backend.Models.DTOs;
using clodlog_backend.Models.Entities;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Services;

public class CardService
{
    private readonly string _cardDataPath;
    private readonly string _priceDataPath;
    private readonly object _pricesLock = new object();
    private readonly List<Card> _cards;
    private readonly Dictionary<string, Dictionary<DateOnly, Dictionary<string, PriceDetails>>> _prices;
    private readonly SetService _setService;
    
    public CardService(string cardDataPath, string priceDataPath, SetService setService)
    {
        _cardDataPath = cardDataPath;
        _priceDataPath = priceDataPath;
        _setService = setService;
        _cards = new List<Card>();
        _prices = new Dictionary<string, Dictionary<DateOnly, Dictionary<string, PriceDetails>>>();

        Initialize();
    }

    private void Initialize()
    {
        LoadPrices();
        LoadCards();
    }

    public void LoadCards()
    {
        Console.WriteLine("Loading cards...");
        foreach (var file in Directory.GetFiles(_cardDataPath, "*.json"))
        {
            ProcessCardFile(file);
        }
        Console.WriteLine($"Total cards loaded: {_cards.Count}");
    }

    private void ProcessCardFile(string file)
    {
        Console.WriteLine($"Reading file: {file}");
        var json = File.ReadAllText(file);
        var cards = JsonSerializer.Deserialize<List<Card>>(json, GetJsonSerializerOptions());

        if (cards == null) return;

        string setId = Path.GetFileNameWithoutExtension(file);
        foreach (var card in cards)
        {
            card.SetId = setId;
            UpdateCardPrice(card);
        }

        _cards.AddRange(cards);
        Console.WriteLine($"Added {cards.Count} cards from file: {file}");
    }

    private void UpdateCardPrice(Card card)
    {
        Dictionary<DateOnly, Dictionary<string, PriceDetails>>? cardPrices;
        lock (_pricesLock)
        {
            if (!_prices.TryGetValue(card.Id, out cardPrices) || cardPrices.Count == 0) return;
        }

        // We can safely work with cardPrices outside the lock as it's a copy
        DateOnly latestDate = cardPrices.Keys.Max();
        if (cardPrices.TryGetValue(latestDate, out var latestPrice))
        {
            card.Prices = latestPrice;
        }
    }

    private void LoadPrices()
    {
        Console.WriteLine("Loading prices...");
        Parallel.ForEach(Directory.GetDirectories(_priceDataPath), ProcessSetFolder);
    }

    private void ProcessSetFolder(string setFolder)
    {
        string setId = Path.GetFileName(setFolder);
        Console.WriteLine($"Processing set: {setId}");
        Parallel.ForEach(Directory.GetFiles(setFolder, "*.json"), ProcessPriceFile);
    }

    private void ProcessPriceFile(string file)
    {
        var json = File.ReadAllText(file);
        var priceData = JsonSerializer.Deserialize<PriceData>(json, GetJsonSerializerOptions());
        if (priceData == null) return;

        var cardPrices = new Dictionary<DateOnly, Dictionary<string, PriceDetails>>();
        if (priceData.PriceHistory != null)
        {
            foreach (var (dateString, priceDetails) in priceData.PriceHistory)
            {
                if (DateOnly.TryParse(dateString, out DateOnly date))
                {
                    cardPrices[date] = priceDetails;
                }
            }
        }

        lock (_pricesLock)
        {
            _prices[priceData.Id] = cardPrices;
        }
    }
    
    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new EnumDescriptionConverter<SuperType>(),
                new EnumDescriptionConverter<Rarity>(),
                new EnumDescriptionListConverter<SubType>(),
                new EnumDescriptionListConverter<PokemonType>(),
            }
        };
    }
 
    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        var result = await Task.FromResult(_cards);
        return result;
    }

    public async Task<Card> GetCardByIdAsync(string id)
    {
        Card? card = await Task.FromResult(_cards.FirstOrDefault(c => c.Id == id));
        if (card != null)
        {
            Set? set = await _setService.GetSetByIdAsync(card.SetId);
            if (set != null)
                card.Set = new SetDTO(set);
        }
        return card;
    }

    public async Task<IEnumerable<Card>?> GetCardsBySetIdAsync(string setId)
    {
        return await Task.FromResult(_cards.Where(c => c.SetId == setId));
    }
    
    public async Task<IEnumerable<Card>> GetCardsByNameAsync(string name)
    {
        return await Task.FromResult(_cards.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }
    
    public async Task<IEnumerable<Card>> GetCardsBySupertypeAsync(SuperType supertype)
    {
        return await Task.FromResult(_cards.Where(c => c.SuperType == supertype));
    }

    public async Task<IEnumerable<Card>> GetCardsByRarityAsync(Rarity rarity)
    {
        return await Task.FromResult(_cards.Where(c => c.Rarity == rarity));
    }
    
    public async Task<IEnumerable<Card>> GetCardsByCriteriaAsync(CardQueryCriteria criteria)
    {
        var query = _cards.AsQueryable();

        if (!string.IsNullOrEmpty(criteria.Name))
            query = query.Where(c => c.Name.Contains(criteria.Name, StringComparison.OrdinalIgnoreCase));

        if (criteria.SuperType.HasValue)
            query = query.Where(c => c.SuperType == criteria.SuperType);

        if (criteria.SubTypes != null && criteria.SubTypes.Any())
            query = query.Where(c => c.SubTypes.Any(st => criteria.SubTypes.Contains(st)));

        if (!string.IsNullOrEmpty(criteria.SetId))
            query = query.Where(c => c.SetId == criteria.SetId);

        if (criteria.Rarity.HasValue)
            query = query.Where(c => c.Rarity == criteria.Rarity);

        if (!string.IsNullOrEmpty(criteria.Artist))
            query = query.Where(c => c.Artist.Contains(criteria.Artist, StringComparison.OrdinalIgnoreCase));

        if (criteria.Types != null && criteria.Types.Any())
            query = query.Where(c => c.Types != null && c.Types.Any(t => criteria.Types.Contains(t)));

        if (criteria.MinHp.HasValue)
            query = query.Where(c => !string.IsNullOrEmpty(c.Hp) && int.Parse(c.Hp) >= criteria.MinHp.Value);

        if (criteria.MaxHp.HasValue)
            query = query.Where(c => !string.IsNullOrEmpty(c.Hp) && int.Parse(c.Hp) <= criteria.MaxHp.Value);

        if (criteria.Weaknesses != null && criteria.Weaknesses.Any())
            query = query.Where(c => c.Weaknesses != null && c.Weaknesses.Any(w => criteria.Weaknesses.Contains(w.Type)));

        if (criteria.Resistances != null && criteria.Resistances.Any())
            query = query.Where(c => c.Resistances != null && c.Resistances.Any(r => criteria.Resistances.Contains(r.Type)));

        if (criteria.MinRetreatCost.HasValue)
            query = query.Where(c => c.ConvertedRetreatCost >= criteria.MinRetreatCost.Value);

        if (criteria.MaxRetreatCost.HasValue)
            query = query.Where(c => c.ConvertedRetreatCost <= criteria.MaxRetreatCost.Value);

        // Sorting
        if (!string.IsNullOrEmpty(criteria.SortBy))
        {
            query = criteria.SortBy.ToLower() switch
            {
                "name" => criteria.SortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                "hp" => criteria.SortDescending ? query.OrderByDescending(c => int.Parse(c.Hp ?? "0")) : query.OrderBy(c => int.Parse(c.Hp ?? "0")),
                "number" => criteria.SortDescending ? query.OrderByDescending(c => c.Number) : query.OrderBy(c => c.Number),
                _ => query
            };
        }
        
        if (!string.IsNullOrEmpty(criteria.Series))
        {
            var setsInSeries = await _setService.GetSetsBySeriesAsync(criteria.Series);
            var setIdsInSeries = setsInSeries.Select(s => s.Id).ToList();
            query = query.Where(c => setIdsInSeries.Contains(c.SetId));
        }

        // Pagination
        if (criteria.Skip.HasValue)
            query = query.Skip(criteria.Skip.Value);

        if (criteria.Limit.HasValue)
            query = query.Take(criteria.Limit.Value);

        return await Task.FromResult(query.ToList());
    }
}
