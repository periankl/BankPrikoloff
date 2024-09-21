using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Domain.Models;
using BankPrikoloff.Contracts;
using BusinessLogic.Servises;
using Mapster;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private IDepositService _depositService;
        public DepositController(IDepositService depositService)
        {
            _depositService = depositService;
        }
        /// <summary>
        /// Получение всех вкладов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _depositService.GetAll();
            return Ok(dto.Adapt<List<GetDepositRequest>>());
        }
        /// <summary>
        /// Получение вклада по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var dto = await _depositService.GetById(id);
            return Ok(dto.Adapt<GetDepositRequest>());
        }
        /// <summary>
        /// Создание нового вклада
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "depositId": "Qwerty1",
        ///         "depositTypeId": 1,
        ///         "statusId": 1,
        ///         "documentId": "Qwerty",
        ///         "accountId": "Qwerty",
        ///         "name": "Dep",
        ///         "startDate": "2024-09-21T19:04:25.120Z"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add(CreateDepositRequest request)
        {
            var Dto = request.Adapt<Deposit>();
            await _depositService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение вклада
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "depositId": "Qwerty1",
        ///         "depositTypeId": 1,
        ///         "statusId": 1,
        ///         "documentId": "Qwerty",
        ///         "accountId": "Qwerty",
        ///         "name": "Dep",
        ///         "startDate": "2024-09-21T19:04:25.120Z"
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Update(GetDepositRequest request)
        {
            var Dto = request.Adapt<Deposit>();
            await _depositService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление вклада по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _depositService.Delete(id);
            return Ok();
        }
    }
}
