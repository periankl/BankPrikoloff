using BankPrikoloff.Contracts;
using BusinessLogic.Authorization;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : BaseController
    {

        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        /// <summary>
        /// Получение всех файлов
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dto = await _fileService.GetAll();
            return Ok(dto.Adapt<List<GetFileRequest>>());
        }
        /// <summary>
        /// Получение файла по ID
        /// </summary>
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var dto = await _fileService.GetById(id);
            if (dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            return Ok(dto.Adapt<GetFileRequest>());
        }
        /// <summary>
        /// Создание нового файла
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "messageId": 2,
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateFileRequest request)
        {
            var Dto = request.Adapt<Domain.Models.File>();
            Dto.ClientId = "qwerty";
            Dto.FilePath = $"File{Dto.FileId}";
            if (Dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _fileService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение файла
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "fileId": "qwerty1",
        ///         "filePath": "./qwerty1",
        ///         "messageId": 2,
        ///         "clientId": "qwerty",
        ///         "uploadAt": "2024-09-21T22:30:34.195Z"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetFileRequest request)
        {
            var Dto = request.Adapt<Domain.Models.File>();
            if (Dto.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _fileService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление файла по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var file = await _fileService.GetById(id);
            if (file.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _fileService.Delete(id);
            return Ok();
        }
    }
}