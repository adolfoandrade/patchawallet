using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class SimulateGoalResultVM
    {
        public string Id { get; set; }
        public double AnnualPercente { get; set; }
        public decimal BeginValue { get; set; }
        public List<GoalResult> Goals { get; set; }
        public GoalResult FinalGoal { get; set; }
    }
}
