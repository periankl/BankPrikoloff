using BankPrikoloff.Contracts;
using BusinessLogic.Authorization;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {
        private IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        /// <summary>
        /// Получение всех сообщений
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _messageService.GetAll();
            return Ok(dto.Adapt<List<GetMessageRequest>>());
        }
        /// <summary>
        /// Получение сообщения по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _messageService.GetById(id);
            if (dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            return Ok(dto.Adapt<GetMessageRequest>());
        }
        /// <summary>
        /// Создание нового сообщения
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "tredId": 2,
        ///         "clientId": "qwerty",
        ///         "content": "AAAAA",
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateMessageRequest request)
        {
            var Dto = request.Adapt<Message>();
            if (Dto.ClientId != User.ClientId)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _messageService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение сообщения
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "messageId": 1,
        ///         "statusId": 1,
        ///         "tredId": 2,
        ///         "clientId": "qwerty",
        ///         "content": "AAAAA",
        ///         "createdAt": "2024-09-22T12:14:09.047Z"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetMessageRequest request)
        {
            var Dto = request.Adapt<Message>();
            if (Dto.ClientId != User.ClientId)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _messageService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление сообщения по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _messageService.GetById(id);
            if (message.ClientId != User.ClientId)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _messageService.Delete(id);
            return Ok();
        }
    }
}