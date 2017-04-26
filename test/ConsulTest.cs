using System.Linq;
using Microsoft.Extensions.Configuration;
using Xunit;

using EasySettings;
using EasySettings.CustomConfiguration;

namespace test
{
    public class ConsulTest
    {
        private readonly IConfiguration _configuration;

        public ConsulTest() 
        {
            var builder = new ConfigurationBuilder();
            builder.AddConsulConfig(null);

            _configuration = builder.Build();
        }


        [Fact]
        public void should_read_sample_kv()
        {
            Assert.Equal("test", _configuration["sample2"]);
        }
    }
}