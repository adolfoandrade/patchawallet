using MediatR;
using Patcha.InvestmentWallet.Api.Factory;
using Patcha.InvestmentWallet.Api.Interfaces;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Services
{
    public class StockTradeService : IStockTradeService
    {
        private readonly IMediator _mediator;

        public StockTradeService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<DashboardViewModel> GetAsync(int year = 0, int month = 0, int day = 0, string stock = null)
        {
            var stock_transactions = await _mediator.Send(new GetCollectionRequest<StockTransaction>());

            if (!String.IsNullOrEmpty(stock))
                stock_transactions = stock_transactions.Where(p => p.Stock.Symbol == stock);

            if (year > 0)
            {
                stock_transactions = stock_transactions.Where(p => p.When.Year == year);

                if (month > 0)
                {
                    stock_transactions = stock_transactions.Where(p => p.When.Month == month);

                    if (day > 0)
                        stock_transactions = stock_transactions.Where(p => p.When.Day == day);
                }
            }

            var stock_prices_info = await StockAvgPriceAsync(stock_transactions);

            var vm = new DashboardViewModel()
            {
                StockPriceInfoViewModel = stock_prices_info
            };

            return vm;
        }

        public async Task<AccountStatementVM> GetTradeHistory(int year = 0, int month = 0, int day = 0, string stock = null)
        {
            var stock_negotiations = await _mediator.Send(new GetCollectionRequest<StockTransaction>());
            stock_negotiations = stock_negotiations.Where(x => x.When > DateTime.Parse("2019/09/01"));

            if (!String.IsNullOrEmpty(stock))
                stock_negotiations = stock_negotiations.Where(p => p.Stock.Symbol == stock);

            if (year > 0)
            {
                stock_negotiations = stock_negotiations.Where(p => p.When.Year == year);

                if (month > 0)
                {
                    stock_negotiations = stock_negotiations.Where(p => p.When.Month == month);

                    if (day > 0)
                        stock_negotiations = stock_negotiations.Where(p => p.When.Day == day);
                }
            }

            var vm = new AccountStatementVM()
            {
                TradeStatements = new List<TradeStatementVM>()
            };

            TradeStatementVM tradeStatementVM = new TradeStatementVM()
            {
                Trades = new List<TradeVM>()
            };

            var stocks_trade = stock_negotiations.Select(x => x.Stock.Symbol).Distinct();

            foreach (var stock_trade in stocks_trade)
            {
                var buys = stock_negotiations
                    .Where(x => x.Stock.Symbol == stock_trade && x.TradeType == TradeTypeEnum.BUY)
                    .OrderBy(x => x.When)
                    .ToList();

                var sells = stock_negotiations
                    .Where(x => x.Stock.Symbol == stock_trade && x.TradeType == TradeTypeEnum.SELL)
                    .OrderBy(x => x.When)
                    .ToList();

                foreach (var sell in sells)
                {
                    var trade = buys.Where(x => x.When < sell.When).ToList();
                    var trade_amount = sell.Amount - trade.Sum(x => x.Amount) < 1 ? sell.Amount : sell.Amount - trade.Sum(x => x.Amount);
                    var buy_price = (decimal)trade_amount * (trade.Sum(x => x.Price * (decimal)x.Amount) / (decimal)trade_amount);
                    var sold_price = (decimal)sell.Amount * sell.Price;
                    var stock_description = trade.FirstOrDefault().Stock.Name;
                    var remaining_amount = trade.Sum(x => x.Amount) - sell.Amount;

                    var tradeVM = new TradeVM()
                    {
                        TransactionsBuy = trade.ToVM(),
                        TransactionSell = sell.ToVM(),
                        RemainingAmount = remaining_amount,
                        Description = stock_description,
                        Amount = sell.Amount,
                        GainOrLoss = sold_price - buy_price,
                        Symbol = stock_trade,
                        When = sell.When,
                        Buy = trade.Sum(x => x.Price * (decimal)x.Amount) / (decimal)trade_amount,
                        Sell = sell.Price,
                        TotalBuy = buy_price,
                        TotalSell = sold_price,
                        TotalCost = sell.Price * (decimal)sell.Amount
                    };

                    tradeStatementVM.Trades.Add(tradeVM);
                    tradeStatementVM.Month = tradeVM.When.Month;
                    tradeStatementVM.Year = tradeVM.When.Year;
                    tradeStatementVM.GainOrLoss += tradeVM.GainOrLoss;
                    tradeStatementVM.TotalCost += tradeVM.TotalCost;
                }

                if (!vm.TradeStatements.Any(x => x.Month == tradeStatementVM.Month && x.Year == tradeStatementVM.Year))
                    vm.TradeStatements.Add(tradeStatementVM);

            }

            vm.TotalCost = vm.TradeStatements.Sum(x => x.TotalCost);
            vm.MustPayTax = vm.TotalCost > 20000m;
            vm.GainOrLoss = vm.TradeStatements.Sum(x => x.GainOrLoss);

            return vm;
        }

        public Task<List<StocksPricesInfoViewModel>> StockAvgPriceAsync(IEnumerable<StockTransaction> transactions)
        {
            return Task.Factory.StartNew(() =>
            {
                var investment_ids = transactions.Select(s => s.Stock.Id).Distinct();
                var stockPriceInfoVM = new List<StocksPricesInfoViewModel>();

                foreach (var investment_id in investment_ids)
                {
                    var negociations_histories = transactions.Where(p => p.Stock.Id == investment_id).OrderBy(p => p.When);
                    decimal total_price = 0;
                    double total_amount = 0;
                    foreach (var purchase_history in negociations_histories)
                    {
                        if (purchase_history.TradeType == TradeTypeEnum.SELL)
                        {
                            total_price -= (decimal)purchase_history.Amount * purchase_history.Price;
                            total_amount -= purchase_history.Amount;
                        }
                        else
                        {
                            total_price += (decimal)purchase_history.Amount * purchase_history.Price;
                            total_amount += purchase_history.Amount;
                        }
                    }
                    if (total_amount > 0)
                    {
                        var company = negociations_histories.Select(p => p.Stock).FirstOrDefault();
                        var quotes = _mediator.Send(new GetCollectionRequest<Quote>()).Result.OrderByDescending(x => x.LastRequest);
                        var quote = quotes.FirstOrDefault(x => x.Symbol == company.Symbol);

                        if (quote == null)
                        {
                            // Call an other api or web crawler to get current price
                        }

                        stockPriceInfoVM.Add(new StocksPricesInfoViewModel()
                        {
                            Stock = company,
                            TotalPrice = total_price,
                            TotalAmount = total_amount,
                            AveragePrice = total_price / (decimal)total_amount,
                            CurrentPrice = quote != null ? quote.Price : 0,
                            TotalCurrentPrice = quote != null ? quote.Price * (decimal)total_amount : 0,
                            Difference = quote != null ? (quote.Price * (decimal)total_amount) - total_price : 0,
                        });
                    }
                }

                return stockPriceInfoVM;
            });
        }

    }
}
