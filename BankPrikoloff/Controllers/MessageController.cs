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
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        /// <summary>
        /// Получение всех сообщений
        /// </summary>
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
        ///         "statusId": 1,
        ///         "tredId": 2,
        ///         "clientId": "qwerty",
        ///         "content": "AAAAA",
        ///         "createdAt": "2024-09-22T12:14:09.047Z"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateMessageRequest request)
        {
            var Dto = request.Adapt<Message>();
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
            await _messageService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление сообщения по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.Delete(id);
            return Ok();
        }
    }
}
