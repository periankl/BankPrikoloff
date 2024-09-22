using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        /// <summary>
        /// Получение чата
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _chatService.GetAll();
            return Ok(Dto.Adapt<List<GetChatRequest>>());
        }
        /// <summary>
        /// Получение чата по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _chatService.GetAll();
            return Ok(Dto.Adapt<GetChatRequest>());
        }
        /// <summary>
        /// Изменение чата
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///       "createdAt": "2024-09-18T21:11:03",
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Add(CreateChatRequest request)
        {
            var Dto = request.Adapt<Chat>();
            await _chatService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Добавление нового чата
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///       "createdAt": "2024-09-18T21:11:03",
        ///     }
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> Update(GetChatRequest request)
        {
            var Dto = request.Adapt<Chat>();
            await _chatService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление чата по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _chatService.Delete(id);
            return Ok();
        }
    }
}
