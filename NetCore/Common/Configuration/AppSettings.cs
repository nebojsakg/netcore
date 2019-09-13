using Microsoft.Extensions.Configuration;
using System.IO;

namespace Common.Configuration
{
    public class AppSettings
    {
        private static readonly IConfigurationRoot _configurationRoot;

        static AppSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            configurationBuilder.AddJsonFile(path, false);

            _configurationRoot = configurationBuilder.Build();
        }

        /// <summary>
        /// Connection string for NetCore database (SqlServer)
        /// </summary>
        public static string ConnectionString =>
            _configurationRoot.GetSection("ConnectionStrings").GetSection("NetCore").Value;

        /// <summary>
        /// Connection string for NetCore database (PostgreSql)
        /// </summary>
        public static string PostgreSqlConnectionString =>
            _configurationRoot.GetSection("ConnectionStrings").GetSection("PostgreSql").Value;

        /// <summary>
        /// Serilog settings
        /// </summary>
        public static LoggerSettings LoggerSettings =>
            new LoggerSettings(_configurationRoot);

        /// <summary>
        /// Jwt settings
        /// </summary>
        public static JwtSettings JwtSettings =>
            new JwtSettings(_configurationRoot);
    }
}
