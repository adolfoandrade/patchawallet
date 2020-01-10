using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface ISymbolSearchService
    {
        /// <summary>
        /// List all coins with data (name, price, market, developer, community, etc) - paginated by 50
        /// </summary>
        /// <param name="function">SYMBOL_SEARCH</param>
        /// <param name="keywords">String to search</param>
        /// <returns></returns>
        Task<BestMatches> SearchSymbolAsync(string function, string keywords);
    }
}
