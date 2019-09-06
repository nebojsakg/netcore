using System.Linq;
using Common.Extensions;
using Controllers;
using Core.Auth.Command;
using Core.Users;
using Domain;
using Web.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly LoginCommandValidator _loginCmdValidator;
        private readonly RegisterCommandValidator _registerCmdValidator;

        public AccountController(
            DatabaseContext ctx,
            LoginCommandValidator loginCmdValidator,
            RegisterCommandValidator registerCmdValidator
            ) : base(ctx)
        {
            _loginCmdValidator = loginCmdValidator;
            _registerCmdValidator = registerCmdValidator;
        }

        [Route("/api/account/login")]
        [HttpPost]
        public object Login([FromBody] LoginCommand cmd)
        {
            _loginCmdValidator.ValidateCmd(cmd);

            var appUser = ctx.Users
                .IncludeRoles()
                .First(r => r.Email == cmd.Email);

            return OkResponse(SecurityHelper.GenerateJwtToken(cmd.Email, appUser));
        }

        [Route("/api/account/register")]
        [HttpPost]
        public object Register([FromBody] RegisterCommand cmd)
        {
            _registerCmdValidator.ValidateCmd(cmd);

            var createdUser = ctx.Users
                .IncludeRoles()
                .First(r => r.Email == cmd.Email);

            return OkResponse(SecurityHelper.GenerateJwtToken(cmd.Email, createdUser));
        }
    }
}