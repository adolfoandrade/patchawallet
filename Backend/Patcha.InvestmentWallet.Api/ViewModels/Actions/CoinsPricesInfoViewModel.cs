using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api
{
    public class CoinsPricesInfoViewModel
    {
        public Coin Coin { get; set; }
        public double TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalCurrentPrice { get; set; }
        public decimal Difference { get; set; }
    }
}
