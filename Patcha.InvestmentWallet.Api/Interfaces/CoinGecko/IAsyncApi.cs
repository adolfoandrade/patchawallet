using System;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces.CoinGecko
{
    public interface IAsyncApi
    {
        /// <summary>
        ///     Sends an API request async using GET Method
        /// </summary>
        /// <param name="resourceUri">The resouce uri path</param>
        /// <returns>Asyncronous result turns by TApiResouce</returns>
        Task<T> GetAsync<T>(Uri resourceUri);
    }
}
