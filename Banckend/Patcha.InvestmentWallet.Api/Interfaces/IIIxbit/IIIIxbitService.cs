using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.IIIxbit
{
    public interface IIIIxbitService : IBestPriceTo<IIIxbitOrderBook>
    {
        Task<IIIxbitOrderBook> GetOrderBookAsync(string coin);
    }
}
