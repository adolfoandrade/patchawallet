using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api
{
    public class AccountStatementVM
    {
        public List<TradeStatementVM> TradeStatements { get; set; }
        public decimal TotalCost { get; set; }
        public bool MustPayTax { get; set; }
        public decimal GainOrLoss { get; set; }
    }
}
