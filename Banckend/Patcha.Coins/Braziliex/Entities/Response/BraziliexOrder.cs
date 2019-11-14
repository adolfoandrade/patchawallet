using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class BraziliexOrder
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
