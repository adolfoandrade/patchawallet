using System.Collections.Generic;

namespace PatchaWallet.Wallet
{
    public class SimulateGoalDocument : IDocument
    {
        public string Id { get; set; }
        public double AnnualPercente { get; set; }
        public decimal BeginValue { get; set; }
        public List<ContributionDocument> Contributions { get; set; }
    }
}
