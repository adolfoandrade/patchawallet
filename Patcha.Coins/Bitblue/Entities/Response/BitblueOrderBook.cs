using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class BitblueOrderBook
    {
        [JsonProperty("order-book")]
        public BitblueBook OrderBook { get; set; }
    }
}
