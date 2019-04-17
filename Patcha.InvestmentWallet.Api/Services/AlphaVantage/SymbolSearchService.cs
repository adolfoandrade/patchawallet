using Patcha.InvestmentWallet.Api.Interfaces.AlphaVantage;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Services.AlphaVantage
{
    public class SymbolSearchService : BaseApi, ISymbolSearchService
    {
        public SymbolSearchService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<BestMatches> SearchSymbolAsync(string function, string keywords)
        {
            if (keywords.ToLower().Contains("BVMF3"))
                keywords = "B3SA3.SA";

            return await GetAsync<BestMatches>(AlphaVantageQueryStringService.AppendQueryString(string.Empty, 
                new Dictionary<string, object>
                {
                    {"function", function},
                    {"keywords", keywords}
                }));
        }
    }
}
