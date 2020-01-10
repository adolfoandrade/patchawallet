using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PatchaWallet.Stocks
{
    public class StockTransactionDocument : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public StockDocument Stock { get; set; }
        public decimal Commission { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime When { get; set; }
        public TradeTypeEnum TradeType { get; set; }
        public UserDocument User { get; set; }
    }
}