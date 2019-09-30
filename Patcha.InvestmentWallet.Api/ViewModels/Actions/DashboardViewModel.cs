using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api
{
    public class DashboardViewModel
    {
        [JsonProperty("StocksPrices")]
        public List<StocksPricesInfoViewModel> StockPriceInfoViewModel { get; set; }

        [JsonProperty("CoinsPrices")]
        public List<CoinsPricesInfoViewModel> CoinsPricesInfoViewModel { get; set; }
    }
}
