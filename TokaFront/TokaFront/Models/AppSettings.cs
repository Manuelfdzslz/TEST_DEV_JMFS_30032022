using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokaFront.Models
{
    public class AppSettings
    {
        private static AppSettings _appConfig;
        public string ServiceUrl { get; set; }

        public AppSettings(IConfiguration config)
        {
            this.ServiceUrl = config.GetValue<string>("ServiceUrl");

            _appConfig = this;
        }

        public static AppSettings Current
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
        public static AppSettings GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();

            var settings = new AppSettings(configuration.GetSection("AppSettings"));
            return settings;
        }
    }
}
