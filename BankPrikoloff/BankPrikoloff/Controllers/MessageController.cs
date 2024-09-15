using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using DataAccess.Models;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _messageService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _messageService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Message message)
        {
            await _messageService.Create(message);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Message message)
        {
            await _messageService.Update(message);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.Delete(id);
            return Ok();
        }
    }
}
