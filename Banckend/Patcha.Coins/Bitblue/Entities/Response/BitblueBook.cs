using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class BitblueBook
    {
        [JsonProperty("market")]
        public string Market { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("ask")]
        public List<BitblueOrder> Asks { get; set; }
        [JsonProperty("bid")]
        public List<BitblueOrder> Bids { get; set; }
    }
}
