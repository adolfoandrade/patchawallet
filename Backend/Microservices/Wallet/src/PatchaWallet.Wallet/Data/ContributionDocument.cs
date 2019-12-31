using System;

namespace PatchaWallet.Wallet
{
    public class ContributionDocument : IDocument
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
