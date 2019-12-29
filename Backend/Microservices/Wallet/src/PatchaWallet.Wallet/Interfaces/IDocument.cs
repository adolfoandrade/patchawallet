using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PatchaWallet.Wallet
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}
