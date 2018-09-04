using System;
using Newtonsoft.Json;

namespace SnipeLinksGenerator.Services.PoeNinja.Models
{
    public class Amount
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "league_id")]
        public int LeagueId { get; set; }

        [JsonProperty(PropertyName = "pay_currency_id")]
        public int PayCurrencyId { get; set; }

        [JsonProperty(PropertyName = "get_currency_id")]
        public int GetCurrencyId { get; set; }

        [JsonProperty(PropertyName = "sample_time_utc")]
        public DateTime SampleTimeUtc { get; set; }

        public int Count { get; set; }

        public decimal Value { get; set; }

        [JsonProperty(PropertyName = "data_point_count")]
        public int DataPointCount { get; set; }

        [JsonProperty(PropertyName = "includes_secondary")]
        public bool IncludesSecondary { get; set; }
    }
}
