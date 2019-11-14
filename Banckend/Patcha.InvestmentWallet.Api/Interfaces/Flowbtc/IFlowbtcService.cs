using Patcha.Coins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.Flowbtc
{
    public interface IFlowbtcService : IBestPriceTo<FlowbtcOrderbook>
    {
        Task<FlowbtcOrderbook> GetOrderBookAsync(string coin);
    }
}
