using clodlog_backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace clodlog_backend.Controllers;

[ApiController]
[Route("api/meta")]
public class MetaController : ControllerBase
{
    private readonly SetService _setService;
    private readonly CardService _cardService;

    public MetaController(SetService setService , CardService cardService)
    {
        _setService = setService;
        _cardService = cardService;
    }
    
    [HttpGet("series")]
    public async Task<IActionResult> GetAllSeriesNames()
    {
        var list = await _setService.GetAllSeriesNamesAsync();
        return Ok(list);
    }
    
    [HttpGet("sets")]
    public async Task<IActionResult> GetAllSetNames()
    {
        var list = await _setService.GetAllSetNamesAsync();
        return Ok(list);
    }
    
    [HttpGet("rarities")]
    public async Task<IActionResult> GetAllRarities()
    {
        var list = await  _cardService.GetRaritiesAsync();
        return Ok(list);
    }
    
    [HttpGet("supertypes")]
    public async Task<IActionResult> GetAllSuperTypes()
    {
        var list = await _cardService.GetSuperTypesAsync();
        return Ok(list);
    }
    
    [HttpGet("subtypes")]
    public async Task<IActionResult> GetAllSubTypes()
    {
        var list = await _cardService.GetSubTypesAsync();
        return Ok(list);
    }
    
    [HttpGet("hp")]
    public async Task<IActionResult> GetAllHpValues()
    {
        var list = await _cardService.GetHpValuesAsync();
        return Ok(list);
    }
    
    [HttpGet("artists")]
    public async Task<IActionResult> GetAllArtistNames()
    {
        var list = await _cardService.GetArtistsAsync();
        return Ok(list);
    }
    
    [HttpGet("types")]
    public async Task<IActionResult> GetAllTypes()
    {
        var list = await _cardService.GetTypesAsync();
        return Ok(list);
    }
}
