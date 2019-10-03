using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Patcha.InvestmentWallet.Domain.Model;
using System;

namespace Patcha.InvestmentWallet.Api
{
    public class CreateOrUpdateStockTransactionVM
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string StockId { get; set; }
        public decimal Commission { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime When { get; set; }
        public TradeTypeEnum TradeType { get; set; }
    }
}
