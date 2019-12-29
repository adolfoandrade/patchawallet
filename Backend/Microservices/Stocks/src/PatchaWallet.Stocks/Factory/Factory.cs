using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public static class Factory
    {
        public static StockVM ToVM(this Stock stock)
        {
            var stockVM = new StockVM()
            {
                Id = stock.Id,
                Name = stock.Name,
                Symbol = stock.Symbol,
                Type = stock.Type,
                Region = stock.Region,
                MarketOpen = stock.MarketOpen,
                MarketClose = stock.MarketClose,
                TimeZone = stock.TimeZone,
                Currency = stock.Currency,
                Quote = stock.Quote?.ToVM()
            };

            return stockVM;
        }

        public static Stock ToEntity(this StockVM stockVM)
        {
            var stock = new Stock()
            {
                Id = stockVM.Id,
                Name = stockVM.Name,
                Symbol = stockVM.Symbol,
                Type = stockVM.Type,
                Region = stockVM.Region,
                MarketOpen = stockVM.MarketOpen,
                MarketClose = stockVM.MarketClose,
                TimeZone = stockVM.TimeZone,
                Currency = stockVM.Currency,
                Quote = stockVM.Quote?.ToEntity()
            };

            return stock;
        }

        public static List<StockVM> ToVM(this IEnumerable<Stock> stocks)
        {
            var stocksVM = new List<StockVM>();
            foreach (var item in stocks)
            {
                stocksVM.Add(item.ToVM());
            }
            return stocksVM;
        }

        public static QuoteVM ToVM(this Quote quote)
        {
            var quoteVM = new QuoteVM()
            {
                Id = quote.Id,
                Symbol = quote.Symbol,
                Open = quote.Open,
                Hight = quote.Hight,
                Low = quote.Low,
                Price = quote.Price,
                Volume = quote.Volume,
                LatestTradingDay = quote.LatestTradingDay,
                PreviousClose = quote.PreviousClose,
                Change = quote.Change,
                ChangePercent = quote.ChangePercent,
                LastRequest = quote.LastRequest
            };

            return quoteVM;
        }

        public static Quote ToEntity(this QuoteVM quoteVM)
        {
            var quote = new Quote()
            {
                Id = quoteVM.Id,
                Symbol = quoteVM.Symbol,
                Open = quoteVM.Open,
                Hight = quoteVM.Hight,
                Low = quoteVM.Low,
                Price = quoteVM.Price,
                Volume = quoteVM.Volume,
                LatestTradingDay = quoteVM.LatestTradingDay,
                PreviousClose = quoteVM.PreviousClose,
                Change = quoteVM.Change,
                ChangePercent = quoteVM.ChangePercent,
                LastRequest = quoteVM.LastRequest
            };

            return quote;
        }

    }
}
