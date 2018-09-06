using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SnipeLinksGenerator.Services.Core;
using SnipeLinksGenerator.Services.PoeNinja;
using SnipeLinksGenerator.Services.PoeTrade;
using SnipeLinksGenerator.Services.Sniper;
using SnipeLinksGenerator.Services.Sniper.Models;

namespace SnipeLinksGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-Us");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", false, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var cardsService = scope.ServiceProvider.GetService<LinksService>();
                var settings = scope.ServiceProvider.GetService<IOptions<Settings>>().Value;

                var inputs = new Dictionary<string, string>();

                var searchList = JObject.Parse(File.ReadAllText($@"input.json"))["cards"].ToObject<CardSearch[]>();

                foreach (var entry in searchList)
                {
                    var result = cardsService.GenerateLinks(entry);
                    foreach (var item in result)
                    {
                        inputs.Add(item.Item1, item.Item2);
                    }
                }

                if (!Directory.Exists(settings.OutputPath))
                {
                    Directory.CreateDirectory(settings.OutputPath);
                }

                using (var stream = new StreamWriter($@"{settings.OutputPath}\input.yaml"))
                {
                    foreach (var input in inputs)
                    {
                        stream.WriteLine($"{input.Key} : \"{input.Value}\"");
                    }
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(cfg => cfg.AddConsole());
            services.AddOptions();
            services.Configure<Settings>(configuration);
            services.Configure<NinjaOptions>(configuration.GetSection("PoeNinja"));
            services.Configure<TradeOptions>(configuration.GetSection("PoeTrade"));
            services.AddHttpClient<NinjaApiClient>();
            services.AddSingleton<NinjaService>();
            services.AddHttpClient<TradeApiClient>();
            services.AddScoped<LinksService>();
        }
    }
}
