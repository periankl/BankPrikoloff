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
    [Route("api/[controller]")]
    [ApiController]
    public class OperationHistoryController : BaseController
    {
        private IOperationHistoryService _operationHistoryService;
        private IAccountService _accountService;
        public OperationHistoryController(IOperationHistoryService operationHistoryService, IAccountService accountService)
        {
            _operationHistoryService = operationHistoryService;
            _accountService = accountService;
        }
        /// <summary>
        /// Получение всех операций
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _operationHistoryService.GetAll();
            return Ok(dto.Adapt<List<GetOperationHistoryRequest>>());
        }
        /// <summary>
        /// Получение операции по ID
        /// </summary>
        [Authorize(2)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var dto = await _operationHistoryService.GetById(id);
            return Ok(dto.Adapt<GetOperationHistoryRequest>());
        }
        /// <summary>
        /// Создание новой операции
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "operationId": "qwerty1",
        ///         "senderAccountId": "Qwertydd",
        ///         "senderCardId": null,
        ///         "destinationAccountId": "Qwerty",
        ///         "destinationCardId": null,
        ///         "typeId": 1,
        ///         "statusId": 1,
        ///         "date": "2024-09-22T12:39:26.793Z",
        ///         "amount": 100
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateOperationHistoryRequest request)
        {
            var Dto = request.Adapt<OperationHistory>();
            var Account = await _accountService.GetById(Dto.SenderAccountId);

            if (Account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }

            Dto.OperationId = Guid.NewGuid().ToString("N").Substring(0, 9);
            await _operationHistoryService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение операции
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "operationId": "qwerty1",
        ///         "senderAccountId": "Qwertydd",
        ///         "senderCardId": null,
        ///         "destinationAccountId": "Qwerty",
        ///         "destinationCardId": null,
        ///         "typeId": 1,
        ///         "statusId": 1,
        ///         "date": "2024-09-22T12:39:26.793Z",
        ///         "amount": 100
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateOperationHistoryRequest request)
        {
            var Dto = request.Adapt<OperationHistory>();
            var Account = await _accountService.GetById(Dto.SenderAccountId);

            if (Account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _operationHistoryService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление операции по ID
        /// </summary>
        [Authorize(2)]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _operationHistoryService.Delete(id);
            return Ok();
        }
    }
}