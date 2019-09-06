using AutoMapper;
using Common.Configuration;
using Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag.AspNetCore;

namespace NetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public static IConfiguration Configuration { get; set; }

        public static IHostingEnvironment HostingEnvironment { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcServices();
            services.AddCorsServices();
            services.AddDatabase(AppSettings.ConnectionString);

            services.AddAutoMapper();
            services.AddLocalization();
            services.RegisterJwt();
            services.RegisterSwagger();

            services.RegisterApplicationServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(c => c.SwaggerRoutes.Add(new SwaggerUi3Route("Service API V1", "/swagger/v1/swagger.json")));

            app.UseCors("AllowAll");

            app.UseMiddleware<StackifyMiddleware.RequestTracerMiddleware>();

            app.UseMvc();

            app.MigrateDatabase();         
        }
    }
}
