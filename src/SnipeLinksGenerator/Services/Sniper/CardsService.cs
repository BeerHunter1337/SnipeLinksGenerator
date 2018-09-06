using System;
using System.Collections.Generic;
using System.Linq;
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

            decimal itemValue;

            switch (entry.ProduceType)
            {
                case ItemType.Currency:
                    var cur = _repository.Currencies.Single(c => c.Name == entry.ProduceName);
                    itemValue = _repository.GetRateInChaos((Currency)cur.Id);
                    break;
                case ItemType.Unique:
                    itemValue = _repository.Items
                        .Where(i => (i.Name == entry.ProduceName) && (i.Links == 0) && !i.Icon.EndsWith("&relic=1"))
                        .OrderByDescending(i => i.Count).First()
                        .ChaosValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entry));
            }

            var rawCardValue = (itemValue * entry.ProduceCount) / entry.StackSize;
            var buyOutMax = rawCardValue - ((rawCardValue * entry.Profit) / 100);

            foreach (var currency in entry.Currencies)
            {
                var buyout = Math.Round(buyOutMax / _repository.GetRateInChaos(currency), 1);
                var query = new Query
                {
                    Name = entry.Name,
                    BuyoutMax = buyout,
                    BuyoutCurrency = currency,
                    Online = true,
                    ExactCurrency = true
                };

                var text = $"[DC] {entry.Name}, {buyout:#.#}{currency.ToString()[0]}";
                queries.Add(text, query);
            }

            return queries.Select(q => (_client.GetSearchLink(q.Value).Result, q.Key)).ToArray();
        }
    }
}
