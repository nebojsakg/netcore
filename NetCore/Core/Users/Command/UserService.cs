using Common.Extensions;
using Core.Auth.Command;
using Core.Common;
using Core.Users.Query.Response;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Core.Users.Command
{
    public class UserService : BaseService
    {
        private readonly RegisterCommandValidator _registerCmdValidator;
        private readonly UserManager<User> _userManager;

        public UserService(DatabaseContext ctx, RegisterCommandValidator registerCmdValidator, UserManager<User> userManager) : base(ctx)
        {
            _registerCmdValidator = registerCmdValidator;
            _userManager = userManager;
        }

        /// <summary>
        /// Creates new user and assigne User role by default
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<User> Create(RegisterCommand cmd)
        {
            _registerCmdValidator.ValidateCmd(cmd);

            var user = new User
            {
                UserName = cmd.Email,
                Email = cmd.Email
            };

            var result = await _userManager.CreateAsync(user, cmd.Password);
            if (!result.Succeeded)
                throw new Exception("Identity User Creation Failed.");

            result = await _userManager.AddToRoleAsync(user, Domain.Constants.Roles.User);
            if (!result.Succeeded)
                throw new Exception("Identity User Role Creation Failed.");

            return await ctx.Users
                .IncludeRoles()
                .FirstOrDefaultAsync(u => u.Email == cmd.Email);
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public async Task<User> Update(int id, UserDetails cmd)
        {
            cmd.Id = id;

            var user = await ctx.Users.FindAsync(id);

            user.Name = cmd.Name;
            user.PhoneNumber = cmd.PhoneNumber;

            await ctx.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var user = await ctx.Users.FindAsync(id);

            ctx.Users.Remove(user);

            await ctx.SaveChangesAsync();
        }
    }
}
