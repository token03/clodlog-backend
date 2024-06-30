using System.Text.Json;
using clodlog_backend.Enums;
using clodlog_backend.Models.Entities;
using clodlog_backend.Utils.Converters;

namespace clodlog_backend.Repositories;

public class PriceRepository
{
    private readonly string _priceDataPath;
    private readonly Dictionary<string, Dictionary<DateOnly, Dictionary<string, PriceDetails>>> _prices;

    public PriceRepository(string priceDataPath)
    {
        _priceDataPath = priceDataPath;
        _prices = new Dictionary<string, Dictionary<DateOnly, Dictionary<string, PriceDetails>>>();
        LoadPrices();
    }

    private void LoadPrices()
    {
        Console.WriteLine("Loading prices...");
        foreach (var setFolder in Directory.GetDirectories(_priceDataPath))
        {
            ProcessSetFolder(setFolder);
        }
    }

    private void ProcessSetFolder(string setFolder)
    {
        string setId = Path.GetFileName(setFolder);
        Console.WriteLine($"Processing set: {setId}");

        foreach (var file in Directory.GetFiles(setFolder, "*.json"))
        {
            ProcessPriceFile(file);
        }
    }

    private void ProcessPriceFile(string file)
    {
        Console.WriteLine($"Reading file: {file}");
        var json = File.ReadAllText(file);
        var priceData = JsonSerializer.Deserialize<PriceData>(json, GetJsonSerializerOptions());

        if (priceData == null) return;

        _prices.TryAdd(priceData.Id, new Dictionary<DateOnly, Dictionary<string, PriceDetails>>());

        priceData.PriceHistory ??= new Dictionary<string, Dictionary<string, PriceDetails>>();
        foreach (var (dateString, priceDetails) in priceData.PriceHistory)
        {
            DateOnly date = DateOnly.Parse(dateString);
            _prices[priceData.Id][date] = priceDetails;
        }

        Console.WriteLine($"Added prices for card: {priceData.Id}");
    }


    public async Task<Dictionary<string, Dictionary<DateOnly, Dictionary<string, PriceDetails>>>> GetAllPricesAsync()
    {
        return await Task.FromResult(_prices);
    }

    public async Task<Dictionary<DateOnly, Dictionary<string, PriceDetails>>?> GetPricesByCardIdAsync(string cardId)
    {
        _prices.TryGetValue(cardId, out var cardPrices);
        return await Task.FromResult(cardPrices);
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

}