using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class BitcointradeOrder
    {
        [JsonProperty("asks")]
        public List<BitcointradeBook> Asks { get; set; }

        [JsonProperty("bids")]
        public List<BitcointradeBook> Bids { get; set; }
    }

    public class BitcointradeBook
    {
        [JsonProperty("unit_price")]
        public decimal Price { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("stop_limit_price")]
        public decimal? StopLimitPrice { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }
    }

    public class BitcointradeOrderBook
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public BitcointradeOrder Data { get; set; }
    }

}
