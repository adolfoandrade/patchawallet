using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PatchaWallet.Stocks
{
    public class CoinTrade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Coin Coin { get; set; }
        public decimal Commission { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime When { get; set; }
        public TradeTypeEnum PurchaseType { get; set; }
    }
}
