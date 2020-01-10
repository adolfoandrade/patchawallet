using Newtonsoft.Json;

namespace PatchaWallet.Stocks
{
    public class GlobalQuoteResult
    {
        [JsonProperty("Global Quote")]
        public GlobalQuote GlobalQuote { get; set; }
    }
}
