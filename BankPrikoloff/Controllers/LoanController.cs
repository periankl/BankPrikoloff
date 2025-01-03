﻿using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private ILoanService _loanService;
        private BankContext _context;
        public LoanController(BankContext context, ILoanService loanService)
        {
            _context = context;
            _loanService = loanService;
        }
        /// <summary>
        /// Получение всех кредитов
        /// </summary>
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
            return Ok(dto.Adapt<GetLoanRequest>());
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetByAccount(string accountId)
        {
            var card = await _loanService.GetByAccountId(accountId);
            return Ok(card.Adapt<GetLoanRequest>());
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
        [HttpPost]
        public async Task<IActionResult> Add(CreateLoanRequest request)
        {
            var Dto = request.Adapt<Loan>();
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
            await _loanService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление кредита по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _loanService.Delete(id);
            return Ok();
        }
    }
}