using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class BitblueOrder
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("order_amount")]
        public double Amount { get; set; }

        [JsonProperty("order_value")]
        public decimal Value { get; set; }

        [JsonProperty("converted_from")]
        public string ConvertedFrom { get; set; }
    }
}
