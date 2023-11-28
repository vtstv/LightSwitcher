using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace LightSwitcher
{
    public class AppConfig
    {
        public int ServerPort { get; set; }
        public string StatusCommandEndpoint { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            var appConfig = configuration.Get<AppConfig>();

            Console.WriteLine($"ServerPort: {appConfig.ServerPort}");
            Console.WriteLine($"StatusCommandEndpoint: {appConfig.StatusCommandEndpoint}");

            CreateHostBuilder(args, appConfig).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, AppConfig appConfig) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls($"http://localhost:{appConfig.ServerPort}");
                });
    }
}
