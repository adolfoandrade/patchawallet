using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Braziliex
{
    public interface IBraziliexService : IBestPriceTo<BraziliexOrderBook>
    {
        Task<BraziliexOrderBook> GetOrderBookAsync(string coin = "btc_brl");
    }
}
