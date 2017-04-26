using System;
using Microsoft.Extensions.Configuration;
using Xunit;

using EasySettings;

namespace test
{
    public class ConfigurationTest
    {

        private readonly IConfiguration _config;
        public ConfigurationTest()
        {
            Environment.SetEnvironmentVariable("Environment", "first");
            _config = new Configuration("first").Load();
        }

        [Fact]
        public void should_load_configs() 
        {
            Assert.Equal(_config["keyOne"], "valueOne");
        }
    }
}