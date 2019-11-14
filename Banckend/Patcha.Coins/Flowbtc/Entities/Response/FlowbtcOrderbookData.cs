using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class FlowbtcOrderbookData
    {
        [JsonProperty("asks")]
        public List<FlowbtcOrder> Asks { get; set; }

        [JsonProperty("bids")]
        public List<FlowbtcOrder> Bids { get; set; }
    }
}
