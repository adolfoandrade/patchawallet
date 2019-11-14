using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class NegociecoinsBook
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("quantity")]
        public double Amount { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
}
