using System;

namespace PatchaWallet.Stocks
{
    public class CreateOrUpdateStockTransactionVM
    {
        public string Id { get; set; }
        public string StockId { get; set; }
        public decimal Commission { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime When { get; set; }
        public TradeTypeEnum TradeType { get; set; }
    }
}
