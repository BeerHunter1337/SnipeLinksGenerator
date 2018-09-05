using System.ComponentModel;

namespace SnipeLinksGenerator.Services.PoeTrade.Models
{
    public class Group
    {
        public Modifier[] Mods { get; set; }

        [DisplayName("group_type")]
        public string GroupType { get; set; }

        [DisplayName("group_min")]
        public decimal GroupMin { get; set; }

        [DisplayName("group_max")]
        public decimal GroupMax { get; set; }

        [DisplayName("group_count")]
        public int GroupCount => Mods.Length;
    }
}
