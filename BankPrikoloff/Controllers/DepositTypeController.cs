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
    public class DepositTypeController : ControllerBase
    {
        private IDepositTypeService _depositTypeService;
        public DepositTypeController(IDepositTypeService depositTypeService)
        {
            _depositTypeService = depositTypeService;
        }
        /// <summary>
        /// Получение всех типов вкладов
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _depositTypeService.GetAll();
            return Ok(dto.Adapt<List<GetDepositTypeRequest>>());
        }
        /// <summary>
        /// Получение типа вклада по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _depositTypeService.GetById(id);
            return Ok(dto.Adapt<GetDepositTypeRequest>());
        }
        /// <summary>
        /// Создание нового типа вклада
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "name": "Накопительный",
        ///         "interestRate": 5,
        ///         "minAmount": 500,
        ///         "minTerm": 1,
        ///         "createdAt": "2024-09-21T21:52:18",
        ///         "deletedAt": null
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateDepositTypeRequest request)
        {
            var Dto = request.Adapt<DepositType>();
            await _depositTypeService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение типа вклада
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "depositTypeId": 1,
        ///         "name": "Накопительный",
        ///         "interestRate": 5,
        ///         "minAmount": 500,
        ///         "minTerm": 1,
        ///         "createdAt": "2024-09-21T21:52:18",
        ///         "deletedAt": null
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetDepositTypeRequest request)
        {
            var Dto = request.Adapt<DepositType>();
            await _depositTypeService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление вклада по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _depositTypeService.Delete(id);
            return Ok();
        }

    }
}