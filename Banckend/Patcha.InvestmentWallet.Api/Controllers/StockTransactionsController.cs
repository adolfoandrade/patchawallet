using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Patcha.InvestmentWallet.Api.Extensions;
using Patcha.InvestmentWallet.Api.Interfaces;
using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;
using Patcha.InvestmentWallet.Domain.CoinGecko.Entities.Reponse.Coins;
using Patcha.InvestmentWallet.Domain.CoinGecko.Parameters;
using Patcha.InvestmentWallet.Domain.DomainNotification;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionsController : ControllerBase
    {
        #region Fields
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICoinsService _coinsService;
        private readonly ITransactionService _transactionService;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly ILogger<StockTransactionsController> _logger;
        #endregion

        #region Constructor
        public StockTransactionsController(
            IHostingEnvironment hostingEnvironment,
            ICoinsService coinsService,
            ITransactionService transactionService,
            IDomainNotificationHandler<DomainNotification> notifications,
            ILogger<StockTransactionsController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _coinsService = coinsService;
            _transactionService = transactionService;
            _notifications = notifications;
            _logger = logger;
        }
        #endregion

        #region Actions

        [HttpPost("CEI")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> ImportFromCEI()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            var negotiations_to_import = await _transactionService.ImportFromCEIAsync(file, newPath);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(negotiations_to_import);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var stock_transaction = await _transactionService.GetAsync(id);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(stock_transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            var stock_trasaction = await _transactionService.CreateAsync(createOrUpdateStockTransactionVM);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(stock_trasaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            createOrUpdateStockTransactionVM.Id = id;
            var stock_trasaction = await _transactionService.CreateAsync(createOrUpdateStockTransactionVM);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(stock_trasaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _transactionService.DeleteAsync(id);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok();
        }

        #endregion

        #region Methods

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