using Microsoft.Extensions.Configuration;

namespace Common.Configuration
{
    public class JwtSettings
    {
        private readonly IConfigurationSection _configurationSection;

        public JwtSettings(IConfigurationRoot root)
        {
            _configurationSection = root.GetSection("Jwt");
        }

        /// <summary>
        /// Key for jwt token
        /// </summary>
        public string JwtKey =>
            _configurationSection.GetSection("JwtKey").Value;

        /// <summary>
        /// Jwt issuer
        /// </summary>
        public string JwtIssuer =>
            _configurationSection.GetSection("JwtIssuer").Value;

        /// <summary>
        /// Days until expiration of jwt token
        /// </summary>
        public string JwtExpireDays =>
            _configurationSection.GetSection("JwtExpireDays").Value;
    }
}
