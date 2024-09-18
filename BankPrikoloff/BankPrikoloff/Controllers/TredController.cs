using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Domain.Models;


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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tredService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _tredService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Tred tred)
        {
            await _tredService.Create(tred);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Tred tred)
        {
            await _tredService.Update(tred);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _tredService.Delete(id);
            return Ok();
        }
    }
}
