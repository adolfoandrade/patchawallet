using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class NegociecoinsOrderBook
    {
        [JsonProperty("ask")]
        public List<NegociecoinsBook> Asks { get; set; }

        [JsonProperty("bid")]
        public List<NegociecoinsBook> Bids { get; set; }
    }
}
