using System;
using System.Collections.Generic;
using System.Linq;
using SnipeLinksGenerator.Services.PoeNinja;
using SnipeLinksGenerator.Services.PoeTrade;
using SnipeLinksGenerator.Services.PoeTrade.Models;
using SnipeLinksGenerator.Services.Sniper.Models;

namespace SnipeLinksGenerator.Services.Sniper
{
    public class LinksService
    {
        private readonly TradeApiClient _client;
        private readonly NinjaService _ninja;

        public LinksService(NinjaService ninja, TradeApiClient client)
        {
            _ninja = ninja;
            _client = client;
        }

        public (string, string)[] GenerateLinks(CardSearch search)
        {
            var queries = new Dictionary<string, Query>();

            decimal itemValue;

            var card = _ninja.Items.Single(i => i.Name == search.Name);

            switch (search.ProduceType)
            {
                case ItemType.Currency:
                    var cur = _ninja.Currencies.Single(c => c.Name == search.ProduceName);
                    itemValue = _ninja.GetRateInChaos((Currency)cur.Id);
                    break;
                case ItemType.Unique:
                    itemValue = _ninja.Items
                        .Where(i => (i.Name == search.ProduceName) && (i.Links == 0) && !i.Icon.EndsWith("&relic=1"))
                        .OrderByDescending(i => i.Count).First()
                        .ChaosValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(search));
            }

            var rawCardValue = itemValue * search.ProduceCount / card.StackSize;
            var buyOutMax = rawCardValue - (rawCardValue * search.Profit / 100);

            foreach (var currency in search.Currencies)
            {
                var rate = _ninja.GetRateInChaos(currency);
                var convertedValue = rawCardValue / rate;
                var buyout = Math.Round(buyOutMax / rate, 0);
                var query = new Query
                {
                    Name = search.Name,
                    BuyoutMax = buyout,
                    BuyoutCurrency = currency,
                    Online = true,
                    ExactCurrency = true
                };

                var curSymbol = currency.ToString().ToLower()[0];
                var text = $"[DC] {search.Name} < {buyout}{curSymbol}, {convertedValue:#.#}{curSymbol} worth";
                queries.Add(text, query);
            }

            return queries.Select(q => (_client.GetSearchLink(q.Value).Result, q.Key)).ToArray();
        }
    }
}
