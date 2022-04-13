using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaApi.Models
{
    public class Settings
    {
        private static Settings _appConfig;
        public string KeySecret { get; set; }
        public int Expires { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public Settings(IConfiguration config)
        {
            this.KeySecret = config.GetValue<string>("KeySecret");
            this.Expires = config.GetValue<int>("Expires");
            this.Issuer = config.GetValue<string>("Issuer");
            this.Audience = config.GetValue<string>("Audience");

            _appConfig = this;
        }

        public static Settings Current
        {
            get
            {
                if (_appConfig == null)
                {
                    _appConfig = GetCurrentSettings();
                }

                return _appConfig;
            }
        }
        public static Settings GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();

            var settings = new Settings(configuration.GetSection("AppSettings"));
            return settings;
        }
    }
}
