using Newtonsoft.Json;

namespace Patcha.Coins
{ 
    public class IIIxbitOrder
    {
        [JsonProperty("unit_price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }
    }
}
