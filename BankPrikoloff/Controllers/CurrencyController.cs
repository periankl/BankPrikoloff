using BankPrikoloff.Contracts;
using BusinessLogic.Authorization;
using BusinessLogic.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        /// <summary>
        /// Получение курса валюты
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _currencyService.GetAll();
            return Ok(Dto.Adapt<List<GetCurrencyRequest>>());
        }
        /// <summary>
        /// Получение курса валюты по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _currencyService.GetAll();
            return Ok(Dto.Adapt<GetCurrencyRequest>());
        }
        /// <summary>
        /// Добавление нового курса валюты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "name": "Билет банка приколов",
        ///         "course": "100000"
        ///     }
        /// </remarks>
        [Authorize(2)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateCurrencyRequest request)
        {
            var Dto = request.Adapt<Currency>();
            await _currencyService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение курса валюты
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "currencyId": "1",
        ///         "name": "Билет банка приколов",
        ///         "course": "100000"
        ///     }
        /// </remarks>
        [Authorize(2)]
        [HttpPut]
        public async Task<IActionResult> Update(GetCurrencyRequest request)
        {
            var Dto = request.Adapt<Currency>();
            await _currencyService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление курса валюты по ID
        /// </summary>
        [Authorize(2)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _currencyService.Delete(id);
            return Ok();
        }
    }
}