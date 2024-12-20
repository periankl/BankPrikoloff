using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Authorization;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : BaseController
    {
        private ILoanService _loanService;
        private BankContext _context;
        private IAccountService _accountService;
        public LoanController(BankContext context, ILoanService loanService, IAccountService accountService)
        {
            _context = context;
            _loanService = loanService;
            _accountService = accountService;
        }
        /// <summary>
        /// Получение всех кредитов
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _loanService.GetAll();
            return Ok(dto.Adapt<List<GetLoanRequest>>());
        }
        /// <summary>
        /// Получение кредита по ID
        /// </summary>
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var dto = await _loanService.GetById(id);
            var account = await _accountService.GetById(dto.AccountId);

            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            return Ok(dto.Adapt<GetLoanRequest>());
        }
        /// <summary>
        /// Создание нового кредита
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "accountId": "Qwerty",
        ///         "loanTypeId": 1,
        ///         "amount": 3000,
        ///         "endDate": "2026-09-22T10:57:25.113"
        ///     }
        /// </remarks>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<IActionResult> Add(CreateLoanRequest request)
        {
            var Dto = request.Adapt<Loan>();
            var account = await _accountService.GetById(Dto.AccountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            Dto.RemarningAmount = Dto.Amount;
            Dto.Document = new Document();
            Dto.Document.DocumentId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Dto.Document.ClientId = (await _context.Accounts.FirstOrDefaultAsync(x => x.AccountId == Dto.AccountId)).ClientId;
            Dto.Document.TypeId = 2;
            Dto.Document.Path = $"/document/{Dto.Document.DocumentId}";
            Dto.Document.Name = Dto.Document.DocumentId;
            Dto.DocumentId = Dto.Document.DocumentId;
            _context.Loans.Add(Dto);
            _context.Documents.Add(Dto.Document);

            await _context.SaveChangesAsync();

            await _loanService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Редактирование кредита
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "loanId": "Qwerty",
        ///         "accountId": "Qwerty",
        ///         "loanTypeId": 1,
        ///         "statusId": 1,
        ///         "documentId": "Qwerty1",
        ///         "amount": 3000,
        ///         "remarningAmount": 0,
        ///         "startDate": "2024-09-22T10:57:25.113",
        ///         "endDate": "2026-09-22T10:57:25.113"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetLoanRequest request)
        {
            var Dto = request.Adapt<Loan>();
            var account = await _accountService.GetById(Dto.AccountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _loanService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление кредита по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var loan = await _loanService.GetById(id);
            var account = await _accountService.GetById(loan.AccountId);
            if (account.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _loanService.Delete(id);
            return Ok();
        }
    }
}