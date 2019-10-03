using System;
using System.Collections.Generic;

namespace Patcha.InvestmentWallet.Api
{
    public class TradeVM
    {
        public IEnumerable<StockTransactionVM> TransactionsBuy { get; set; }
        public StockTransactionVM TransactionSell { get; set; }
        public double RemainingAmount { get; set; }
        public string Description { get; set; }
        public string Symbol { get; set; }
        public double Amount { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public decimal TotalBuy { get; set; }
        public decimal TotalSell { get; set; }
        public decimal TotalCost { get; set; }
        public decimal GainOrLoss { get; set; }
        public DateTime When { get; set; }
    }
}
