using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using Domain.Models;
using Mapster;
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
        /// <summary>
        /// Получение всех карт
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var card = await _cardService.GetAll();
            return Ok(card.Adapt<GetCardRequest>());
        }
        /// <summary>
        /// Получение карты по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var card = await _cardService.GetById(id);
            return Ok(card.Adapt<List<GetCardRequest>>());
        }
        /// <summary>
        /// Добавление новой карты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///       "cardId": "Qwerty",
        ///       "typeId": 1,
        ///       "currencyId": 1,
        ///       "accountId": "Qwerty",
        ///       "cardNumber": "2222222222222222",
        ///       "expDate": "2024-09-21T17:41:27.482Z",
        ///       "cvv": "562",
        ///       "ownerName": "QWERTY",
        ///       "balance": 0,
        ///       "blocked": false,
        ///       "createdAt": "2024-09-21T17:41:27.482Z"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add(CreateCardRequest request)
        {
            var card = request.Adapt<Card>();
            await _cardService.Create(card);
            return Ok();
        }
        /// <summary>
        /// Изменение карты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///       "cardId": "Qwerty",
        ///       "typeId": 1,
        ///       "currencyId": 1,
        ///       "accountId": "Qwerty",
        ///       "cardNumber": "2222222222222222",
        ///       "expDate": "2024-09-21T17:41:27.482Z",
        ///       "cvv": "562",
        ///       "ownerName": "QWERTY",
        ///       "balance": 0,
        ///       "blocked": false,
        ///       "createdAt": "2024-09-21T17:41:27.482Z"
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Update(GetCardRequest request)
        {
            var card = request.Adapt<Card>();
            await _cardService.Update(card);
            return Ok();
        }
        /// <summary>
        /// Удаление карты по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _cardService.Delete(id);
            return Ok();
        }
    }
}
