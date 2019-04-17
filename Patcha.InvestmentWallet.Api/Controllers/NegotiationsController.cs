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
using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Parameters;
using Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins;
using Patcha.InvestmentWallet.Domain.CoinGecko.Parameters;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class NegotiationsController : Controller
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly HttpClient _httpClient;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICoinsService _coinsService;
        private readonly ISymbolSearchService _symbolSearchService;
        private readonly IGlobalQuoteService _globalQuoteService;
        private readonly ILogger<NegotiationsController> _logger;

        // Get the default form options so that we can use them to set the default limits for
        // request body data
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
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
            _globalQuoteService = globalQuoteService;
            _logger = logger;
        }
        #endregion

        #region Actions
        // GET api/purchases
        [HttpGet]
        public async Task<DashboardViewModel> Get(int year = 0, int month = 0, int day = 0, string stock = null)
        {
            var negotiations = await _mediator.Send(new GetCollectionRequest<Trade>());

            if (!String.IsNullOrEmpty(stock))
            {
                negotiations = negotiations.Where(p => p.InvestmentCompany.Symbol == stock);
            }

            if (year > 0)
            {
                negotiations = negotiations.Where(p => p.When.Year == year);

                if (month > 0)
                {
                    negotiations = negotiations.Where(p => p.When.Month == month);

                    if (day > 0)
                    {
                        negotiations = negotiations.Where(p => p.When.Day == day);
                    }
                }
            }

            var coins_negociations = negotiations.Where(c => c.InvestmentCompany.Type == InvestimentTypeEnum.CRYPTOCURRENCY);
            var coins = coins_negociations.Select(c => c.InvestmentCompany.Name.Replace(" ", "-").ToLower()).Distinct().ToArray();
            var coin_average_info = await CoinAveragePriceAsync(coins_negociations);

            var stock_negotiations = negotiations.Where(s => s.InvestmentCompany.Type != InvestimentTypeEnum.CRYPTOCURRENCY);
            var stock_prices_info = await StockAveragePriceAsync(stock_negotiations);

            var vm = new DashboardViewModel()
            {
                StockPriceInfoViewModel = stock_prices_info,
                CoinsPricesInfoViewModel = coin_average_info
            };

            return vm;
        }

        [HttpPost("importfromcei")]
        [DisableFormValueModelBinding]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportFromCEI()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            var negotiations = await _mediator.Send(new GetCollectionRequest<Trade>());
            var companies = await _mediator.Send(new GetCollectionRequest<InvestmentCompany>());
            var negotiations_to_import = new List<Trade>();

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

                        var company = companies
                            .Where(n => n.Symbol.ToLower().Contains(symbol.ToLower()))
                            .FirstOrDefault();

                        if (company == null)
                        {
                            if (symbol.Contains("BIDI11"))
                            {
                                var new_company = new InvestmentCompany()
                                {
                                    Name = "Banco Inter S.A.",
                                    Symbol = "BIDI11.SAO",
                                    Type = InvestimentTypeEnum.STOCK,
                                    Region = null,
                                    MarketOpen = new TimeSpan(10, 0, 0),
                                    MarketClose = new TimeSpan(17, 30, 0),
                                    Currency = "BRL"
                                };

                                company = await _mediator.Send(new CreateRequest<InvestmentCompany>(new_company));
                            }
                        }

                        if (company == null)
                        {
                            BestMatches result = await _symbolSearchService.SearchSymbolAsync(Functions.SYMBOL_SEARCH, symbol + ".SA");

                            if (result.SymbolsSearch == null)
                            {
                                ModelState.AddModelError("Message", "Stock not found or API call frequency is 5 calls per minute and 500 calls per day " + symbol);
                                return BadRequest(ModelState);
                            }

                            var bestMatch = result.SymbolsSearch.FirstOrDefault();

                            var new_company = new InvestmentCompany()
                            {
                                Name = bestMatch.Name,
                                Symbol = bestMatch.Symbol,
                                Type = InvestimentTypeEnum.STOCK,
                                Region = bestMatch.Region,
                                MarketOpen = bestMatch.MarketOpen,
                                MarketClose = bestMatch.MarketClose,
                                Currency = bestMatch.Currency
                            };
                            company = await _mediator.Send(new CreateRequest<InvestmentCompany>(new_company));
                            companies = await _mediator.Send(new GetCollectionRequest<InvestmentCompany>());
                        }

                        var negotiation = new Trade()
                        {
                            When = DateTime.Parse(when),
                            PurchaseType = type,
                            InvestmentCompany = company,
                            Amount = int.Parse(amount),
                            Price = decimal.Parse(price)
                        };

                        negotiations_to_import.Add(negotiation);

                        await _mediator.Send(new CreateRequest<Trade>(negotiation));
                    }

                }
            }
            return Ok(negotiations_to_import);

        }

        // GET api/purchases/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> Get(string id)
        {
            Trade purchase = await _mediator.Send(new GetSingleRequest<Trade>(id));
            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // POST api/purchases
        [HttpPost]
        public async Task<IActionResult> Post(Trade purchase)
        {
            //if (await _mediator.Send(new CheckExistsRequest<Purchase>(purchase.Id)))
            //{
            //    return StatusCode((int)HttpStatusCode.Conflict);
            //}

            purchase = await _mediator.Send(new CreateRequest<Trade>(purchase));

            return CreatedAtAction(nameof(Get), new { purchase.Id }, purchase);
        }

        // PUT api/purchases/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Trade>> Put(string id, Trade update)
        {
            //if (await _mediator.Send(new CheckExistsRequest<Purchase>(update.Name, id)))
            //{
            //    return StatusCode((int)HttpStatusCode.Conflict);
            //}

            Trade purchase = null;
            try
            {
                purchase = await _mediator.Send(CreateUpdateRequest(id, update));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // DELETE api/purchases/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Trade purchase = await _mediator.Send(new GetSingleRequest<Trade>(id));
            if (purchase == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteRequest<Trade>(purchase));

            return NoContent();
        }
        #endregion

        #region Methods
        private UpdateRequest<T> CreateUpdateRequest<T>(string id, T update)
        {
            return new UpdateRequest<T>(id, update);
        }

        private async Task<List<StocksPricesInfoViewModel>> StockAveragePriceAsync(IEnumerable<Trade> negociations)
        {
            var investment_symbols = negociations.Select(s => s.InvestmentCompany.Symbol).Distinct();

            foreach (var investment_symbol in investment_symbols)
            {
                GlobalQuoteResult result = await _globalQuoteService.GetGlobalQuote(Functions.GLOBAL_QUOTE, investment_symbol);
            }

            var investment_ids = negociations.Select(s => s.InvestmentCompany.Id).Distinct();

            var stockPriceInfoVM = new List<StocksPricesInfoViewModel>();

            foreach (var investment_id in investment_ids)
            {
                var negociations_histories = negociations.Where(p => p.InvestmentCompany.Id == investment_id).OrderBy(p => p.When);

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
                    stockPriceInfoVM.Add(new StocksPricesInfoViewModel()
                    {
                        InvestmentCompany = negociations_histories.Select(p => p.InvestmentCompany).FirstOrDefault(),
                        TotalPrice = total_price,
                        TotalAmount = total_amount,
                        AveragePrice = total_price / (decimal)total_amount,
                        CurrentPrice = 0,
                        TotalCurrentPrice = 0
                    });
                }
            }

            return stockPriceInfoVM;
        }

        private async Task<List<CoinsPricesInfoViewModel>> CoinAveragePriceAsync(IEnumerable<Trade> negociations)
        {
            var coins = negociations.Select(c => c.InvestmentCompany.Name.Replace(" ", "-").ToLower()).Distinct().ToArray();

            List<CoinMarkets> coin_markets = await _coinsService.GetCoinMarketsAsync(Currency.Brl, coins, OrderField.MarketCapDesc, 100, 1, false);

            var investment_ids = negociations.Select(s => s.InvestmentCompany.Id).Distinct();

            var coinsPricesInfoVM = new List<CoinsPricesInfoViewModel>();

            foreach (var investment_id in investment_ids)
            {
                var negociations_histories = negociations.Where(p => p.InvestmentCompany.Id == investment_id).OrderBy(p => p.When);

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
                    var coin = negociations_histories.Select(p => p.InvestmentCompany).FirstOrDefault();
                    var coin_market = coin_markets.Where(c => c.Name.Equals(coin.Name)).FirstOrDefault();
                    var total_current_price = coin_market.CurrentPrice.Brl * (decimal)total_amount;
                    coinsPricesInfoVM.Add(new CoinsPricesInfoViewModel()
                    {
                        InvestmentCompany = coin,
                        TotalPrice = total_price,
                        TotalAmount = total_amount,
                        AveragePrice = total_price / (decimal)total_amount,
                        CurrentPrice = coin_market.CurrentPrice.Brl,
                        TotalCurrentPrice = total_current_price,
                        Difference = total_current_price - total_price
                    });
                }
            }

            return coinsPricesInfoVM;
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }

        #endregion
    }

    public class StocksPricesInfoViewModel
    {
        public InvestmentCompany InvestmentCompany { get; set; }
        public double TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalCurrentPrice { get; set; }
        public decimal Difference { get; set; }
    }

    public class CoinsPricesInfoViewModel : StocksPricesInfoViewModel
    {
    }

    public class CryptoPriceInfoViewModel
    {
        public InvestmentCompany InvestmentCompany { get; set; }
        public double TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalCurrentPrice { get; set; }
    }

    public class DashboardViewModel
    {
        [JsonProperty("StocksPrices")]
        public List<StocksPricesInfoViewModel> StockPriceInfoViewModel { get; set; }

        [JsonProperty("CoinsPrices")]
        public List<CoinsPricesInfoViewModel> CoinsPricesInfoViewModel { get; set; }
    }
}
