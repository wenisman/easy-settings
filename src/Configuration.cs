using System;

namespace Platform.EasySettings
{
    public class Configuration
    {
        private readonly string _environment;
        private readonly ConsulLoader _consulLoader;

        public Configuration(string userEnv) 
        {
            _environment = GetEnvironment(userEnv);
            _consulLoader = new ConsulLoader(_environment);
        }

        private string GetEnvironment(string userEnv) {
            var environment = Environment.GetEnvironmentVariable("Environment");
            if (string.IsNullOrEmpty(environment)) {
                environment = Environment.GetEnvironmentVariable("WHA_ENV");
            }

            if (string.IsNullOrEmpty(environment)) {
                environment = userEnv;
            }

            return environment;
        }


        public void Load() 
        {
            
        }


        private string ReadConsul()
        {
            return string.Empty;
        }
    }
}
