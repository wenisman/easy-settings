using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Consul;

namespace EasySettings.CustomConfiguration
{


    public class ConsulConfigSource : IConfigurationSource
    {
        private readonly string _environment;

        public ConsulConfigSource(string environment) 
        {
            _environment = environment;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigProvider(_environment, this);
        }
    }

    public class ConsulConfigProvider : ConfigurationProvider
    {
        private IConfigurationSource _source;
        private readonly string _consulUri;
        public ConsulConfigProvider(string environment, ConsulConfigSource source) : base() 
        { 
            _source = source;
            if (string.IsNullOrEmpty(environment)) {
                _consulUri = "http://localhost:8500";
            } else {
                _consulUri = $"http://service-discovery.{environment}.williamhill.internal:8500";
            }
        }

        public override void Load() 
        {
            try {
                // read in the config from consul
                using (var client = new ConsulClient((config) => config.Address = new Uri(_consulUri)))
                {
                    Data = LoadData(client).Result;
                }
            }
            catch(Exception ex) {
                // TODO: log exception
            }
        }

        private async Task<IDictionary<string, string>> LoadData(ConsulClient client, string path = "/")
        {
            var kvs = await client.KV.List(path);
            if (kvs.Response.Length > 0) {
                return ToDictionary(kvs.Response);
            }

            // TODO : log there where no config values to load
            return null;
        }

        private IDictionary<string, string> ToDictionary(KVPair[] kvs)
        {
            var output = new Dictionary<string, string>();
            foreach(var kv in kvs) {
                if (!kv.Key.EndsWith("/")) {
                     output.Add(kv.Key.Replace("/", ":"), Encoding.UTF8.GetString(kv.Value));
                }
            }
            return output;
        }
    }
}