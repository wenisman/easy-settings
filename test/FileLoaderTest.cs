using Microsoft.Extensions.Configuration;
using Xunit;

using EasySettings.CustomConfiguration;

namespace test
{
    public class FileLoaderTest
    {
        private readonly IConfiguration _configuration;

        public FileLoaderTest() {
            var builder = new ConfigurationBuilder();
            builder.AddJsonConfig("first");

            _configuration = builder.Build();
        }

        [Fact]
        public void should_load_settings_file()
        {
            Assert.Equal("valueOne", _configuration["keyOne"]);
            Assert.Equal("nested value one", _configuration["keyTwo:nestOne"]);
        }

        [Fact]
        public void should_read_secets()
        {
            Assert.Equal("UnseenOne", _configuration["secretOne"]);
        }
    }
}
