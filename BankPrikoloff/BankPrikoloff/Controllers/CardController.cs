using BusinessLogic.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cardService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _cardService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Card card)
        {
            await _cardService.Create(card);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Card card)
        {
            await _cardService.Update(card);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _cardService.Delete(id);
            return Ok();
        }
    }
}
