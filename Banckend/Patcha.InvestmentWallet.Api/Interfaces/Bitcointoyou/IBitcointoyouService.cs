using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Bitcointoyou
{
    public interface IBitcointoyouService : IBestPriceTo<BitcointoyouOrderBook>
    {
        Task<BitcointoyouOrderBook> GetOrderBookAsync();
    }
}
