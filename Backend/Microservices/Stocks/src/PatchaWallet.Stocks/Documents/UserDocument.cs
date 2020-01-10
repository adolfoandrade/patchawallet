using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PatchaWallet.Stocks
{
    public class UserDocument : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
