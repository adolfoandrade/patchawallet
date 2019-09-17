using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Bitblue
{
    public interface IBitblueService : IBestPriceTo<BitblueOrderBook>
    {
        Task<BitblueOrderBook> GetOrderBookAsync(string coin = "");
    }
}
