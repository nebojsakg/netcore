using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Configuration
{
    public class LoggerSettings
    {
        private readonly IConfigurationSection _configurationSection;

        public LoggerSettings(IConfigurationRoot root)
        {
            _configurationSection = root.GetSection("Logger");
        }

        /// <summary>
        /// Key for jwt token
        /// </summary>
        public string InfoPath =>
            _configurationSection.GetSection("InfoPath").Value;

        /// <summary>
        /// Jwt issuer
        /// </summary>
        public string ErrorPath =>
            _configurationSection.GetSection("ErrorPath").Value;
    }
}
