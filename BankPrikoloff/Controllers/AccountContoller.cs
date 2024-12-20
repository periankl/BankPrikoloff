using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Authorization;



namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route(template: "api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Получение информации о всех счетах
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var account = await _accountService.GetAll();
            return Ok(account.Adapt<List<GetAccountRequest>>());
        }
        /// <summary>
        /// Получение информации о счете по ID
        /// </summary>
        [Authorize(2)]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var account = await _accountService.GetById(id);
            return Ok(account.Adapt<GetAccountRequest>());
        }

        /// <summary>
        /// Получение информации о счете по ClientID
        /// </summary>
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetByClientId(string clientId)
        {
            if (clientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            var account = await _accountService.GetUserAccounts(clientId);
            return Ok(account.Adapt<List<GetAccountRequest>>());
        }



        /// <summary>
        /// Создание нового счета
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "clientId": "Qwerty",
        ///         "typeId": 1,
        ///         "currencyId": 1,
        ///     }
        /// </remarks>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Add(CreateAccountRequest request)
        {

            var Dto = request.Adapt<Account>();
            if (Dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            Dto.AccountId = Guid.NewGuid().ToString("N").Substring(0, 9);
            await _accountService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Обновление информации о счете
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
            var Dto = request.Adapt<Account>();
            if (Dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _accountService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление счета по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var account = await _accountService.GetById(id);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _accountService.Delete(id);
            return Ok();
        }
    }
}