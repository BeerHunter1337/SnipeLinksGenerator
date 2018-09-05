using System.ComponentModel;

namespace SnipeLinksGenerator.Services.PoeTrade.Models
{
    public class Query
    {
        [DisplayName("league")]
        public string League { get; set; }

        [DisplayName("type")]
        public string Type { get; set; }

        [DisplayName("base")]
        public string Base { get; set; }

        [DisplayName("name")]
        public string Name { get; set; }

        [DisplayName("dmg_min")]
        public decimal? DmgMin { get; set; }

        [DisplayName("dmg_max")]
        public decimal? DmgMax { get; set; }

        [DisplayName("aps_min")]
        public decimal? ApsMin { get; set; }

        [DisplayName("aps_max")]
        public decimal? ApsMax { get; set; }

        [DisplayName("crit_min")]
        public decimal? CritMin { get; set; }

        [DisplayName("crit_max")]
        public decimal? CritMax { get; set; }

        [DisplayName("dps_min")]
        public decimal? DpsMin { get; set; }

        [DisplayName("dps_max")]
        public decimal? DpsMax { get; set; }

        [DisplayName("edps_min")]
        public decimal? EdpsMin { get; set; }

        [DisplayName("edps_max")]
        public decimal? EdpsMax { get; set; }

        [DisplayName("pdps_min")]
        public decimal? PdpsMin { get; set; }

        [DisplayName("pdps_max")]
        public decimal? PdpsMax { get; set; }

        [DisplayName("armour_min")]
        public decimal? ArmourMin { get; set; }

        [DisplayName("armour_max")]
        public decimal? ArmourMax { get; set; }

        [DisplayName("evasion_min")]
        public decimal? EvasionMin { get; set; }

        [DisplayName("evasion_max")]
        public decimal? EvasionMax { get; set; }

        [DisplayName("shield_min")]
        public decimal? ShieldMin { get; set; }

        [DisplayName("shield_max")]
        public decimal? ShieldMax { get; set; }

        [DisplayName("block_min")]
        public decimal? BlockMin { get; set; }

        [DisplayName("block_max")]
        public decimal? BlockMax { get; set; }

        [DisplayName("sockets_min")]
        public int? SocketsMin { get; set; }

        [DisplayName("sockets_max")]
        public int? SocketsMax { get; set; }

        [DisplayName("link_min")]
        public int? LinkMin { get; set; }

        [DisplayName("link_max")]
        public int? LinkMax { get; set; }

        [DisplayName("sockets_r")]
        public int? SocketsR { get; set; }

        [DisplayName("sockets_g")]
        public int? SocketsG { get; set; }

        [DisplayName("sockets_b")]
        public int? SocketsB { get; set; }

        [DisplayName("sockets_w")]
        public int? SocketsW { get; set; }

        [DisplayName("linked_r")]
        public int? LinkedR { get; set; }

        [DisplayName("linked_g")]
        public int? LinkedG { get; set; }

        [DisplayName("linked_b")]
        public int? LinkedB { get; set; }

        [DisplayName("linked_w")]
        public int? LinkedW { get; set; }

        [DisplayName("rlevel_min")]
        public int? RlevelMin { get; set; }

        [DisplayName("rlevel_max")]
        public int? RlevelMax { get; set; }

        [DisplayName("rstr_min")]
        public int? RstrMin { get; set; }

        [DisplayName("rstr_max")]
        public int? RstrMax { get; set; }

        [DisplayName("rdex_min")]
        public int? RdexMin { get; set; }

        [DisplayName("rdex_max")]
        public int? RdexMax { get; set; }

        [DisplayName("rint?_min")]
        public int? RintMin { get; set; }

        [DisplayName("rint?_max")]
        public int? RintMax { get; set; }

        public Group[] Groups { get; set; }

        [DisplayName("q_min")]
        public int? QMin { get; set; }

        [DisplayName("q_max")]
        public int? QMax { get; set; }

        [DisplayName("level_min")]
        public int? LevelMin { get; set; }

        [DisplayName("level_max")]
        public int? LevelMax { get; set; }

        [DisplayName("ilvl_min")]
        public int? IlvlMin { get; set; }

        [DisplayName("ilvl_max")]
        public int? IlvlMax { get; set; }

        [DisplayName("rarity")]
        public Rarity? Rarity { get; set; }

        [DisplayName("seller")]
        public string Seller { get; set; }

        [DisplayName("thread")]
        public string Thread { get; set; }

        [DisplayName("identified")]
        public bool? Identified { get; set; }

        [DisplayName("corrupted")]
        public bool? Corrupted { get; set; }

        [DisplayName("progress_min")]
        public decimal? ProgressMin { get; set; }

        [DisplayName("progress_max")]
        public decimal? ProgressMax { get; set; }

        [DisplayName("sockets_a_min")]
        public int? SocketsAMin { get; set; }

        [DisplayName("sockets_a_max")]
        public int? SocketsAMax { get; set; }

        [DisplayName("shaper")]
        public bool? Shaper { get; set; }

        [DisplayName("elder")]
        public bool? Elder { get; set; }

        [DisplayName("map_series")]
        public MapSeries? MapSeries { get; set; }

        [DisplayName("crafted")]
        public bool? Crafted { get; set; }

        [DisplayName("enchanted")]
        public bool? Enchanted { get; set; }

        [DisplayName("online")]
        public bool Online { get; set; }

        [DisplayName("altart")]
        public bool Altart { get; set; }

        [DisplayName("capquality")]
        public bool Capquality { get; set; }

        [DisplayName("buyout_min")]
        public decimal? BuyoutMin { get; set; }

        [DisplayName("buyout_max")]
        public decimal? BuyoutMax { get; set; }

        [DisplayName("buyout_currency")]
        public Currency? BuyoutCurrency { get; set; }

        [DisplayName("has_buyout")]
        public bool? HasBuyout { get; set; }

        [DisplayName("exact_currency")]
        public bool ExactCurrency { get; set; }
    }
}
