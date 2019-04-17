using Patcha.InvestmentWallet.Core.Braziliex.Entities.Response;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Braziliex
{
    public interface IBraziliexService : IBestPriceTo<BraziliexOrderBook>
    {
        Task<BraziliexOrderBook> GetOrderBookAsync();
    }
}
