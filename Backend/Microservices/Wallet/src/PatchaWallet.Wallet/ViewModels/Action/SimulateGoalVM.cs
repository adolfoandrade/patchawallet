using System.Collections.Generic;

namespace PatchaWallet.Wallet
{
    public class SimulateGoalVM
    {
        public string Id { get; set; }
        public double AnnualPercente { get; set; }
        public decimal BeginValue { get; set; }
        public List<ContributionVM> Contributions { get; set; }
    }
}
