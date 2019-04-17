using Patcha.InvestmentWallet.Core.TemBTC.Entities.Response;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.TemBTC
{
    public interface ITemBTCService : IBestPriceTo<TemBTCOrderBook>
    {
        Task<TemBTCOrderBook> GetOrderBookAsync();
    }
}
