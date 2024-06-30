using System.Text.Json;
using clodlog_backend.Models;

namespace clodlog_backend.Repositories;

public class SetRepository
{
    private readonly string _dataPath;
    private List<Set> _sets;

    public SetRepository(string dataPath)
    {
        _dataPath = dataPath;
        _sets = new List<Set>();
        LoadSets();
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


    public async Task<IEnumerable<Set>> GetAllAsync()
    {
        return await Task.FromResult(_sets);
    }

    public async Task<Set?> GetByIdAsync(string id)
    {
        return await Task.FromResult(_sets.FirstOrDefault(s => s.Id == id));
    }

    public async Task<Dictionary<string, List<Set>>> GetSeriesSetMapAsync()
    {
        return await Task.FromResult(_sets.GroupBy(s => s.Series).ToDictionary(g => g.Key, g => g.ToList()));
    }
}