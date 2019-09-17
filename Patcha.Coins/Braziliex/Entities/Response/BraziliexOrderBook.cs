using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class BraziliexOrderBook
    {
        [JsonProperty("asks")]
        public List<BraziliexOrder> Asks { get; set; }

        [JsonProperty("bids")]
        public List<BraziliexOrder> Bids { get; set; }
    }
}
