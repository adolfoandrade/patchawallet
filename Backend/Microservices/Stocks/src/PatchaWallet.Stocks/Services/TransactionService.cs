using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace PatchaWallet.Stocks
{
    public class TransactionService : ITransactionService
    {
        private readonly IMediator _mediator;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly ISymbolSearchService _symbolSearchService;
        private readonly IUser _user;
        private readonly IUserService _userService;

        public TransactionService(IMediator mediator,
            IDomainNotificationHandler<DomainNotification> notifications,
            ISymbolSearchService symbolSearchService,
            IUser user,
            IUserService userService)
        {
            _mediator = mediator;
            _notifications = notifications;
            _symbolSearchService = symbolSearchService;
            _user = user;
            _userService = userService;
        }

        public async Task<IEnumerable<StockTransactionVM>> GetAsync()
        {
            var stock_transactions = await _mediator.Send(new GetCollectionRequest<StockTransactionVM>());
            return stock_transactions;
        }

        public async Task<StockTransactionVM> GetAsync(string id)
        {
            StockTransactionVM transaction = await _mediator.Send(new GetSingleRequest<StockTransactionVM>(id));

            if (transaction == null)
                _notifications.AddNotification("404", "stock transaction was not found.");

            return transaction;
        }

        public async Task<StockTransactionVM> CreateAsync(CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            if (!_user.IsAuthenticated())
            {
                _notifications.AddNotification("401", "User not logged in, please sign in an try again");
                return null;
            }

            var user = await _userService.GetByNameAsync(_user.Name);

            var stock = await _mediator.Send(new GetSingleRequest<StockVM>(createOrUpdateStockTransactionVM.StockId));
            var stock_transaction = new StockTransactionVM()
            {
                Id = createOrUpdateStockTransactionVM.Id,
                Stock = stock,
                Commission = createOrUpdateStockTransactionVM.Commission,
                Amount = createOrUpdateStockTransactionVM.Amount,
                Price = createOrUpdateStockTransactionVM.Price,
                When = createOrUpdateStockTransactionVM.When,
                TradeType = createOrUpdateStockTransactionVM.TradeType,
                User = user
            };

            var checkIfExists = await _mediator.Send(new CheckExistsRequest<StockTransactionVM>(stock_transaction));
            if (checkIfExists != null)
            {
                _notifications.AddNotification("400", "Already exist a transaction with the same data.");
                return checkIfExists;
            }

            stock_transaction = await _mediator.Send(new CreateRequest<StockTransactionVM>(stock_transaction));
            return stock_transaction;
        }

        public async Task<StockTransactionVM> UpdateAsync(string id, CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            if (!_user.IsAuthenticated())
            {
                _notifications.AddNotification("401", "User not logged in, please sign in an try again");
                return null;
            }

            var user = await _userService.GetByNameAsync(_user.Name);

            if (string.IsNullOrEmpty(createOrUpdateStockTransactionVM.Id))
            {
                _notifications.AddNotification("404", "Stock transaction was not found.");
                return null;
            }

            var stock = await _mediator.Send(new GetSingleRequest<StockVM>(createOrUpdateStockTransactionVM.StockId));
            var stockTransactionToUpdate = await _mediator.Send(new GetSingleRequest<StockTransactionVM>(createOrUpdateStockTransactionVM.Id));

            stockTransactionToUpdate.Stock = stock;
            stockTransactionToUpdate.Commission = createOrUpdateStockTransactionVM.Commission;
            stockTransactionToUpdate.Amount = createOrUpdateStockTransactionVM.Amount;
            stockTransactionToUpdate.Price = createOrUpdateStockTransactionVM.Price;
            stockTransactionToUpdate.When = createOrUpdateStockTransactionVM.When;
            stockTransactionToUpdate.TradeType = createOrUpdateStockTransactionVM.TradeType;

            StockTransactionVM stock_transaction;
            try
            {
                stock_transaction = await _mediator.Send(new UpdateRequest<StockTransactionVM>(id, stockTransactionToUpdate));
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("500", $"Iternal server error on try to update the Stock Transaction. Exception generated was: {ex.Message}");
                return null;
            }

            if (stock_transaction == null)
                _notifications.AddNotification("404", "Stock transaction was not found.");

            return stock_transaction;
        }

        public async Task DeleteAsync(string id)
        {
            if (!_user.IsAuthenticated())
            {
                _notifications.AddNotification("401", "User not logged in, please sign in an try again");
                return;
            }

            StockTransactionVM stock_trade = await _mediator.Send(new GetSingleRequest<StockTransactionVM>(id));
            if (stock_trade == null)
            {
                _notifications.AddNotification("404", "stock transaction was not found.");
                return;
            }

            await _mediator.Send(new DeleteRequest<StockTransactionVM>(stock_trade));
        }

        public async Task<List<StockTransactionVM>> ImportFromCEIAsync(IFormFile file)
        {
            if (!_user.IsAuthenticated())
            {
                _notifications.AddNotification("401", "User not logged in, please sign in an try again");
                return null;
            }

            var user = await _userService.GetByNameAsync(_user.Name);

            var negotiations = await _mediator.Send(new GetCollectionRequest<StockTransactionVM>());
            var stocks = await _mediator.Send(new GetCollectionRequest<StockVM>());
            var negotiations_to_import = new List<StockTransactionVM>();

            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                using (var stream = new MemoryStream())
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
                            var new_stock = new StockVM()
                            {
                                Name = "Banco Inter S.A.",
                                Symbol = "BIDI11.SAO",
                                Type = StockTypeEnum.STOCK,
                                Region = null,
                                MarketOpen = new TimeSpan(10, 0, 0),
                                MarketClose = new TimeSpan(17, 30, 0),
                                Currency = "BRL"
                            };

                            stock = await _mediator.Send(new CreateRequest<StockVM>(new_stock));
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

                            var new_stock = new StockVM()
                            {
                                Name = bestMatch.Name,
                                Symbol = bestMatch.Symbol,
                                Type = StockTypeEnum.STOCK,
                                Region = bestMatch.Region,
                                MarketOpen = bestMatch.MarketOpen,
                                MarketClose = bestMatch.MarketClose,
                                Currency = bestMatch.Currency
                            };
                            stock = await _mediator.Send(new CreateRequest<StockVM>(new_stock));
                            stocks = await _mediator.Send(new GetCollectionRequest<StockVM>());
                        }

                        var exist = negotiations.Any(x => x.Amount.ToString() == amount && x.TradeType == type && x.When.Date.ToString() == when && x.Price.ToString() == price);

                        if (!exist)
                        {
                            var negotiation = new StockTransactionVM()
                            {
                                When = DateTime.Parse(when),
                                TradeType = type,
                                Stock = stock,
                                Amount = int.Parse(amount),
                                Price = decimal.Parse(price),
                                User = user
                            };

                            negotiations_to_import.Add(negotiation);

                            await _mediator.Send(new CreateRequest<StockTransactionVM>(negotiation));
                        }

                    }

                }
            }

            return negotiations_to_import;
        }

        private async Task<BestMatches> getSymbol(string symbol)
        {
            return await _symbolSearchService.SearchSymbolAsync(Functions.SYMBOL_SEARCH, symbol + ".SA");
        }
    }
}
