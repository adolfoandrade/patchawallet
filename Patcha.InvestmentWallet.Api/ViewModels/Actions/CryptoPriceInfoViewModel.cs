﻿using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api
{
    public class CryptoPriceInfoViewModel
    {
        public Stock Stock { get; set; }
        public double TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalCurrentPrice { get; set; }
    }
}
