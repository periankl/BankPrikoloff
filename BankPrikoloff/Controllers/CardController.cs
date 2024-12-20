using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Authorization;


namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : BaseController
    {
        private ICardService _cardService;
        private IAccountService _accountService;
        public CardController(ICardService cardService, IAccountService accountService)
        {
            _cardService = cardService;
            _accountService = accountService;
        }
        /// <summary>
        /// Получение всех карт
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var card = await _cardService.GetAll();
            return Ok(card.Adapt<List<GetCardRequest>>());
        }
        /// <summary>
        /// Получение карты по ID
        /// </summary>
        [Authorize(2)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var card = await _cardService.GetById(id);
            return Ok(card.Adapt<GetCardRequest>());
        }

        /// <summary>
        /// Получение карт по AccountID
        /// </summary>
        /// 
        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetByAccount(string accountId)
        {
            var account = await _accountService.GetById(accountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            var card = await _cardService.GetByAccountId(accountId);
            return Ok(card.Adapt<GetCardRequest>());
        }

        /// <summary>
        /// Получение карты по номеру
        /// </summary>
        [HttpGet("by-number/{cardNumber}")]
        public async Task<IActionResult> GetByCardNumber(string cardNumber)
        {
            var card = await _cardService.GetByCardNumber(cardNumber);
            return Ok(card.Adapt<GetCardRequest>());
        }


        /// <summary>
        /// Добавление новой карты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///       "typeId": 1,
        ///       "currencyId": 1,
        ///       "accountId": "Qwerty",
        ///       "ownerName": "QWERTY QWERTY",
        ///     }
        /// </remarks>
        
        [HttpPost]
        public async Task<IActionResult> Add(CreateCardRequest request)
        {
            var Dto = request.Adapt<Card>();
            Dto.CardId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Random random = new Random();
            int[] digits = new int[16];
            for (int i = 0; i < 15; i++)
            {
                digits[i] = random.Next(1, 10);
            }
            int sum = 0;
            bool shouldDouble = true;
            for (int i = 14; i >= 0; i--)
            {
                int digit = digits[i];

                if (shouldDouble)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                shouldDouble = !shouldDouble;
            }
            int checkDigit = (10 - (sum % 10)) % 10;
            digits[15] = checkDigit;
            Dto.CardNumber = string.Join("", digits);
            Dto.ExpDate = DateTime.Now.AddYears(3);
            Dto.Cvv = random.Next(100, 1000).ToString();
            Dto.ExpDate = Dto.CreatedAt.AddYears(3);
            Dto.Blocked = false;
            var account = await _accountService.GetById(Dto.AccountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _cardService.Create(Dto);
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
            var account = await _accountService.GetById(card.AccountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _cardService.Update(card);
            return Ok();
        }
        /// <summary>
        /// Удаление карты по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var card = await _cardService.GetById(id);
            var account = await _accountService.GetById(card.AccountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _cardService.Delete(id);
            return Ok();
        }
    }
}