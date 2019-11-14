using Microsoft.AspNetCore.Http;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces
{
    public interface ITransactionService
    {
        Task<List<StockTransactionVM>> ImportFromCEIAsync(IFormFile file, string newPath);
        Task<IEnumerable<StockTransactionVM>> GetAsync();
        Task<StockTransactionVM> GetAsync(string id);
        Task<StockTransactionVM> CreateAsync(CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM);
        Task<StockTransactionVM> UpdateAsync(string id, CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM);
        Task DeleteAsync(string id);
    }
}
