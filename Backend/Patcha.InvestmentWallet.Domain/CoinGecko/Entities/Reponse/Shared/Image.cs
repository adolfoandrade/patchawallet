using Newtonsoft.Json;
using System;

namespace Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Shared
{
    public class Image
    {
        [JsonProperty("thumb")]
        public Uri Thumb { get; set; }

        [JsonProperty("small")]
        public Uri Small { get; set; }

        [JsonProperty("large")]
        public Uri Large { get; set; }
    }
}
