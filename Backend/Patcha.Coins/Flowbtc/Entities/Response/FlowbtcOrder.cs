using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class FlowbtcOrder
    {
        [JsonProperty("LastTradePrice")]
        public decimal LastTradePrice { get; set; }

        [JsonProperty("Price")]
        public decimal Price { get; set; }

        [JsonProperty("Quantity")]
        public double Quantity { get; set; }
    }
}
