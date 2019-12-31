using MongoDB.Bson;
using System;

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
        public DateTime Date { get; set; }
    }
}
