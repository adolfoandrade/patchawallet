using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class BitcointoyouOrderBook
    {
        [JsonProperty("asks")]
        public decimal[,] Asks { get; set; }

        [JsonProperty("bids")]
        public decimal[,] Bids { get; set; }
    }
}
