using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class BitPrecoOrderBook
    {
        [JsonProperty("asks")]
        public List<BitPrecoOrder> Asks { get; set; }

        [JsonProperty("bids")]
        public List<BitPrecoOrder> Bids { get; set; }
    }
}
