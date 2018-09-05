using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SnipeLinksGenerator.Services.PoeNinja.Models;

namespace SnipeLinksGenerator.Services.PoeNinja
{
    public class NinjaRepository
    {
        private readonly NinjaApiClient _client;
        private List<Item> _cards;
        private List<Currency> _currencies;
        private List<CurrencyRate> _rates;

        public NinjaRepository(NinjaApiClient client)
        {
            _client = client;
            LoadData().Wait();
        }

        public IEnumerable<CurrencyRate> Rates => _rates;

        public IEnumerable<Currency> Currencies => _currencies;

        public IEnumerable<Item> Cards => _cards;

        private async Task LoadData()
        {
            await _client.GetData();

            var currency = JObject.Parse(File.ReadAllText("currency.json"));
            _rates = currency["lines"].ToObject<List<CurrencyRate>>();
            _currencies = currency["currencyDetails"].ToObject<List<Currency>>();

            var cards = JObject.Parse(File.ReadAllText("currency.json"));
            _cards = cards["lines"].ToObject<List<Item>>();
        }
    }
}
