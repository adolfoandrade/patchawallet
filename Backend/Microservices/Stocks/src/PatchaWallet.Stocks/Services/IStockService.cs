using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface IStockService
    {
        Task<IEnumerable<StockVM>> GetStocksAsync(int pageSize = 10, int nextCursor = 0, string search = "");
        Task<StockVM> GetStockAsync(string id);
        Task<StockVM> AddStockAsync(StockVM stockVM);
        Task<StockVM> AddStocksAsync(IEnumerable<StockVM> stocks);
    }
}
