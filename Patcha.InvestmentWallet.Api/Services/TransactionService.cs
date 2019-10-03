using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Patcha.InvestmentWallet.Api.Factory;
using Patcha.InvestmentWallet.Api.Interfaces;
using Patcha.InvestmentWallet.Api.Interfaces.AlphaVantage;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Parameters;
using Patcha.InvestmentWallet.Domain.DomainNotification;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IMediator _mediator;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly ISymbolSearchService _symbolSearchService;

        public TransactionService(IMediator mediator,
            IDomainNotificationHandler<DomainNotification> notifications,
            ISymbolSearchService symbolSearchService)
        {
            _mediator = mediator;
            _notifications = notifications;
            _symbolSearchService = symbolSearchService;
        }

        public async Task<IEnumerable<StockTransactionVM>> GetAsync()
        {
            var stock_transactions = await _mediator.Send(new GetCollectionRequest<StockTransaction>());
            return stock_transactions.ToVM();
        }

        public async Task<StockTransactionVM> GetAsync(string id)
        {
            StockTransaction transaction = await _mediator.Send(new GetSingleRequest<StockTransaction>(id));

            if (transaction == null)
                _notifications.AddNotification("404", "stock transaction was not found.");

            return transaction.ToVM();
        }

        public async Task<StockTransactionVM> CreateAsync(CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            var stock = await _mediator.Send(new GetSingleRequest<Stock>(createOrUpdateStockTransactionVM.StockId));
            var stock_transaction = new StockTransaction()
            {
                Id = createOrUpdateStockTransactionVM.Id,
                Stock = stock,
                Commission = createOrUpdateStockTransactionVM.Commission,
                Amount = createOrUpdateStockTransactionVM.Amount,
                Price = createOrUpdateStockTransactionVM.Price,
                When = createOrUpdateStockTransactionVM.When,
                TradeType = createOrUpdateStockTransactionVM.TradeType
            };

            var checkIfExists = await _mediator.Send(new CheckExistsRequest<StockTransaction>(stock_transaction));
            if (checkIfExists != null)
            {
                _notifications.AddNotification("400", "Already exist a transaction with the same data.");
                return checkIfExists.ToVM();
            }

            stock_transaction = await _mediator.Send(new CreateRequest<StockTransaction>(stock_transaction));
            return stock_transaction.ToVM();
        }

        public async Task<StockTransactionVM> UpdateAsync(string id, CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            if (string.IsNullOrEmpty(createOrUpdateStockTransactionVM.Id))
            {
                _notifications.AddNotification("404", "Stock transaction was not found.");
                return null;
            }

            var stock = await _mediator.Send(new GetSingleRequest<Stock>(createOrUpdateStockTransactionVM.StockId));
            var stockTransactionToUpdate = await _mediator.Send(new GetSingleRequest<StockTransaction>(createOrUpdateStockTransactionVM.Id));

            stockTransactionToUpdate.Stock = stock;
            stockTransactionToUpdate.Commission = createOrUpdateStockTransactionVM.Commission;
            stockTransactionToUpdate.Amount = createOrUpdateStockTransactionVM.Amount;
            stockTransactionToUpdate.Price = createOrUpdateStockTransactionVM.Price;
            stockTransactionToUpdate.When = createOrUpdateStockTransactionVM.When;
            stockTransactionToUpdate.TradeType = createOrUpdateStockTransactionVM.TradeType;

            StockTransaction stock_transaction;
            try
            {
                stock_transaction = await _mediator.Send(new UpdateRequest<StockTransaction>(id, stockTransactionToUpdate));
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("500", $"Iternal server error on try to update the Stock Transaction. Exception generated was: {ex.Message}");
                return null;
            }

            if (stock_transaction == null)
                _notifications.AddNotification("404", "Stock transaction was not found.");

            return stock_transaction.ToVM();
        }

        public async Task DeleteAsync(string id)
        {
            StockTransaction stock_trade = await _mediator.Send(new GetSingleRequest<StockTransaction>(id));
            if (stock_trade == null)
            {
                _notifications.AddNotification("404", "stock transaction was not found.");
                return;
            }

            await _mediator.Send(new DeleteRequest<StockTransaction>(stock_trade));
        }

        public async Task<List<StockTransactionVM>> ImportFromCEIAsync(IFormFile file, string newPath)
        {
            var negotiations = await _mediator.Send(new GetCollectionRequest<StockTransaction>());
            var stocks = await _mediator.Send(new GetCollectionRequest<Stock>());
            var negotiations_to_import = new List<StockTransaction>();

            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }

                    var last_row_num = sheet.LastRowNum - 4;
                    for (int i = 11; i <= last_row_num; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        var when = row.GetCell(1).ToString().Trim();
                        var type = row.GetCell(3).ToString().Trim().Contains("C") ? TradeTypeEnum.BUY : TradeTypeEnum.SELL;
                        var stock_symbol = row.GetCell(6).ToString().Trim();
                        var symbol = stock_symbol.EndsWith('F') ? stock_symbol.Remove(stock_symbol.Length - 1) : stock_symbol;
                        var amount = row.GetCell(8).ToString().Trim().Replace(".", "");
                        var price = row.GetCell(9).ToString().Trim();

                        if (symbol.Contains("BVMF3"))
                            symbol = "B3SA3";

                        var stock = stocks
                            .FirstOrDefault(n => n.Symbol.ToLower().Contains(symbol.ToLower()));

                        if (stock == null && symbol.Contains("BIDI11"))
                        {
                            var new_stock = new Stock()
                            {
                                Name = "Banco Inter S.A.",
                                Symbol = "BIDI11.SAO",
                                Type = StockTypeEnum.STOCK,
                                Region = null,
                                MarketOpen = new TimeSpan(10, 0, 0),
                                MarketClose = new TimeSpan(17, 30, 0),
                                Currency = "BRL"
                            };

                            stock = await _mediator.Send(new CreateRequest<Stock>(new_stock));
                        }

                        if (stock == null)
                        {
                            BestMatches result = null;
                            do
                            {
                                Thread.Sleep(60001);
                                result = await getSymbol(symbol);
                            }
                            while (result.SymbolsSearch == null);

                            if (result.SymbolsSearch == null)
                            {
                                _notifications.AddNotification("400", "Stock not found or API call frequency is 5 calls per minute and 500 calls per day " + symbol);
                            }

                            var bestMatch = result.SymbolsSearch.FirstOrDefault();

                            var new_stock = new Stock()
                            {
                                Name = bestMatch.Name,
                                Symbol = bestMatch.Symbol,
                                Type = StockTypeEnum.STOCK,
                                Region = bestMatch.Region,
                                MarketOpen = bestMatch.MarketOpen,
                                MarketClose = bestMatch.MarketClose,
                                Currency = bestMatch.Currency
                            };
                            stock = await _mediator.Send(new CreateRequest<Stock>(new_stock));
                            stocks = await _mediator.Send(new GetCollectionRequest<Stock>());
                        }

                        var exist = negotiations.Any(x => x.Amount.ToString() == amount && x.TradeType == type && x.When.Date.ToString() == when && x.Price.ToString() == price);

                        if (!exist)
                        {
                            var negotiation = new StockTransaction()
                            {
                                When = DateTime.Parse(when),
                                TradeType = type,
                                Stock = stock,
                                Amount = int.Parse(amount),
                                Price = decimal.Parse(price)
                            };

                            negotiations_to_import.Add(negotiation);

                            await _mediator.Send(new CreateRequest<StockTransaction>(negotiation));
                        }

                    }

                }
            }

            return negotiations_to_import.ToVM();
        }

        private async Task<BestMatches> getSymbol(string symbol)
        {
            return await _symbolSearchService.SearchSymbolAsync(Functions.SYMBOL_SEARCH, symbol + ".SA");
        }

    }
}
