using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.Coins
{
    public class IIIxbitOrderBook
    {
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("buy_orders")]
        public List<IIIxbitOrder> BuyOrders { get; set; }

        [JsonProperty("sell_orders")]
        public List<IIIxbitOrder> SellOrders { get; set; }
    }
}
