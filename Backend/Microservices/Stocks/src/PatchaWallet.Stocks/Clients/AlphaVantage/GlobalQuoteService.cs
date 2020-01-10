using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class GlobalQuoteService : BaseApi, IGlobalQuoteService
    {
        public GlobalQuoteService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<GlobalQuoteResult> GetGlobalQuote(string function, string symbol)
        {
            return await GetAsync<GlobalQuoteResult>(AlphaVantageQueryStringService.AppendQueryString(string.Empty,
                new Dictionary<string, object>
                {
                    {"function", function},
                    {"symbol", symbol}
                }));
        }

    }
}
