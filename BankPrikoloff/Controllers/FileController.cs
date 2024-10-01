using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// <summary>
        /// Получение всех файлов
        /// </summary>
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
            Dto.FileId = Guid.NewGuid().ToString("N").Substring(0, 9);
            Dto.ClientId = "qwerty";
            Dto.FilePath = $"File{Dto.FileId}";
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
            await _fileService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление файла по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _fileService.Delete(id);
            return Ok();
        }
    }
}