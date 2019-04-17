using Newtonsoft.Json;
using System;

namespace Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins
{
    public class SparklineIn7D
    {
        [JsonProperty("price")]
        public double[] Price { get; set; }
    }
}
