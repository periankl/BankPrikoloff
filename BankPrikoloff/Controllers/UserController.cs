using BankPrikoloff.Contracts;
using BusinessLogic.Authorization;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace BankPrikoloff.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Получение пользователя
        /// </summary>
        [Authorize(2)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Dto = await _userService.GetAll();
            return Ok(Dto.Adapt<List<GetUserRequest>>());
        }
        /// <summary>
        /// Получение пользователя по ID
        /// </summary>
        [Authorize(2)]
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var Dto = await _userService.GetById(id);
            return Ok(Dto.Adapt<GetUserRequest>());
        }

        /// <summary>
        /// Получение пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Password</param>
        [HttpGet("logpas/{login}/{password}")]
        public async Task<IActionResult> GetByLogin(string login, string password)
        {
            var Dto = await _userService.GetByLogin(login, password);
            return Ok(Dto.Adapt<GetUserRequest>());
        }
        [AllowAnonymous]
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var Dto = await _userService.GetByEmail(email);
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
        ///         "login": "Login",
        ///         "password": "Password",
        ///         "firstName": "FName",
        ///         "lastName": "LName",
        ///         "patronomic": "Patr",
        ///         "dateOfBirth": "2004-09-22T15:04:59.377Z",
        ///         "seriesPasport": 4512,
        ///         "numberPasport": 123456,
        ///         "email": "example@example.com"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var Dto = request.Adapt<User>();
            Dto.ClientId = Guid.NewGuid().ToString();
            Dto.Chat = new Chat();
            Dto.ChatId = Dto.Chat.ChatId;
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
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(GetUserRequest request)
        {
            var Dto = request.Adapt<User>();
            await _userService.Update(Dto);
            Dto.UpdatedAt = DateTime.Now;
            return Ok();
        }
        /// <summary>
        /// Удаление пользователя по ID
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetById(id);
            if (user.ClientId != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _userService.Delete(id);
            return Ok();
        }
    }
}