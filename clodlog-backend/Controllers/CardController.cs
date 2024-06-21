using clodlog_backend.Services;
using Microsoft.AspNetCore.Mvc;
namespace clodlog_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{
    private readonly CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        var cards = await _cardService.GetAllCardsAsync();
        return Ok(cards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById(string id)
    {
        var card = await _cardService.GetCardByIdAsync(id);
        if (card == null)
            return NotFound();
        return Ok(card);
    }
}