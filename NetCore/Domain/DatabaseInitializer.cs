using Domain.Constants;
using Domain.Model;
using System.Linq;

namespace Domain
{
    /// <summary>
    /// Populates db with data that must exist for app to work properly
    /// </summary>
    public class DatabaseInitializer
    {
        private DatabaseContext ctx;

        public DatabaseInitializer(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public void Initialize()
        {
            InsertRoles();

            ctx.SaveChanges();
        }

        private void InsertSomethingFromSql()
        {
            SeedScripts.SeedANSI("InsertUsers.sql", ctx);
        }

        private void InsertRoles()
        {
            if (ctx.Roles.Count() == 0)
            {
                ctx.Roles.Add(new Role
                {
                    Name = Roles.Admin,
                    NormalizedName = Roles.Admin.ToUpper()
                });

                ctx.Roles.Add(new Role
                {
                    Name = Roles.User,
                    NormalizedName = Roles.User.ToUpper()
                });
            }
        }
    }
}
