using Patcha.InvestmentWallet.Core.MercadoBitcoin.Entities.Response;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.MercadoBitcoin
{
    public interface IMercadoBitcoinService : IBestPriceTo<MercadoBitcoinOrderBook>
    {
        Task<MercadoBitcoinOrderBook> GetOrderBookAsync();
    }
}
