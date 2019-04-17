using Newtonsoft.Json;
using Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Shared;

namespace Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins
{
    public class TickerById
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tickers")]
        public Ticker[] Tickers { get; set; }
    }
}
