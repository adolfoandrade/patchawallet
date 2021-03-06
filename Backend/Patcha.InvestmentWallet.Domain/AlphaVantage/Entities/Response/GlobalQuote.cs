﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response
{
    public class GlobalQuote
    {
        [JsonProperty("01. symbol")]
        public string Symbol { get; set; }

        [JsonProperty("02. open")]
        public decimal Open { get; set; }

        [JsonProperty("03. high")]
        public decimal Hight { get; set; }

        [JsonProperty("04. low")]
        public decimal Low { get; set; }

        [JsonProperty("05. price")]
        public decimal Price { get; set; }

        [JsonProperty("06. volume")]
        public decimal Volume { get; set; }

        [JsonProperty("07. latest trading day")]
        public DateTime LatestTradingDay { get; set; }

        [JsonProperty("08. previous close")]
        public decimal PreviousClose { get; set; }

        [JsonProperty("09. change")]
        public decimal Change { get; set; }

        [JsonProperty("10. change percent")]
        public string ChangePercent { get; set; }
    }
}
