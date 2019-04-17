using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Core.Braziliex.Entities.Response
{
    public class BraziliexOrderBook
    {
        [JsonProperty("asks")]
        public List<BraziliexOrder> Asks { get; set; }

        [JsonProperty("bids")]
        public List<BraziliexOrder> Bids { get; set; }
    }
}
