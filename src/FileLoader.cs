using System.IO;
using Microsoft.Extensions.Configuration;

namespace EasySettings
{
    public class FileLoader {

        private readonly string _environment;
        private readonly string _path;

        public FileLoader(string environment) {
            _environment = environment;
            
            _path = Directory.GetCurrentDirectory();
        }

        public FileLoader(string environment, string configPath) : this(environment)
        {
            _path = configPath;
        }

        private string GetSettingsFileName() {
            return $"{_environment}.config.json";
        }

        private string GetSecretsFileName() {
            return $"{_environment}.secrets.json";
        }

        public ConfigurationBuilder AddJsonFiles(ConfigurationBuilder builder) 
        {
            builder
                .SetBasePath(_path)
                .AddJsonFile(GetSettingsFileName(), false, true)
                .AddJsonFile(GetSecretsFileName(), true, true);

            return builder;
        }

    }
}