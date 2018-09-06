using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SnipeLinksGenerator.Services.Core;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaApiClient
    {
        private readonly Settings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly NinjaOptions _options;

        public NinjaApiClient(IOptions<NinjaOptions> options, IOptions<Settings> appsettings, HttpClient httpClient)
        {
            _appSettings = appsettings.Value;
            _httpClient = httpClient;
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task GetData()
        {
            await SaveToFile(_options.CurrencyEndpoint.Name, _options.CurrencyEndpoint.Value);
            foreach (var endpoint in _options.Endpoints)
            {
                await SaveToFile(endpoint.Name, endpoint.Value);
            }
        }

        private async Task SaveToFile(string fileName, string endpoint)
        {
            if (!Directory.Exists(Constants.DataDir))
            {
                Directory.CreateDirectory(Constants.DataDir);
            }

            var fi = new FileInfo($@"{Constants.DataDir}\{fileName}.json");
            if (fi.Exists && (DateTime.Now - fi.LastWriteTime < TimeSpan.FromHours(1)))
            {
                return;
            }

            var response = await _httpClient.GetAsync($"{endpoint}?league={_appSettings.League}");
            response.EnsureSuccessStatusCode();

            using (var stream = File.Open($@"{Constants.DataDir}\{fileName}.json", FileMode.Create, FileAccess.ReadWrite))
            {
                await stream.WriteAsync(await response.Content.ReadAsByteArrayAsync());
            }
        }
    }
}
