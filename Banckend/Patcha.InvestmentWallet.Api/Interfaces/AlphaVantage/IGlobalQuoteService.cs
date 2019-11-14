using Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.AlphaVantage
{
    public interface IGlobalQuoteService
    {
        Task<GlobalQuoteResult> GetGlobalQuote(string function, string symbol);
    }
}
