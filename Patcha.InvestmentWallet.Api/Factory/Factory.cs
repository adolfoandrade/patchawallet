using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Factory
{
    // Replace this after by some Framework
    public static class Factory
    {
        public static StockTransactionVM ToVM(this StockTransaction stockTransaction)
        {
            var stockTransactionVM = new StockTransactionVM()
            {
                Id = stockTransaction.Id,
                Stock = stockTransaction.Stock,
                Commission = stockTransaction.Commission,
                Amount = stockTransaction.Amount,
                Price = stockTransaction.Price,
                When = stockTransaction.When,
                TradeType = stockTransaction.TradeType
            };

            return stockTransactionVM;
        }

        public static StockTransaction ToEntity(this StockTransactionVM stockTransactionVM)
        {
            var stockTransaction = new StockTransaction()
            {
                Id = stockTransactionVM.Id,
                Stock = stockTransactionVM.Stock,
                Commission = stockTransactionVM.Commission,
                Amount = stockTransactionVM.Amount,
                Price = stockTransactionVM.Price,
                When = stockTransactionVM.When,
                TradeType = stockTransactionVM.TradeType
            };

            return stockTransaction;
        }

        public static List<StockTransactionVM> ToVM(this List<StockTransaction> stockTransactions)
        {
            var stockTransactionsVM = new List<StockTransactionVM>();

            foreach (var item in stockTransactions)
            {
                stockTransactionsVM.Add(item.ToVM());
            }

            return stockTransactionsVM;
        }

        public static List<StockTransaction> ToEntity(this List<StockTransactionVM> stockTransactionsVM)
        {
            var stockTransactions = new List<StockTransaction>();

            foreach (var item in stockTransactionsVM)
            {
                stockTransactions.Add(item.ToEntity());
            }

            return stockTransactions;
        }

        public static IEnumerable<StockTransactionVM> ToVM(this IEnumerable<StockTransaction> stockTransactions)
        {
            return stockTransactions.ToVM();
        }

        public static IEnumerable<StockTransaction> ToEntity(this IEnumerable<StockTransactionVM> stockTransactionsVM)
        {
            return stockTransactionsVM.ToEntity();
        }
    }
}
