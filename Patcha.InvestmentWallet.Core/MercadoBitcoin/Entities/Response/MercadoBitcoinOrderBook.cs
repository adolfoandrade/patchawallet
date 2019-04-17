using Newtonsoft.Json;

namespace Patcha.InvestmentWallet.Core.MercadoBitcoin.Entities.Response
{
    public class MercadoBitcoinOrderBook
    {
        [JsonProperty("asks")]
        public decimal[,] Asks { get; set; }

        [JsonProperty("bids")]
        public decimal[,] Bids { get; set; }
    }
}
