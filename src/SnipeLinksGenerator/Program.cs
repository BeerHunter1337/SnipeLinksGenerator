using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
                var profit = 18m;
                var cardsService = scope.ServiceProvider.GetService<CardsService>();
                var settings = scope.ServiceProvider.GetService<IOptions<Settings>>().Value;

                var inputs = new Dictionary<string, string>();

                var searchList = new List<CardEntry>
                {
                    //new CardEntry
                    //{
                    //    Name = "Abandoned Wealth",
                    //    StackSize = 5,
                    //    ProduceType = ItemType.Currency,
                    //    ProduceName = "Exalted Orb",
                    //    ProduceCount = 3,
                    //    Profit = profit
                    //},
                    new CardEntry
                    {
                        Name = "The Hoarder",
                        StackSize = 12,
                        ProduceType = ItemType.Currency,
                        ProduceName = "Exalted Orb",
                        ProduceCount = 1,
                        Profit = profit
                    },
                    //new CardEntry
                    //{
                    //    Name = "The Sephirot",
                    //    StackSize = 11,
                    //    ProduceType = ItemType.Currency,
                    //    ProduceName = "Divine Orb",
                    //    ProduceCount = 10,
                    //    Profit = profit
                    //},
                    //new CardEntry
                    //{
                    //    Name = "The Saint's Treasure",
                    //    StackSize = 10,
                    //    ProduceType = ItemType.Currency,
                    //    ProduceName = "Exalted Orb",
                    //    ProduceCount = 2,
                    //    Profit = profit
                    //}
                };

                foreach (var entry in searchList)
                {
                    var result = cardsService.GenerateLinks(entry);
                    foreach (var item in result)
                    {
                        inputs.Add(item.Item1, item.Item2);
                    }
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
            services.AddSingleton<NinjaRepository>();
            services.AddHttpClient<TradeApiClient>();
            services.AddScoped<CardsService>();
        }
    }
}
