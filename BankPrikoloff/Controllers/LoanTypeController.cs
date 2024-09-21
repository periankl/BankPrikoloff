using BusinessLogic.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTypeController : ControllerBase
    {
        private ILoanTypeService _loanTypeService;
        public LoanTypeController(ILoanTypeService loanTypeService)
        {
            _loanTypeService = loanTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _loanTypeService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _loanTypeService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(LoanType loanType)
        {
            await _loanTypeService.Create(loanType);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(LoanType loanType)
        {
            await _loanTypeService.Update(loanType);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _loanTypeService.Delete(id);
            return Ok();
        }
    }
}
