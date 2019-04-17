﻿using Newtonsoft.Json;

namespace Patcha.InvestmentWallet.Core.Braziliex.Entities.Response
{
    public class BraziliexOrder
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}
