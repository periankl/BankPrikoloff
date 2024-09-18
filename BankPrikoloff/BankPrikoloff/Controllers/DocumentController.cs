using BusinessLogic.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _documentService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _documentService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Document document)
        {
            await _documentService.Create(document);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Document document)
        {
            await _documentService.Update(document);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _documentService.Delete(id);
            return Ok();
        }
    }
}
