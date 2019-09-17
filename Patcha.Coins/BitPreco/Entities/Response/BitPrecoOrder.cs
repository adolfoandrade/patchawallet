using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class BitPrecoOrder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
