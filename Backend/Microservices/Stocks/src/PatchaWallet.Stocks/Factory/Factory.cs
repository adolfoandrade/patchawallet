using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public static class Factory
    {
        public static StockVM ToVM(this StockDocument stock)
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

        public static StockDocument ToDocument(this StockVM stockVM)
        {
            var stock = new StockDocument()
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

        public static List<StockVM> ToVM(this IEnumerable<StockDocument> stocks)
        {
            var stocksVM = new List<StockVM>();
            foreach (var item in stocks)
            {
                stocksVM.Add(item.ToVM());
            }
            return stocksVM;
        }

        public static QuoteVM ToVM(this QuoteDocument quote)
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

        public static QuoteDocument ToEntity(this QuoteVM quoteVM)
        {
            var quote = new QuoteDocument()
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

        public static StockTransactionVM ToVM(this StockTransactionDocument stockTransaction)
        {
            var stockTransactionVM = new StockTransactionVM()
            {
                Id = stockTransaction.Id,
                Stock = stockTransaction.Stock?.ToVM(),
                Commission = stockTransaction.Commission,
                Amount = stockTransaction.Amount,
                Price = stockTransaction.Price,
                When = stockTransaction.When,
                TradeType = stockTransaction.TradeType,
                User = stockTransaction.User?.ToVM()
            };

            return stockTransactionVM;
        }

        public static StockTransactionDocument ToDocument(this StockTransactionVM stockTransactionVM)
        {
            var stockTransaction = new StockTransactionDocument()
            {
                Id = stockTransactionVM.Id,
                Stock = stockTransactionVM.Stock?.ToDocument(),
                Commission = stockTransactionVM.Commission,
                Amount = stockTransactionVM.Amount,
                Price = stockTransactionVM.Price,
                When = stockTransactionVM.When,
                TradeType = stockTransactionVM.TradeType,
                User = stockTransactionVM.User?.ToDocument()
            };

            return stockTransaction;
        }

        public static List<StockTransactionVM> ToVM(this List<StockTransactionDocument> stockTransactions)
        {
            var stockTransactionsVM = new List<StockTransactionVM>();

            foreach (var item in stockTransactions)
            {
                stockTransactionsVM.Add(item.ToVM());
            }

            return stockTransactionsVM;
        }

        public static List<StockTransactionDocument> ToDocument(this List<StockTransactionVM> stockTransactionsVM)
        {
            var stockTransactions = new List<StockTransactionDocument>();

            foreach (var item in stockTransactionsVM)
            {
                stockTransactions.Add(item.ToDocument());
            }

            return stockTransactions;
        }

        public static IEnumerable<StockTransactionVM> ToVM(this IEnumerable<StockTransactionDocument> stockTransactions)
        {
            return stockTransactions.ToVM();
        }

        public static IEnumerable<StockTransactionDocument> ToDocument(this IEnumerable<StockTransactionVM> stockTransactionsVM)
        {
            return stockTransactionsVM.ToDocument();
        }

        public static UserVM ToVM(this UserDocument document)
        {
            var userVM = new UserVM()
            {
                Id = document.Id,
                UserName = document.UserName,
                Nome = document.Nome,
                Sobrenome = document.Sobrenome,
                Email = document.Email,
                PhoneNumber = document.PhoneNumber,
            };

            return userVM;
        }

        public static UserDocument ToDocument(this UserVM vm)
        {
            var document = new UserDocument()
            {
                Id = vm.Id,
                UserName = vm.UserName,
                Nome = vm.Nome,
                Sobrenome = vm.Sobrenome,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber,
            };

            return document;
        }

    }
}
