using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Consul;

namespace EasySettings
{

    class ConsulLoader  
    {
        private readonly string _consulUri;
        public ConsulLoader(string consulUri){
            _consulUri = consulUri;
        }

        private async Task<IDictionary<string, string>> Load()
        {
            using (var client = new ConsulClient())
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
                output.Add(kv.Response.Key, Encoding.UTF8.GetString(kv.Response.Value));
            }
            return output;
        }

    }
}