using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Extensions
{
    public static class IWebHostExtensions
    {
        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory)webHost.ApplicationServices.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbContext = services.GetRequiredService<DatabaseContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var dbInitializer = services.GetRequiredService<DatabaseInitializer>();
                var dbSeed = services.GetRequiredService<DatabaseSeed>();
                var env = services.GetRequiredService<IHostingEnvironment>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                    Log.Information("Database droped and recreated.");

                    dbInitializer.Initialize();
                    Log.Information("Database initialized with required data.");

                    dbSeed.Seed(userManager);
                    Log.Information("Database seeded with test data.");
                }
                else
                {
                    dbContext.Database.Migrate();
                    Log.Information("Database migrations executed.");

                    dbInitializer.Initialize();
                    Log.Information("Database initialized with required data.");
                }

            }

            return webHost;
        }
    }
}
