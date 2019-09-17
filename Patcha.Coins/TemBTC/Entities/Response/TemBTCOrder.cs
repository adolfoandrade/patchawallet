using Newtonsoft.Json;

namespace Patcha.Coins
{ 
    public class TemBTCOrder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
}
