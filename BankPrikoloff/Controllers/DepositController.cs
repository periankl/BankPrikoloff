using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        ///         "depositId": "",
        ///         "depositTypeId": 1,
        ///         "statusId": 1,
        ///         "accountId": "Qwerty",
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add(CreateDepositRequest request)
        {
            var Dto = request.Adapt<Deposit>();
            Dto.DepositId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Dto.StatusId = 1;
            Dto.Document = new Document();
            Dto.Document.DocumentId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Dto.Document.ClientId = "qwerty";
            Dto.DocumentId = Dto.Document.DocumentId;
            Dto.Document.TypeId = 1;
            Dto.Document.Name = Dto.Document.DocumentId;
            Dto.Document.Path = $"/document/{Dto.Document.DocumentId}";
            Dto.Document.CreatedAt = DateTime.Now;
            Dto.Name = $"{Dto.AccountId}Deposit";

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