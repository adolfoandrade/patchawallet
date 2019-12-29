using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PatchaWallet.Stocks
{
    public class QuoteVM
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public decimal Open { get; set; }
        public decimal Hight { get; set; }
        public decimal Low { get; set; }
        public decimal Price { get; set; }
        public decimal Volume { get; set; }
        public DateTime LatestTradingDay { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal Change { get; set; }
        public string ChangePercent { get; set; }
        public DateTime LastRequest { get; set; }
    }
}
