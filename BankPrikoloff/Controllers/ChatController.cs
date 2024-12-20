using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Authorization;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseController
    {
        private IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        /// <summary>
        /// Получение чата
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _chatService.GetAll();
            return Ok(Dto.Adapt<List<GetChatRequest>>());
        }
        /// <summary>
        /// Получение чата по ID
        /// </summary>
        [Authorize(2)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var Dto = await _chatService.GetAll();
            return Ok(Dto.Adapt<GetChatRequest>());
        }
        /// <summary>
        /// Добавление нового чата
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///     }
        /// </remarks>
        [Authorize(1,2)]
        [HttpPost]
        public async Task<IActionResult> Add(CreateChatRequest request)
        {
            var Dto = request.Adapt<Chat>();
            await _chatService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение чата
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///       "createdAt": "2024-09-18T21:11:03",
        ///     }
        /// </remarks>
        [Authorize(1, 2)]
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
        [Authorize(2)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _chatService.Delete(id);
            return Ok();
        }
    }
}