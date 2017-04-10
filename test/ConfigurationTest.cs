using System;
using Microsoft.Extensions.Configuration;
using Xunit;

using EasySettings;

namespace test
{
    public class ConfigurationTest
    {

        private readonly Configuration _config;
        public ConfigurationTest()
        {
            Environment.SetEnvironmentVariable("Environment", "first");
            _config = new Configuration("first");
        }

    }
}