using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class StockTransactionVM
    {
        public string Id { get; set; }
        public StockVM Stock { get; set; }
        public decimal Commission { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime When { get; set; }
        public TradeTypeEnum TradeType { get; set; }
        public UserVM User { get; set; }
    }
}
