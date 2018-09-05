using System.ComponentModel;

namespace SnipeLinksGenerator.Services.PoeTrade.Models
{
    public enum Rarity
    {
        [DisplayName("normal")] Normal,
        [DisplayName("magic")] Magic,
        [DisplayName("rare")] Rare,
        [DisplayName("unique")] Unique,
        [DisplayName("relic")] Relic,
        [DisplayName("non-unique")] NonUnique
    }
}
