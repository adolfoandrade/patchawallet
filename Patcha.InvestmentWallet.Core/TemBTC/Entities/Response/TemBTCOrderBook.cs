using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Core.TemBTC.Entities.Response
{
    public class TemBTCOrderBook
    {
        [JsonProperty("ask")]
        public List<TemBTCOrder> Asks { get; set; }

        [JsonProperty("bid")]
        public List<TemBTCOrder> Bids { get; set; }
    }
}
