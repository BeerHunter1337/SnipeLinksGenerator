using Newtonsoft.Json;

namespace SnipeLinksGenerator.Services.PoeNinja.Models
{
    public class CurrencyRate
    {
        public string CurrencyTypeName { get; set; }

        public Amount Pay { get; set; }

        public Amount Receive { get; set; }

        public SparkLine PaySparkLine { get; set; }

        public SparkLine ReceiveSparkLine { get; set; }

        public decimal ChaosEquivalent { get; set; }

        public SparkLine LowConfidencePaySparkLine { get; set; }

        public SparkLine LowConfidenceReceiveSparkLine { get; set; }
    }
}
