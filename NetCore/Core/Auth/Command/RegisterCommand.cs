using Core.Common.Commands;
using Domain.Model;
using FluentValidation;
using Localization.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Core.Auth.Command
{
    public class RegisterCommand : IBaseCommand
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public RegisterCommandValidator(IStringLocalizer<SharedResource> stringLocalizer, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

            RuleFor(cmd => cmd.Email).NotEmpty();
            RuleFor(cmd => cmd.Password).NotEmpty();
            RuleFor(cmd => cmd)
            .MustAsync((cmd, cancellationToken) => CanRegister(cmd)).WithMessage(stringLocalizer["UsernameTaken"]);
        }

        private async Task<bool> CanRegister(RegisterCommand registerCommand)
        {
            var user = await userManager.FindByEmailAsync(registerCommand.Email);

            return user == null;
        }
    }
}
