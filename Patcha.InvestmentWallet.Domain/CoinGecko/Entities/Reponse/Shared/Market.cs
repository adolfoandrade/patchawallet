using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Shared
{
    public class Market
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("has_trading_incentive")]
        public bool HasTradingIncentive { get; set; }
    }
}
