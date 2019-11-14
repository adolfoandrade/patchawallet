using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Negociecoins
{
    public interface INegociecoinsService : IBestPriceTo<NegociecoinsOrderBook>
    {
        Task<NegociecoinsOrderBook> GetOrderBookAsync(string coin = "btcbrl");
    }
}
