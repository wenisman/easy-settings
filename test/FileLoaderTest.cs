using System.Linq;
using Microsoft.Extensions.Configuration;
using Xunit;

using EasySettings;

namespace test
{
    public class FileLoaderTest
    {
        private readonly IConfiguration _configuration;

        public FileLoaderTest() {
            var loader = new FileLoader("first");
            var builder = new ConfigurationBuilder();
            loader.AddJsonFiles(builder);

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
            Assert.Equal("unseen one", _configuration["secretOne"]);
        }
    }
}
