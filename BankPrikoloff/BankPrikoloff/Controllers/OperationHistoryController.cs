using BusinessLogic.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationHistoryController : ControllerBase
    {
        private IOperationHistoryService _operationHistoryService;
        public OperationHistoryController(IOperationHistoryService operationHistoryService)
        {
            _operationHistoryService = operationHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _operationHistoryService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _operationHistoryService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(OperationHistory operationHistory)
        {
            await _operationHistoryService.Create(operationHistory);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(OperationHistory operationHistory)
        {
            await _operationHistoryService.Update(operationHistory);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _operationHistoryService.Delete(id);
            return Ok();
        }
    }
}
