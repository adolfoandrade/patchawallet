using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface ITransactionService
    {
        Task<List<StockTransactionVM>> ImportFromCEIAsync(IFormFile file);
        Task<IEnumerable<StockTransactionVM>> GetAsync();
        Task<StockTransactionVM> GetAsync(string id);
        Task<StockTransactionVM> CreateAsync(CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM);
        Task<StockTransactionVM> UpdateAsync(string id, CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM);
        Task DeleteAsync(string id);
    }
}
