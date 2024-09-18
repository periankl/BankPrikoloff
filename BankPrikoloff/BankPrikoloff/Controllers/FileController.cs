using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _fileService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _fileService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Domain.Models.File file)
        {
            await _fileService.Create(file);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Domain.Models.File file)
        {
            await _fileService.Update(file);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _fileService.Delete(id);
            return Ok();
        }
    }
}
