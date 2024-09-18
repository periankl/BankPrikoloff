using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Domain.Models;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositTypeController : ControllerBase
    {
        private IDepositTypeService _depositTypeService;
        public DepositTypeController(IDepositTypeService depositTypeService)
        {
            _depositTypeService = depositTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _depositTypeService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _depositTypeService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepositType depositType)
        {
            await _depositTypeService.Create(depositType);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DepositType depositType)
        {
            await _depositTypeService.Update(depositType);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _depositTypeService.Delete(id);
            return Ok();
        }

    }
}
