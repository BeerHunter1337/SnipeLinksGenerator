namespace SnipeLinksGenerator.Services.Sniper.Models
{
    public class CardEntry
    {
        public string Name { get; set; }

        public ItemType ProduceType { get; set; }

        public string ProduceName { get; set; }

        public int ProduceCount { get; set; }

        public int StackSize { get; set; }

        public decimal Profit { get; set; }
    }
}
