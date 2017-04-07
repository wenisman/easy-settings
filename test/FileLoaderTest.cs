using System;
using Xunit;

using Platform.EasySettings;

namespace test
{
    public class FileLoaderTest
    {

        private FileLoader _loader;

        public FileLoaderTest() {
            _loader = new FileLoader("first");
        }


        [Fact]
        public void should_flatmap_json()
        {
            var output = _loader.Load();
            foreach(var item in output) {
                Console.WriteLine($"{item.Key} : {item.Value}");
            }
            Assert.True(true);
        }
    }
}
