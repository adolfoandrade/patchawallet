using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PatchaWallet.Stocks
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}
