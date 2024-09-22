using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Получение пользователя
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _userService.GetAll();
            return Ok(Dto.Adapt<List<GetUserRequest>>());
        }
        /// <summary>
        /// Получение пользователя по ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var Dto = await _userService.GetAll();
            return Ok(Dto.Adapt<GetUserRequest>());
        }
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     POST /Todo
        ///     {
        ///         "clientId": "Qwerty12",
        ///         "roleId": 1,
        ///         "login": "Login",
        ///         "password": "Password",
        ///         "firstName": "FName",
        ///         "lastName": "LName",
        ///         "patronomic": "Patr",
        ///         "dateOfBirth": "2024-09-22T15:04:59.377Z",
        ///         "seriesPasport": 4512,
        ///         "numberPasport": 123456,
        ///         "email": "example@example.com",
        ///         "chatId": 3,
        ///         "createdAt": "2024-09-22T15:04:59.377Z",
        ///         "updatedAt": "2024-09-22T15:04:59.377Z"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var Dto = request.Adapt<User>();
            await _userService.Create(Dto);
            return Ok();
        }
        /// <summary>
        /// Изменение пользователя
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// 
        ///     PUT /Todo
        ///     {
        ///         "clientId": "qwerty1",
        ///         "roleId": 1,
        ///         "login": "Login",
        ///         "password": "admin",
        ///         "firstName": "admin",
        ///         "lastName": "admin",
        ///         "patronomic": "admin",
        ///         "dateOfBirth": "2000-01-01T00:00:00",
        ///         "seriesPasport": 1234,
        ///         "numberPasport": 567890,
        ///         "email": "admin@admin.com",
        ///         "chatId": 2,
        ///         "createdAt": "2024-09-14T21:12:40",
        ///         "deletedBy": null,
        ///         "deletedAt": null,
        ///         "updatedAt": "2024-09-14T21:12:47"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetUserRequest request)
        {
            var Dto = request.Adapt<User>();
            await _userService.Update(Dto);
            return Ok();
        }
        /// <summary>
        /// Удаление пользователя по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}