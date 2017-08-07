using System.IO;
using Microsoft.Extensions.Configuration;

namespace HackThePlanetApp
{
    public static class AppConfig
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json")
        .AddJsonFile("config.json")
        .Build();
    }
}