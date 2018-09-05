using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly NinjaOptions _options;

        public NinjaApiClient(IOptions<NinjaOptions> options, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task GetData()
        {
            foreach (var endpoint in _options.Endpoints)
            {
                var response = await _httpClient.GetAsync($"{endpoint.Value}?league={_options.League}");
                response.EnsureSuccessStatusCode();

                using (var stream = File.Open($"{endpoint.Key}.json", FileMode.Create, FileAccess.ReadWrite))
                {
                    await stream.WriteAsync(await response.Content.ReadAsByteArrayAsync());
                }
            }
        }
    }
}
