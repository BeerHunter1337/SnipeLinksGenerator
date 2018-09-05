using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnipeLinksGenerator.Services.PoeNinja;

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
                var repo = scope.ServiceProvider.GetService<NinjaRepository>();
                var exalt = repo.Currencies.Single(c => c.Name == "Exalted Orb");

                var rate = repo.Rates.Single(r => r.Receive.GetCurrencyId == exalt.Id);
            }
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(cfg => cfg.AddConsole());
            services.AddOptions();
            services.Configure<NinjaOptions>(configuration.GetSection("PoeNinja"));
            services.AddHttpClient<NinjaApiClient>();
            services.AddSingleton<NinjaRepository>();
        }
    }
}
