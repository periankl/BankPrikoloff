using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Models;
using BankPrikoloff.Authorization;
 

namespace BusinessLogic.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<int> _roles; //Сделал массивом
        public AuthorizeAttribute(params int[] roles)
        {
            _roles = roles ?? new int[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }
            var account = (User)context.HttpContext.Items["User"];

            if(account == null || (_roles.Any() && !_roles.Contains(account.RoleId)))
            {
                context.Result = new JsonResult(new {message = "Unauthorized"}) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
