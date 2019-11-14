using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces
{
    public interface IStockTradeService
    {
        Task<List<StocksPricesInfoViewModel>> StockAvgPriceAsync(IEnumerable<StockTransaction> transactions);
        Task<DashboardViewModel> GetAsync(int year = 0, int month = 0, int day = 0, string stock = null);
        Task<AccountStatementVM> GetTradeHistory(int year = 0, int month = 0, int day = 0, string stock = null);
    }
}
