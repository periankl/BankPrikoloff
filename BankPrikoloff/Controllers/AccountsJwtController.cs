using BusinessLogic.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models.Accounts;
using BankPrikoloff.Authorization;
using BusinessLogic.Servises;
using Domain.Models;


namespace BankPrikoloff.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsJwtController : BaseController
    {
        private readonly IAccountJWTService _accountJWTService;

        public AccountsJwtController(IAccountJWTService accountJWTService)
        {
            _accountJWTService = accountJWTService;
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Lax 

            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("x-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }
            else
            {
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        [AllowAnonymous]
        [HttpPost("authenficate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            var response = await _accountJWTService.Authenticate(model, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponse>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountJWTService.RefreshToken(refreshToken, ipAddress());
            setTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is required" });
            }

            if (!User.OwnsToken(token) && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            await _accountJWTService.RevokeToken(token, ipAddress());   
            return Ok(new {message = "Token revoked" });
        }

        [AllowAnonymous]
        [HttpPost("regitster")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            await _accountJWTService.Register(model, Request.Headers["origin"]);
            return Ok(new { message = "Registration sucsesfull, please check your email for verification insrtructions" });
        }

        [AllowAnonymous]
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest model)
        {
            await _accountJWTService.VerifyEmail(model.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            await _accountJWTService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset insrtructions" });
        }

        [AllowAnonymous]
        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidateResetToken(ValidateResetTokenRequest model)
        {
            await _accountJWTService.ValidateResetToken(model);
            return Ok(new { message = "Token is valid" });
        }


        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {
            await _accountJWTService.ResetPassword(model);
            return Ok(new { message = "Password reset successfullm you can now login" });
        }

        [Authorize(2)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAll()
        {
            var accounts = await _accountJWTService.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<AccountResponse>> GetById(string id)
        {
            if (id != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            var account = await _accountJWTService.GetById(id);
            return Ok();
        }

        [Authorize(2)]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AccountResponse>>> Create(CreateRequest model)
        {
            var account = await _accountJWTService.Create(model);
            return Ok(account);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<AccountResponse>> Update(string id, UpdateRequest model)
        {
            if(id != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            if(User.RoleId != 2)
            {
                model.Role = null;
            }

            var account = await _accountJWTService.Update(id, model);
            return Ok(account);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(string id)
        {
            if(id != User.ClientId && User.RoleId != 2)
            {
                return Unauthorized(new { message = "Unathorized" });
            }
            await _accountJWTService.Delete(id);
            return Ok(new { message = "Account deleted successfully" });
        }

    }


}
