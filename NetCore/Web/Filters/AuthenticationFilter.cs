using Core.Users;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Filters
{
    public class AuthenticationFilter : IActionFilter
    {
        private readonly DatabaseContext ctx;
        private readonly UserManager<User> userManager;

        public AuthenticationFilter(DatabaseContext ctx, UserManager<User> userManager)
        {
            this.ctx = ctx;
            this.userManager = userManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
            {
                ctx.CurrentUser = ctx.Users.IncludeAll().First(u => u.UserName == username);
            }
        }
    }

}
