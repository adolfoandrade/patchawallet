using Patcha.InvestmentWallet.Domain.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Domain.Interfaces
{
    public interface IRepository<T> where T : IDocument
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> AllAsync();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}
