using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Domain.Models;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private IDepositService _depositService;
        public DepositController(IDepositService depositService)
        {
            _depositService = depositService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _depositService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _depositService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Deposit deposit)
        {
            await _depositService.Create(deposit);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Deposit deposit)
        {
            await _depositService.Update(deposit);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _depositService.Delete(id);
            return Ok();
        }
    }
}
