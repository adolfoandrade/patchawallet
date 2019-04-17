using Newtonsoft.Json;

namespace Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response
{
    public class GlobalQuoteResult
    {
        [JsonProperty("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }
}
