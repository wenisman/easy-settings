using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EasySettings.CustomConfiguration
{
    public class Json {

        private readonly string _environment;
        private readonly string _path;

        public Json(string environment) {
            _environment = environment;
            _path = Directory.GetCurrentDirectory();
        }

        public Json(string environment, string configPath) : this(environment)
        {
            _path = configPath;
        }

        private string GetSettingsFileName() {
            return $"{_environment}.config.json";
        }

        private string GetSecretsFileName() {
            return $"{_environment}.secrets.json";
        }

        public IConfigurationBuilder AddJsonFiles(IConfigurationBuilder builder) 
        {
            builder
                .SetBasePath(_path)
                .AddJsonFile(GetSettingsFileName(), false, true)
                .AddJsonFile(GetSecretsFileName(), true, true);

            return builder;
        }

    }
}