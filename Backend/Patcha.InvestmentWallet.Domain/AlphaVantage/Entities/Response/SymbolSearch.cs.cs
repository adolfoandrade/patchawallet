using Newtonsoft.Json;
using System;

namespace Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response
{
    public class SymbolSearch
    {
        [JsonProperty("1. symbol")]
        public string Symbol { get; set; }

        [JsonProperty("2. name")]
        public string Name { get; set; }  

        [JsonProperty("3. type")]
        public string Type { get; set; }

        [JsonProperty("4. regin")]
        public string Region { get; set; }

        [JsonProperty("5. marketOpen")]
        public TimeSpan MarketOpen { get; set; }

        [JsonProperty("6. marketClose")]
        public TimeSpan MarketClose { get; set; }

        [JsonProperty("7. timeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("8. currency")]
        public string Currency { get; set; }

        [JsonProperty("9. matchScore")]
        public double MatchScore { get; set; }
    }
}
