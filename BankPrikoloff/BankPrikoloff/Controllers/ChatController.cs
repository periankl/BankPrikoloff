using BusinessLogic.Interfaces;
using DataAccess.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _chatService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _chatService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Chat chat)
        {
            await _chatService.Create(chat);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Chat chat)
        {
            await _chatService.Update(chat);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _chatService.Delete(id);
            return Ok();
        }
    }
}
