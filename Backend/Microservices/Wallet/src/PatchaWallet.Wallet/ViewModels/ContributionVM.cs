using MongoDB.Bson;

namespace PatchaWallet.Wallet
{
    public class ContributionVM
    {
        public ContributionVM()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        public string Id { get; set; }
        public decimal Value { get; set; }
        public DateKindEnum DateKind { get; set; }
        public int DateKindValue { get; set; }
    }
}
