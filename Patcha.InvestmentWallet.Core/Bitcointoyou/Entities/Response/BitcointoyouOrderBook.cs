using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Core.Bitcointoyou.Entities.Response
{
    public class BitcointoyouOrderBook
    {
        [JsonProperty("asks")]
        public decimal[,] Asks { get; set; }

        [JsonProperty("bids")]
        public decimal[,] Bids { get; set; }
    }
}
