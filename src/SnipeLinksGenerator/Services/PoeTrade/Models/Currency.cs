using System.ComponentModel;

namespace SnipeLinksGenerator.Services.PoeTrade.Models
{
    public enum Currency
    {
        [DisplayName("chaos")] Chaos = 1,
        [DisplayName("exalted")] Exalted,
        [DisplayName("divine")] Divine,
        [DisplayName("alchemy")] Alchemy,
        [DisplayName("fusing")] Fusing,
        [DisplayName("alteration")] Alteration,
        [DisplayName("regal")] Regal,
        [DisplayName("vaal")] Vaal,
        [DisplayName("regret")] Regret,
        [DisplayName("chisel")] Chisel,
        [DisplayName("jewellers")] Jewellers,
        [DisplayName("silver")] Silver,
        [DisplayName("coin")] Coin,
        [DisplayName("scouring")] Scouring,
        [DisplayName("gcp")] Gcp,
        [DisplayName("chance")] Chance,
        [DisplayName("chromatic")] Chromatic,
        [DisplayName("blessed")] Blessed
    }
}
