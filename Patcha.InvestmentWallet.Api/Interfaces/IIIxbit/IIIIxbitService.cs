using Patcha.InvestmentWallet.Core.IIIxbit.Entities.Response;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.IIIxbit
{
    public interface IIIIxbitService : IBestPriceTo<IIIxbitOrderBook>
    {
        Task<IIIxbitOrderBook> GetOrderBookAsync(string coin);
    }
}
