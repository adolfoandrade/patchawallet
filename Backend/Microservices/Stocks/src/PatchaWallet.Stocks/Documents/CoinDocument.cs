using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PatchaWallet.Stocks
{
    public class CoinDocument : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string ApiUrl { get; set; }
        public Fee Fee { get; set; }

        private decimal fee(decimal value)
        {
            var fee = ((value * Fee.WithDrawalComissionPercent) / 100) + Fee.WithDrawalComissionValueInBRL;
            return fee;
        }
    }
}
