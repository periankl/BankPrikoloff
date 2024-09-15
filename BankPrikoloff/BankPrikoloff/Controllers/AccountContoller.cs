using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountContoller : ControllerBase
    {
        private IAccountService _accountService;
        public AccountContoller(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _accountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _accountService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Account account)
        {
            await _accountService.Create(account);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Account account)
        {
            await _accountService.Update(account);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _accountService.Delete(id);
            return Ok();
        }
    }
}

