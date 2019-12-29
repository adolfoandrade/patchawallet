using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class WalletComponentVM
    {
        public List<CoinComponentVM> Coins { get; set; }
        public List<InvestFundComponentVM> InvestFunds { get; set; }
        public List<StockComponentVM> Stocks { get; set; }
        public decimal GainOrLossValue { get; set; }
        public double GainOrLossPercent { get; set; }
    }
}
