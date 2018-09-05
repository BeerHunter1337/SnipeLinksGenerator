using System;
using System.Linq;
using System.Threading.Tasks;
using SnipeLinksGenerator.Services.PoeNinja;
using SnipeLinksGenerator.Services.PoeTrade;
using SnipeLinksGenerator.Services.PoeTrade.Models;
using SnipeLinksGenerator.Services.Sniper.Models;

namespace SnipeLinksGenerator.Services.Sniper
{
    public class CardsService
    {
        private readonly TradeApiClient _client;
        private readonly NinjaRepository _repository;

        public CardsService(NinjaRepository repository, TradeApiClient client)
        {
            _repository = repository;
            _client = client;
        }

        public async Task<(string, string)> GenerateLink(CardEntry entry)
        {
            Query query = null;
            string text = null;
            switch (entry.ProduceType)
            {
                case ItemType.Currency:
                    var rate = _repository.Rates.Single(r => r.CurrencyTypeName == entry.ProduceName);
                    var rawCardValue = (1 / rate.Pay.Value * entry.ProduceCount) / entry.StackSize;
                    query = new Query
                    {
                        Name = entry.Name,
                        BuyoutMax = Math.Round(rawCardValue - ((rawCardValue * entry.Profit) / 100), 1),
                        BuyoutCurrency = Currency.Chaos,
                        Online = true
                    };
                    text = $"[DC] {entry.Name}, {rawCardValue:#.#}c";
                    break;
                case ItemType.Unique:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return (await _client.GetSearchLink(query), text);
        }
    }
}
