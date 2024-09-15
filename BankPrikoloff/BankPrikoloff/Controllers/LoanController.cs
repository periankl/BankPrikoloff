using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using DataAccess.Models;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _loanService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _loanService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Loan loan)
        {
            await _loanService.Create(loan);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Loan loan)
        {
            await _loanService.Update(loan);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _loanService.Delete(id);
            return Ok();
        }
    }
}
