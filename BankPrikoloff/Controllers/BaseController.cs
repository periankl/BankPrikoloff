using Microsoft.AspNetCore.Mvc;
using Domain.Models;

namespace BankPrikoloff.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];
    }
}
