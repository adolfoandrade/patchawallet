using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Core.BitPreco.Entities.Response
{
    public class BitPrecoOrderBook
    {
        [JsonProperty("asks")]
        public List<BitPrecoOrder> Asks { get; set; }

        [JsonProperty("bids")]
        public List<BitPrecoOrder> Bids { get; set; }
    }
}
