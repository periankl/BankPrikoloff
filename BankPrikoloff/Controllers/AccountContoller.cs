using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Получение всех счетов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var account = await _accountService.GetAll();   
            return Ok(account.Adapt<GetAccountRequest>());
        }
        /// <summary>
        /// Получение счета по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var account = await _accountService.GetById(id);
            return Ok(account.Adapt<List<GetCardRequest>>());
        }

        /// <summary>
        /// Создание нового счета
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "accountId": "Qwerty",
        ///         "clientId": "Qwerty",
        ///         "typeId": 1,
        ///         "currencyId": 1,
        ///         "statusId": 1,
        ///         "balance": 0,
        ///         "updatedAt": "2024-09-21T14:19:50.851Z"
        ///     }
        /// </remarks>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Add(CreateAccountRequest request)
        {
            var account = request.Adapt<Account>();
            await _accountService.Create(account);
            return Ok();
        }
        /// <summary>
        /// Обновление счета
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///       "accountId": "Qwertydd",
        ///       "clientId": "Qwerty1234",
        ///       "typeId": 1,
        ///       "currencyId": 1,
        ///       "statusId": 1,
        ///       "balance": 100,
        ///       "updatedAt": "2024-09-21T14:19:50.85"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// 
        [HttpPut]
        public async Task<IActionResult> Update(GetAccountRequest request)
        {
            var account = request.Adapt<Account>();
            await _accountService.Update(account);
            return Ok();
        }
        /// <summary>
        /// Удаление по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _accountService.Delete(id);
            return Ok();
        }
    }
}

