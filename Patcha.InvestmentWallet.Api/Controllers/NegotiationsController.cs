using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Patcha.InvestmentWallet.Api.Bots;
using Patcha.InvestmentWallet.Api.Extensions;
using Patcha.InvestmentWallet.Api.Interfaces.AlphaVantage;
using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Parameters;
using Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins;
using Patcha.InvestmentWallet.Domain.CoinGecko.Parameters;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class NegotiationsController : ControllerBase
    {

        #region Fields
        private readonly IMediator _mediator;
        private readonly HttpClient _httpClient;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICoinsService _coinsService;
        private readonly ISymbolSearchService _symbolSearchService;
        private readonly ILogger<NegotiationsController> _logger;

        #endregion

        #region Constructor
        public NegotiationsController(IMediator mediator,
            HttpClient httpClient,
            IHostingEnvironment hostingEnvironment,
            ICoinsService coinsService,
            ISymbolSearchService symbolSearchService,
            IGlobalQuoteService globalQuoteService,
            ILogger<NegotiationsController> logger)
        {
            _mediator = mediator;
            _httpClient = httpClient;
            _hostingEnvironment = hostingEnvironment;
            _coinsService = coinsService;
            _symbolSearchService = symbolSearchService;
            _logger = logger;
        }
        #endregion

        #region Actions

        [HttpGet]
        public async Task<DashboardViewModel> Get(int year = 0, int month = 0, int day = 0, string stock = null)
        {
            var coins_negotiations = await _mediator.Send(new GetCollectionRequest<CoinTrade>());
            var stock_negotiations = await _mediator.Send(new GetCollectionRequest<StockTrade>());

            if (!String.IsNullOrEmpty(stock))
                stock_negotiations = stock_negotiations.Where(p => p.Stock.Symbol == stock);

            if (year > 0)
            {
                stock_negotiations = stock_negotiations.Where(p => p.When.Year == year);
                coins_negotiations = coins_negotiations.Where(p => p.When.Year == year);

                if (month > 0)
                {
                    stock_negotiations = stock_negotiations.Where(p => p.When.Month == month);
                    coins_negotiations = coins_negotiations.Where(p => p.When.Month == month);

                    if (day > 0)
                    {
                        stock_negotiations = stock_negotiations.Where(p => p.When.Day == day);
                        coins_negotiations = coins_negotiations.Where(p => p.When.Day == day);
                    }
                }
            }

            var coin_average_info = await CoinAveragePriceAsync(coins_negotiations);
            var stock_prices_info = await StockAveragePriceAsync(stock_negotiations);

            var vm = new DashboardViewModel()
            {
                StockPriceInfoViewModel = stock_prices_info,
                CoinsPricesInfoViewModel = coin_average_info
            };

            return vm;
        }

        [HttpPost("CEI")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> ImportFromCEI()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            var negotiations = await _mediator.Send(new GetCollectionRequest<StockTrade>());
            var stocks = await _mediator.Send(new GetCollectionRequest<Stock>());
            var negotiations_to_import = new List<StockTrade>();

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
                                ModelState.AddModelError("Message", "Stock not found or API call frequency is 5 calls per minute and 500 calls per day " + symbol);
                                BadRequest(ModelState);
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

                        if(!exist)
                        {
                            var negotiation = new StockTrade()
                            {
                                When = DateTime.Parse(when),
                                TradeType = type,
                                Stock = stock,
                                Amount = int.Parse(amount),
                                Price = decimal.Parse(price)
                            };

                            negotiations_to_import.Add(negotiation);

                            await _mediator.Send(new CreateRequest<StockTrade>(negotiation));
                        }
                        
                    }

                }
            }
            return Ok(negotiations_to_import);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            StockTrade stock_trade = await _mediator.Send(new GetSingleRequest<StockTrade>(id));
            if (stock_trade == null)
            {
                return NotFound();
            }

            return Ok(stock_trade);
        }

        [HttpPost]
        public async Task<IActionResult> Post(StockTrade stock_trade)
        {
            stock_trade = await _mediator.Send(new CreateRequest<StockTrade>(stock_trade));

            return CreatedAtAction(nameof(Get), new { stock_trade.Id }, stock_trade);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, StockTrade update)
        {
            StockTrade stock_trade = null;
            try
            {
                stock_trade = await _mediator.Send(CreateUpdateRequest(id, update));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }

            if (stock_trade == null)
            {
                return NotFound();
            }

            return Ok(stock_trade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            StockTrade stock_trade = await _mediator.Send(new GetSingleRequest<StockTrade>(id));
            if (stock_trade == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteRequest<StockTrade>(stock_trade));

            return Ok();
        }

        #endregion

        #region Methods
        private async Task<BestMatches> getSymbol(string symbol)
        {
            return await _symbolSearchService.SearchSymbolAsync(Functions.SYMBOL_SEARCH, symbol + ".SA");
        }

        private UpdateRequest<T> CreateUpdateRequest<T>(string id, T update)
        {
            return new UpdateRequest<T>(id, update);
        }

        private Task<List<StocksPricesInfoViewModel>> StockAveragePriceAsync(IEnumerable<StockTrade> negociations)
        {
            return Task.Factory.StartNew(() =>
            {
                var investment_ids = negociations.Select(s => s.Stock.Id).Distinct();
                var stockPriceInfoVM = new List<StocksPricesInfoViewModel>();

                foreach (var investment_id in investment_ids)
                {
                    var negociations_histories = negociations.Where(p => p.Stock.Id == investment_id).OrderBy(p => p.When);
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

                        if(quote == null)
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

        private async Task<List<CoinsPricesInfoViewModel>> CoinAveragePriceAsync(IEnumerable<CoinTrade> negociations)
        {
            var coins = negociations.Select(c => c.Coin.Name.Replace(" ", "-").ToLower()).Distinct().ToArray();

            List<CoinMarkets> coin_markets = await _coinsService.GetCoinMarketsAsync(Currency.Brl, coins, OrderField.MarketCapDesc, 100, 1, false);

            var investment_ids = negociations.Select(s => s.Id).Distinct();

            var coinsPricesInfoVM = new List<CoinsPricesInfoViewModel>();

            foreach (var investment_id in investment_ids)
            {
                var negociations_histories = negociations.Where(p => p.Id == investment_id).OrderBy(p => p.When);

                decimal total_price = 0;
                foreach (var purchase_history in negociations_histories)
                {
                    if (purchase_history.PurchaseType == TradeTypeEnum.SELL)
                        total_price -= (decimal)purchase_history.Amount * purchase_history.Price;
                    else
                        total_price += (decimal)purchase_history.Amount * purchase_history.Price;
                }

                var total_amount = negociations_histories.Where(p => p.PurchaseType == TradeTypeEnum.BUY).Sum(p => p.Amount) - negociations_histories.Where(p => p.PurchaseType == TradeTypeEnum.SELL).Sum(p => p.Amount);
                if (total_amount > 0)
                {
                    var coin = negociations_histories.Select(p => p.Coin).FirstOrDefault();
                    var coin_market = coin_markets.Where(c => c.Name.Equals(coin.Name)).FirstOrDefault();
                    //var total_current_price = coin_market.CurrentPrice.Brl * (decimal)total_amount;
                    var total_current_price = (decimal)total_amount;
                    coinsPricesInfoVM.Add(new CoinsPricesInfoViewModel()
                    {
                        Coin = coin,
                        TotalPrice = total_price,
                        TotalAmount = total_amount,
                        AveragePrice = total_price / (decimal)total_amount,
                        CurrentPrice = total_current_price,
                        TotalCurrentPrice = total_current_price,
                        Difference = total_current_price - total_price
                    });
                }
            }

            return coinsPricesInfoVM;
        }

        #endregion
    }

}