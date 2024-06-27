using clodlog_backend.Models;
using clodlog_backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace clodlog_backend.Controllers;

[ApiController]
[Route("api/set")]
public class SetController : ControllerBase
{
    private readonly SetService _setService;

    public SetController(SetService setService)
    {
        _setService = setService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSets()
    {
        var sets = await _setService.GetAllSetsAsync();
        return Ok(sets);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSetById(string id)
    {
        var card = await _setService.GetSetByIdAsync(id);
        return Ok(card);
    }
    
    [HttpGet("series")]
    public async Task<IActionResult> GetSeriesSetMap()
    {
        var seriesSetMap = await _setService.GetSeriesSetMapAsync();
        return Ok(seriesSetMap);
    }
}
