using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace Platform.EasySettings
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

        public IDictionary<string, string> Load()
        {
            var json = ReadFile();
            return ParseJson(json);
        }

        private string GetFileName() {
            return $"{_environment}.config.json";
        }

        private string ReadFile() 
        {
            var path = Path.Combine(_path, GetFileName());
            var text = string.Empty;
            Console.WriteLine("path: " + path);

            if (File.Exists(path)) {
                using (TextReader reader = File.OpenText(path)) {
                    text = reader.ReadToEnd();
                }
            } else {
                // TODO : log that the file could not be found
            }

            return text;
        }


        private IDictionary<string, string> ParseJson(string json)
        {
            var config = JObject.Parse(json);
            return FlatMapJson(config, string.Empty);
        }

        private IDictionary<string, string> FlatMapJson(JObject child, string parentpath)
        {
            var map = new Dictionary<string, string>();

            foreach(var item in child) {
                var path = string.IsNullOrEmpty(parentpath) ? item.Key : $"{parentpath}:{item.Key}";
                if (item.Value.Type == JTokenType.Object) {
                    // iterate and flatten
                    map = (Dictionary<string, string>)JoinDictionaries(map, FlatMapJson((JObject)item.Value, path));
                } else {
                    map.Add(path, (string)item.Value);
                }
            }

            return map;
        }


        private IDictionary<string, string> JoinDictionaries(Dictionary<string, string> first, IDictionary<string, string> second)
        {
            return first.Concat(second).GroupBy(d => d.Key).ToDictionary(d => d.Key, d => d.First().Value);
        }
    }
}