using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api
{
    public class TradeStatementVM
    {
        public List<TradeVM> Trades { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GainOrLoss { get; set; }
        public decimal TotalCost { get; set; }
    }
}
