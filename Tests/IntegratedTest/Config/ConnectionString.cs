using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace IntegratedTest.Config
{

    [ExcludeFromCodeCoverage]
    public static class ConnectionString
    {
        public static IConfiguration GetConnection()
        {
            var appsettings = "appsettings.Test.json";
            var directory = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile(appsettings)
                .AddEnvironmentVariables()
                .Build();

            return config;
        }     
    }
}
