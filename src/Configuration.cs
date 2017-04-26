using System;
using System.IO;
using Microsoft.Extensions.Configuration;

using EasySettings.CustomConfiguration;

namespace EasySettings
{
    public class Configuration
    {
        private readonly string _environment;

        public Configuration(string environmentVar = "Environment") 
        {
            _environment = GetEnvironment(environmentVar);
        }

        public Configuration(string configFilePath, string environmentVar = "Environment")
        {
            _environment = GetEnvironment(environmentVar);
        }

        private string GetEnvironment(string environmentVar = "Environment") {
            var environment = Environment.GetEnvironmentVariable("Environment");
            if (string.IsNullOrEmpty(environment)) {
                // TODO : log this error
                environment = string.Empty;
            }

            return environment;
        }


        public IConfiguration Load() 
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder
                .AddJsonConfig(_environment)
                .AddConsulConfig(_environment);

            return builder.Build();
        }
    }
}
