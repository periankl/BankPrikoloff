using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Authorization;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : BaseController
    {
        private IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        /// <summary>
        /// Получение всех документов
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _documentService.GetAll();
            return Ok(dto.Adapt<List<GetDocumentRequest>>());
        }
        /// <summary>
        /// Получение документа по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
           
            var dto = await _documentService.GetById(id);
            if (dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            return Ok(dto.Adapt<GetDocumentRequest>());
        }
        /// <summary>
        /// Создание документа
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "clientId": "Qwerty1234",
        ///         "typeId": 1,
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateDocumentRequest request)
        {
            var Dto = request.Adapt<Document>();
            Dto.DocumentId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Dto.Name = $"Doc{Dto.DocumentId}";
            Dto.Path = $"Docs/{Dto.Name}";
            if (Dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _documentService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Редактирование документа
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "documentId": "Qwerty1",
        ///         "clientId": "Qwerty1234",
        ///         "typeId": 1,
        ///         "name": "Депозит",
        ///         "path": "/Depositqwerty1",
        ///         "createdAt": "2024-09-21T22:06:09"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetDocumentRequest request)
        {
            var Dto = request.Adapt<Document>();
            if (Dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _documentService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление документа по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var document = await _documentService.GetById(id);
            if (document.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _documentService.Delete(id);
            return Ok();
        }
    }
}