using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private IDepositService _depositService;
        private BankContext _context;
        public DepositController(BankContext context, IDepositService depositService)
        {
            _context = context;
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

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetByAccount(string accountId)
        {
            var card = await _depositService.GetByAccountId(accountId);
            return Ok(card.Adapt<GetCardRequest>());
        }
        /// <summary>
        /// Создание нового вклада
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "depositTypeId": 1,
        ///         "accountId": "Qwerty",
        ///         "endTime": null
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add(CreateDepositRequest request)
        {
            var Dto = request.Adapt<Deposit>();
            Dto.Document = new Document();
            Dto.Document.DocumentId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Dto.Document.ClientId = (await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == Dto.AccountId)).ClientId;
            Dto.DocumentId = Dto.Document.DocumentId;
            Dto.Document.TypeId = 1;
            Dto.Document.Name = Dto.Document.DocumentId;
            Dto.Document.Path = $"/document/{Dto.Document.DocumentId}";
            Dto.Document.CreatedAt = DateTime.Now;
            Dto.Name = $"{Dto.AccountId}Deposit";
            _context.Deposits.Add(Dto);
            _context.Documents.Add(Dto.Document);
            await _context.SaveChangesAsync();
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
        ///         "startDate": "2024-09-21T19:04:25.120Z",
        ///         "endTime": null

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