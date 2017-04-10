using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EasySettings
{
    public class Configuration
    {
        private readonly string _environment;
        private readonly FileLoader _fileLoader;

        public Configuration(string environmentVar = "Environment") 
        {
            _environment = GetEnvironment(environmentVar);
            _fileLoader = new FileLoader(_environment);
        }

        public Configuration(string configFilePath, string environmentVar = "Environment")
        {
            _environment = GetEnvironment(environmentVar);
            _fileLoader = new FileLoader(environmentVar, configFilePath);
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
            _fileLoader.AddJsonFiles(builder);

            return builder.Build();
        }
    }
}
