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
    public class LoanTypeController : ControllerBase
    {
        private ILoanTypeService _loanTypeService;
        public LoanTypeController(ILoanTypeService loanTypeService)
        {
            _loanTypeService = loanTypeService;
        }
        /// <summary>
        /// Получение всех типов кредитов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _loanTypeService.GetAll();
            return Ok(dto.Adapt<List<GetLoanTypeRequest>>());
        }
        /// <summary>
        /// Получение типа кредита по ID 
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _loanTypeService.GetById(id);
            return Ok(dto.Adapt<GetLoanTypeRequest>());
        }
        /// <summary>
        /// Создание нового типа кредита
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "name": "Расширенный",
        ///         "interestRate": 8,
        ///         "maxLoanAmount": 100000
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateLoanTypeRequest request)
        {
            var Dto = request.Adapt<LoanType>();
            await _loanTypeService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение типа кредита
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "loanTypeId": 1,
        ///         "name": "Расширенный",
        ///         "interestRate": 8,
        ///         "maxLoanAmount": 100000
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetLoanTypeRequest request)
        {
            var Dto = request.Adapt<LoanType>();
            await _loanTypeService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление типа кредита по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _loanTypeService.Delete(id);
            return Ok();
        }
    }
}
