using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Domain.Models;
using BankPrikoloff.Contracts;
using BusinessLogic.Servises;
using Mapster;
using System;


namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TredController : ControllerBase
    {
        private ITredService _tredService;
        public TredController(ITredService tredService)
        {
            _tredService = tredService;
        }
        /// <summary>
        /// Получение треда
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _tredService.GetAll();
            return Ok(Dto.Adapt<List<GetTredRequest>>());
        }
        /// <summary>
        /// Получение треда по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _tredService.GetAll();
            return Ok(Dto.Adapt<GetTredRequest>());
        }
        /// <summary>
        /// Создание нового счета
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "chatId": 1,
        ///         "operatorId": null,
        ///         "createdAt": "2024-09-22T14:56:10.707Z",
        ///         "isClosed": false,
        ///         "closedAt": null
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateTredRequest request)
        {
            var Dto = request.Adapt<Tred>();
            await _tredService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Создание нового счета
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "tredId": 1,
        ///         "chatId": 1,
        ///         "operatorId": null,
        ///         "createdAt": "2024-09-22T14:56:10.707",
        ///         "isClosed": false,
        ///         "closedAt": null
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetTredRequest request)
        {
            var Dto = request.Adapt<Tred>();
            await _tredService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление треда по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _tredService.Delete(id);
            return Ok();
        }
    }
}
