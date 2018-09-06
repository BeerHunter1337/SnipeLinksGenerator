using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SnipeLinksGenerator.Services.Core;
using SnipeLinksGenerator.Services.PoeNinja.Models;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaRepository
    {
        private readonly NinjaApiClient _client;
        private readonly List<Item> _items = new List<Item>();
        private readonly NinjaOptions _options;
        private List<Currency> _currencies;
        private List<CurrencyRate> _rates;

        public NinjaRepository(NinjaApiClient client, IOptions<NinjaOptions> options)
        {
            _client = client;
            _options = options.Value;
            LoadData().Wait();
        }

        public IEnumerable<CurrencyRate> Rates => _rates;

        public IEnumerable<Currency> Currencies => _currencies;

        public IEnumerable<Item> Items => _items;

        private async Task LoadData()
        {
            await _client.GetData();

            var currency = JObject.Parse(File.ReadAllText($@"{Constants.DataDir}\{_options.CurrencyEndpoint.Name}.json"));
            _rates = currency["lines"].ToObject<List<CurrencyRate>>();
            _currencies = currency["currencyDetails"].ToObject<List<Currency>>();

            foreach (var endpoint in _options.Endpoints)
            {
                var content = JObject.Parse(File.ReadAllText($@"{Constants.DataDir}\{endpoint.Name}.json"));
                _items.AddRange(content["lines"].ToObject<Item[]>());
            }
        }

        public decimal GetRate(PoeTrade.Models.Currency from, PoeTrade.Models.Currency to) => GetRateInChaos(from) / GetRateInChaos(to);

        public decimal GetRateInChaos(PoeTrade.Models.Currency currency)
        {
            if (currency == PoeTrade.Models.Currency.Chaos)
            {
                return 1;
            }

            var rate = _rates.Single(r => r.CurrencyTypeName == _currencies.Single(c => c.Id == (int)currency).Name);
            return 1 / rate.Pay?.Value ?? rate.Receive.Value;
        }
    }
}
