using Newtonsoft.Json;

namespace Patcha.Coins
{
    public class MercadoBitcoinOrderBook
    {
        [JsonProperty("asks")]
        public decimal[,] Asks { get; set; }

        [JsonProperty("bids")]
        public decimal[,] Bids { get; set; }
    }
}
