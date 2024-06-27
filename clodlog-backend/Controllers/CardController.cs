using clodlog_backend.Models;
using clodlog_backend.Models.Criteria;
using clodlog_backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace clodlog_backend.Controllers;

[ApiController]
[Route("api/card")]
public class CardController : ControllerBase
{
    private readonly CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCards([FromQuery] CardQueryCriteria? criteria)
    {
        IEnumerable<Card> cards;

        if (criteria == null)
        {
            cards = await _cardService.GetAllCardsAsync();
        }
        else
        {
            cards = await _cardService.GetCardsByCriteriaAsync(criteria);
        }

        return Ok(cards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById(string id)
    {
        var card = await _cardService.GetCardByIdAsync(id);
        return Ok(card);
    }
    
    [HttpGet("set/{setId}")]
    public async Task<IActionResult> GetCardsBySetId(string setId)
    {
        var cards = await _cardService.GetCardsBySetIdAsync(setId);
        return Ok(cards);
    }
}