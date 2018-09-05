using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SnipeLinksGenerator.Services.Core;
using SnipeLinksGenerator.Services.PoeTrade.Models;

namespace SnipeLinksGenerator.Services.PoeTrade
{
    public class TradeApiClient
    {
        private readonly Settings _appSettings;
        private readonly HttpClient _httpClient;
        private readonly TradeOptions _options;

        public TradeApiClient(IOptions<TradeOptions> options, IOptions<Settings> appsettings, HttpClient httpClient)
        {
            _appSettings = appsettings.Value;
            _httpClient = httpClient;
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<string> GetSearchLink(Query query)
        {
            if (string.IsNullOrWhiteSpace(query.League))
            {
                query.League = _appSettings.League;
            }

            var content = new StringContent(query.ToQueryString(), Encoding.UTF8);
            var response = await _httpClient.PostAsync($"{_options.SearchEndpoint}", content);
            response.EnsureSuccessStatusCode();
            return response.RequestMessage.RequestUri.AbsoluteUri;
        }
    }
}
