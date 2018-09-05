using System;
using System.Collections.Generic;
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

        public (string, string)[] GenerateLinks(CardEntry entry)
        {
            var queries = new Dictionary<string, Query>();
            switch (entry.ProduceType)
            {
                case ItemType.Currency:
                    var rate = _repository.Rates.Single(r => r.CurrencyTypeName == entry.ProduceName);
                    var alchRate = _repository.Rates.Single(r => r.CurrencyTypeName == "Orb of Alchemy");

                    var rawCardValue = (1 / rate.Pay.Value * entry.ProduceCount) / entry.StackSize;
                    var rawCardValueAlch = rawCardValue / (1 / alchRate.Pay.Value);

                    var query = new Query
                    {
                        Name = entry.Name,
                        BuyoutMax = Math.Round(rawCardValue - ((rawCardValue * entry.Profit) / 100), 1),
                        BuyoutCurrency = Currency.Chaos,
                        Online = true,
                        ExactCurrency = true
                    };
                    var queryAlch = new Query
                    {
                        Name = entry.Name,
                        BuyoutMax = Math.Round(rawCardValueAlch - ((rawCardValueAlch * entry.Profit) / 100), 1),
                        BuyoutCurrency = Currency.Alchemy,
                        Online = true,
                        ExactCurrency = true
                    };
                    var text = $"[DC] {entry.Name}, {rawCardValue:#.#}c";
                    var textAlch = $"[DC] {entry.Name}, {rawCardValueAlch:#.#}a";
                    queries.Add(text, query);
                    queries.Add(textAlch, queryAlch);
                    break;
                case ItemType.Unique:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return queries.Select(q => (_client.GetSearchLink(q.Value).Result, q.Key)).ToArray();
        }
    }
}
