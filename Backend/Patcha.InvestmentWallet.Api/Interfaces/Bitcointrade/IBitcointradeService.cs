using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Bitcointrade
{
    public interface IBitcointradeService : IBestPriceTo<BitcointradeOrderBook>
    {
        Task<BitcointradeOrderBook> GetOrderBookAsync(string coin = "BRLBTC");
    }
}
