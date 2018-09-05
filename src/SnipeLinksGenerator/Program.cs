using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnipeLinksGenerator.Services.PoeNinja;
using SnipeLinksGenerator.Services.PoeTrade;
using SnipeLinksGenerator.Services.PoeTrade.Models;

namespace SnipeLinksGenerator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
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
                //var repo = scope.ServiceProvider.GetService<NinjaRepository>();
                //var exalt = repo.Currencies.Single(c => c.Name == "Exalted Orb");

                //var rate = repo.Rates.Single(r => r.Receive.GetCurrencyId == exalt.Id);

                var tradeClient = scope.ServiceProvider.GetService<TradeApiClient>();
                var query = new Query { League = "Delve", Name = "Hoarder", Online = true, BuyoutMax = 100, BuyoutCurrency = Currency.Chaos };
                var link = tradeClient.GetSearchLink(query).Result;
            }
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(cfg => cfg.AddConsole());
            services.AddOptions();
            services.Configure<NinjaOptions>(configuration.GetSection("PoeNinja"));
            services.Configure<TradeOptions>(configuration.GetSection("PoeTrade"));
            services.AddHttpClient<NinjaApiClient>();
            services.AddSingleton<NinjaRepository>();
            services.AddHttpClient<TradeApiClient>();
        }
    }
}
