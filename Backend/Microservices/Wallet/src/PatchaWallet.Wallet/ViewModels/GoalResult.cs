using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class GoalResult
    {
        public DateTime Date { get; set; }
        public decimal CurrentValue { get; set; }
        public decimal Contribution { get; set; }
        public decimal Goal { get; set; }
    }
}
