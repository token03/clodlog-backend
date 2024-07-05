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
        var seriesSetMap = await _setService.GetSeriesSetMapAsync();
        return Ok(seriesSetMap);
    }
    
    [HttpGet("set")]
    public async Task<IActionResult> GetAllSetNames()
    {
        var seriesSetMap = await _setService.GetSeriesSetMapAsync();
        return Ok(seriesSetMap);
    }
    
    [HttpGet("rarity")]
    public async Task<IActionResult> GetAllRarities()
    {
        var seriesSetMap = await _setService.GetSeriesSetMapAsync();
        return Ok(seriesSetMap);
    }
    
    [HttpGet("superType")]
    public async Task<IActionResult> GetAllSuperTypes()
    {
        var seriesSetMap = await _setService.GetSeriesSetMapAsync();
        return Ok(seriesSetMap);
    }
    
    [HttpGet("subType")]
    public async Task<IActionResult> GetAllSubTypes()
    {
        var seriesSetMap = await _setService.GetSeriesSetMapAsync();
        return Ok(seriesSetMap);
    }
}
