namespace SnipeLinksGenerator.Services.PoeNinja.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public int MapTier { get; set; }

        public int LevelRequired { get; set; }

        public int BaseType { get; set; }

        public int StackSize { get; set; }

        public string Variant { get; set; }

        public string ProphecyText { get; set; }

        public string ArtFilename { get; set; }

        public int Links { get; set; }

        public int ItemClass { get; set; }

        public SparkLine Sparkline { get; set; }

        public SparkLine LowConfidenceSparkline { get; set; }

        public Modifier[] ImplicitModifiers { get; set; }

        public Modifier[] ExplicitModifiers { get; set; }

        public string FlavourText { get; set; }

        public bool Corrupted { get; set; }

        public int GemLevel { get; set; }

        public int GemQuality { get; set; }

        public string ItemType { get; set; }

        public decimal ChaosValue { get; set; }

        public decimal ExaltedValue { get; set; }

        public int Count { get; set; }
    }
}
