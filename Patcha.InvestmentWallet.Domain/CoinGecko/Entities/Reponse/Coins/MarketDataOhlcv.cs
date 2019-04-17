using Newtonsoft.Json;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins
{
    public class MarketDataOhlcv
    {
        [JsonProperty("current_price")]
        public CurrentPrice CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public CurrentPrice MarketCap { get; set; }

        [JsonProperty("market_cap_rank")]
        public long MarketCapRank { get; set; }

        [JsonProperty("total_volume")]
        public CurrentPrice TotalVolume { get; set; }

        [JsonProperty("high_24h")]
        public CurrentPrice High24H { get; set; }

        [JsonProperty("low_24h")]
        public CurrentPrice Low24H { get; set; }

        [JsonProperty("price_change_24h")]
        public double PriceChange24H { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24H { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public double MarketCapChange24H { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public double MarketCapChangePercentage24H { get; set; }

        [JsonProperty("circulating_supply")]
        public string CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public long? TotalSupply { get; set; }
    }

    public class CurrentPrice
    {
        [JsonProperty("aed")]
        public decimal Aed { get; set; }

        [JsonProperty("ars")]
        public decimal Ars { get; set; }

        [JsonProperty("aud")]
        public decimal Aud { get; set; }

        [JsonProperty("bch")]
        public decimal Bch { get; set; }

        [JsonProperty("bdt")]
        public decimal Bdt { get; set; }

        [JsonProperty("bhd")]
        public decimal Bhd { get; set; }

        [JsonProperty("bmd")]
        public decimal Bmd { get; set; }

        [JsonProperty("bnb")]
        public decimal Bnb { get; set; }

        [JsonProperty("brl")]
        public decimal Brl { get; set; }

        [JsonProperty("btc")]
        public decimal Btc { get; set; }

        [JsonProperty("usd")]
        public decimal Usd { get; set; }

    }
}
