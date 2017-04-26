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
            // read in the config from consul
            Data = LoadData().Result;
        }

        private async Task<IDictionary<string, string>> LoadData()
        {
            using (var client = new ConsulClient((config) => config.Address = new Uri(_consulUri)))
            {
                var keys = GetKeys(client);
                var kvs = await GetValues(client, keys.Response);

                if (kvs.Length > 0) {
                    return ToDictionary(kvs);
                }
            }

            // TODO : log there where no config values to load
            return null;
        }

        private QueryResult<string[]> GetKeys(ConsulClient client)
        {
            return client.KV.Keys("/").Result;
        }

        private Task<QueryResult<KVPair>[]> GetValues(ConsulClient client, string[] keys) 
        {
            return Task.WhenAll(keys.Select(key => client.KV.Get(key)));
        }

        private IDictionary<string, string> ToDictionary(QueryResult<KVPair>[] kvs)
        {
            var output = new Dictionary<string, string>();
            foreach(var kv in kvs) {
                output.Add(kv.Response.Key.Replace("/", ":"), Encoding.UTF8.GetString(kv.Response.Value));
            }
            return output;
        }

    }

}