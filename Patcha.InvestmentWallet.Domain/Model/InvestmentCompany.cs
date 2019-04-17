using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Patcha.InvestmentWallet.Domain.Model
{
    public class InvestmentCompany
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public InvestimentTypeEnum Type { get; set; }
        public string Region { get; set; }
        public TimeSpan MarketOpen { get; set; }
        public TimeSpan MarketClose { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
        public string Currency { get; set; }
        public Quote Quote { get; set; }
    }
}
