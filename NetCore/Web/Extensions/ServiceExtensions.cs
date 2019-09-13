using Common.Configuration;
using Core.Auth.Command;
using Core.Common;
using Core.Users.Command;
using Domain;
using Domain.Model;
using Filters;
using Web.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NetCore;
using Npgsql;

namespace Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<DatabaseContext>();
            services.AddScoped<DatabaseInitializer>();
            services.AddScoped<DatabaseSeed>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<LoginCommandValidator>();
            services.AddScoped<RegisterCommandValidator>();
            services.AddScoped<UserService>();
            services.AddScoped<EnumsService>();

            return services;
        }

        public static IServiceCollection RegisterJwt(
            this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = AppSettings.JwtSettings.JwtIssuer,
                        ValidAudience = AppSettings.JwtSettings.JwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSettings.JwtKey)),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            return services;
        }

        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerDocument(settings =>
            {
                settings.SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                settings.Title = "NetCore";
            });

            return services;
        }

        public static IServiceCollection AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            return services;
        }

        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            IMvcBuilder builder = services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(GlobalExceptionFilter));
                opt.Filters.Add(typeof(LanguageFilter));
                opt.Filters.Add(typeof(AuthenticationFilter));
                opt.Filters.Add(typeof(LogFilter));
            });

            builder.AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            builder.AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                cfg.ImplicitlyValidateChildProperties = true;
            });

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            // services.AddDbContext<DatabaseContext>(options =>
            //     options
            //     .UseSqlServer(connectionString, optionsAction => optionsAction.EnableRetryOnFailure()));

            var builder = new NpgsqlConnectionStringBuilder(connectionString);

            services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(builder.ConnectionString, optionsAction => optionsAction.EnableRetryOnFailure()));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
