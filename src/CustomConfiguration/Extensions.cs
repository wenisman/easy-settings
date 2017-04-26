using System;
using Microsoft.Extensions.Configuration;

namespace EasySettings.CustomConfiguration
{
    public static class ConsulConfigExtension
    {
        public static IConfigurationBuilder AddConsulConfig(this IConfigurationBuilder builder, string environment) 
        {
            return builder.Add(new ConsulConfigSource(environment));
        }

        public static IConfigurationBuilder AddJsonConfig(this IConfigurationBuilder builder, string environment)
        {
            var jsonLoader = new Json(environment);
            return jsonLoader.AddJsonFiles(builder);
        }
    }

}