namespace PatchaWallet.Wallet
{
    public class ContributionDocument : IDocument
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public DateKindEnum DateKind { get; set; }
        public int DateKindValue { get; set; }
    }
}
