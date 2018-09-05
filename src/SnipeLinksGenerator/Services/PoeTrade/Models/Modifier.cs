using System.ComponentModel;

namespace SnipeLinksGenerator.Services.PoeTrade.Models
{
    public class Modifier
    {
        [DisplayName("mod_name")]
        public string ModName { get; set; }

        [DisplayName("mod_min")]
        public decimal ModMin { get; set; }

        [DisplayName("mod_max")]
        public decimal ModMax { get; set; }

        [DisplayName("mod_weight")]
        public decimal ModWeight { get; set; }
    }
}
