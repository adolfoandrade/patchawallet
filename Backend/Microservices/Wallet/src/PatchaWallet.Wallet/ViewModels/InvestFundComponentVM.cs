using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class InvestFundComponentVM
    {
        public object InvestFund { get; set; }
        public decimal GainOrLossValue { get; set; }
        public double GainOrLossPercent { get; set; }
    }
}
