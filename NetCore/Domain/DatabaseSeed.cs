using Domain.Constants;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Domain
{
    /// <summary>
    /// Populets db with some data for testing
    /// </summary>
    public class DatabaseSeed
    {
        private DatabaseContext ctx;

        public DatabaseSeed(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public void Seed(UserManager<User> userManager)
        {
            IdentityResult result = userManager.CreateAsync(new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
            }, "Admin123!").Result;

            var adminUser = ctx.Users.First(u => u.Email == "admin@gmail.com");
            var adminRole = ctx.Roles.First(u => u.Name == Roles.Admin);

            ctx.UserRoles.Add(new UserRole
            {
                UserId = adminUser.Id,
                RoleId = adminRole.Id
            });

            ctx.SaveChanges();
        }
    }
}
