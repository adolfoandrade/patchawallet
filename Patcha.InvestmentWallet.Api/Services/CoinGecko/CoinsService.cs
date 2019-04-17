using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;
using Patcha.InvestmentWallet.Api.Services;
using Patcha.InvestmentWallet.Domain.CoinGecko.ApiEndPoints;
using Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins;
using Patcha.InvestmentWallet.Domain.CoinGecko.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.CoinGecko.Services
{
    public class CoinsService : BaseApi, ICoinsService
    {
        public CoinsService(HttpClient httpClient)
            : base(httpClient)
        {
        }

        public async Task<IReadOnlyList<CoinFullData>> GetAllCoinsDataAsync()
        {
            return await GetAllCoinsDataAsync(OrderField.GeckoDesc, null, null, "", null);
        }

        public async Task<IReadOnlyList<CoinFullData>> GetAllCoinsDataAsync(string order, int? perPage, int? page, string localization = "brl", bool? sparkline = null)
        {
            return await GetAsync<List<CoinFullData>>(CoinGeckoQueryStringService.AppendQueryString(
                CoinsApiEndPoints.Coins, new Dictionary<string, object>
                {
                    {"order", order},
                    {"per_page", perPage},
                    {"page",page},
                    {"localization", "brl"},
                    {"sparkline",sparkline}
                }));
        }

        public async Task<IReadOnlyList<CoinList>> GetCoinListAsync()
        {
            return await GetAsync<IReadOnlyList<CoinList>>(CoinGeckoQueryStringService.AppendQueryString(CoinsApiEndPoints.CoinList));
        }

        public async Task<List<CoinMarkets>> GetCoinMarketsAsync(string vsCurrency)
        {
            return await GetCoinMarketsAsync(vsCurrency, new string[] { }, null, null, null, false);
        }

        public async Task<List<CoinMarkets>> GetCoinMarketsAsync(string vsCurrency, string[] ids, string order, int? perPage, int? page, bool sparkline)
        {
            return await GetAsync<List<CoinMarkets>>(CoinGeckoQueryStringService.AppendQueryString(CoinsApiEndPoints.CoinMarkets, new Dictionary<string, object>
            {
                {"vs_currency",vsCurrency},
                {"ids",string.Join(",",ids)},
                {"per_page",perPage},
                {"page",page},
                {"sparkline",sparkline}
            }));
        }

        public async Task<CoinFullDataById> GetAllCoinDataWithIdAsync(string id)
        {
            return await GetAllCoinDataWithIdAsync(id, "true", true, true, true, true, false);
        }

        public async Task<CoinFullDataById> GetAllCoinDataWithIdAsync(string id, string localization, bool tickers,
            bool marketData, bool communityData, bool developerData, bool sparkline)
        {
            return await GetAsync<CoinFullDataById>(CoinGeckoQueryStringService.AppendQueryString(
                CoinsApiEndPoints.AllDataByCoinId(id), new Dictionary<string, object>
                {
                    {"localization", localization},
                    {"tickers", tickers},
                    {"market_data", marketData},
                    {"community_data", communityData},
                    {"developer_data", developerData},
                    {"sparkline", sparkline}
                }));
        }

        public async Task<TickerById> GetTickerByCoinIdAsync(string id)
        {
            return await GetTickerByCoinIdAsync(id, null);
        }

        public async Task<TickerById> GetTickerByCoinIdAsync(string id, int? page)
        {
            return await GetAsync<TickerById>(CoinGeckoQueryStringService.AppendQueryString(
                CoinsApiEndPoints.TickerByCoinId(id), new Dictionary<string, object>
                {
                    {"page", page}
                }));
        }

        public async Task<CoinFullData> GetHistoryByCoinIdAsync(string id, string date, string localization)
        {
            return await GetAsync<CoinFullData>(CoinGeckoQueryStringService.AppendQueryString(
                CoinsApiEndPoints.HistoryByCoinId(id), new Dictionary<string, object>
                {
                    {"date",date},
                    {"localization",localization}
                }));
        }

        public async Task<MarketChartById> GetMarketChartsByCoinIdAsync(string id, string[] vsCurrency, string days)
        {
            return await GetAsync<MarketChartById>(CoinGeckoQueryStringService.AppendQueryString(
                CoinsApiEndPoints.MarketChartByCoinId(id), new Dictionary<string, object>
                {
                    {"vs_currency", string.Join(",",vsCurrency)},
                    {"days", days}
                }));
        }


    }
}
