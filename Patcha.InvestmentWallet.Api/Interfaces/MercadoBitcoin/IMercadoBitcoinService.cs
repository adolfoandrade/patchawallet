using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.MercadoBitcoin
{
    public interface IMercadoBitcoinService : IBestPriceTo<MercadoBitcoinOrderBook>
    {
        Task<MercadoBitcoinOrderBook> GetOrderBookAsync(string coin = "BTC");
    }
}
