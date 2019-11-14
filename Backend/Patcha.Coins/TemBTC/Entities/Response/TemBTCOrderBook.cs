using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class TemBTCOrderBook
    {
        [JsonProperty("ask")]
        public List<TemBTCOrder> Asks { get; set; }

        [JsonProperty("bid")]
        public List<TemBTCOrder> Bids { get; set; }
    }
}
