using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Patcha.InvestmentWallet.Api.Interfaces.Bitcointoyou;
using Patcha.InvestmentWallet.Api.Interfaces.Braziliex;
using Patcha.InvestmentWallet.Api.Interfaces.IIIxbit;
using Patcha.InvestmentWallet.Api.Interfaces.MercadoBitcoin;
using Patcha.InvestmentWallet.Api.Interfaces.TemBTC;
using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;
using Patcha.InvestmentWallet.Core.IIIxbit.Entities.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class TradesCoinsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMercadoBitcoinService _mercadoBitcoinService;
        private readonly IBraziliexService _braziliexService;
        private readonly ITemBTCService _temBTCService;
        private readonly IBitcointoyouService _bitcointoyouService;
        private readonly IIIIxbitService _3xbitService;
        private readonly ILogger<NegotiationsController> _logger;

        public TradesCoinsController(IMediator mediator,
           IHostingEnvironment hostingEnvironment,
           IMercadoBitcoinService mercadoBitcoinService,
           IBraziliexService braziliexService,
           ITemBTCService temBTCService,
           IBitcointoyouService bitcointoyouService,
           IIIIxbitService _3xbitService,
           ILogger<NegotiationsController> logger)
        {
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _mercadoBitcoinService = mercadoBitcoinService;
            _braziliexService = braziliexService;
            _temBTCService = temBTCService;
            _bitcointoyouService = bitcointoyouService;
            this._3xbitService = _3xbitService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mercadobitcoin_order_book_task = _mercadoBitcoinService.GetOrderBookAsync();
            var braziliex_order_book_task = _braziliexService.GetOrderBookAsync();
            var temBTC_order_book_task = _temBTCService.GetOrderBookAsync();
            var bitcointoyou_order_book_task = _bitcointoyouService.GetOrderBookAsync();
            var _3xbit_order_book_task = _3xbitService.GetOrderBookAsync(IIIxbitCoins.BITCOIN);

            var best_price_to_buy = new List<BestPriceToBuyViewModel>();

            best_price_to_buy.Add(await _braziliexService.GetBestPriceToBuyAsync(braziliex_order_book_task.Result));
            best_price_to_buy.Add(await _mercadoBitcoinService.GetBestPriceToBuyAsync(mercadobitcoin_order_book_task.Result));
            best_price_to_buy.Add(await _temBTCService.GetBestPriceToBuyAsync(temBTC_order_book_task.Result));
            best_price_to_buy.Add(await _bitcointoyouService.GetBestPriceToBuyAsync(bitcointoyou_order_book_task.Result));
            best_price_to_buy.Add(await _3xbitService.GetBestPriceToBuyAsync(_3xbit_order_book_task.Result));

            best_price_to_buy = best_price_to_buy.OrderBy(p => p.Price).ToList();
            var buy_at = best_price_to_buy.FirstOrDefault();

            var best_price_to_sell = new List<BestPriceToSellViewModel>();

            best_price_to_sell.Add(await _braziliexService.GetBestPriceToSellAsync(braziliex_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add(await _mercadoBitcoinService.GetBestPriceToSellAsync(mercadobitcoin_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add(await _temBTCService.GetBestPriceToSellAsync(temBTC_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add(await _bitcointoyouService.GetBestPriceToSellAsync(bitcointoyou_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add(await _3xbitService.GetBestPriceToSellAsync(_3xbit_order_book_task.Result, buy_at.Amount));

            best_price_to_sell = best_price_to_sell.OrderByDescending(p => p.Price).ToList();
    
            var sell_at = best_price_to_sell.FirstOrDefault();
            var gainMoney = (sell_at.Valeu - buy_at.Valeu);
            var gain = ((gainMoney - (buy_at.Fee + sell_at.Fee)) * 100) / buy_at.Price;

            var response = new BuyAndSellViewModel()
            {
                BestPriceToBuyViewModel = best_price_to_buy,
                BestPriceToSellViewModel = best_price_to_sell,
                BuyAt = buy_at,
                SellAt = sell_at,
                WithDrawal = buy_at.Fee + sell_at.Fee,
                Gain = gainMoney - (buy_at.Fee + sell_at.Fee),
                PercentageGain = gain.ToString() + "%"
            };

            return Ok(response);
        }

        [HttpGet("buyandsell")]
        public async Task<IActionResult> BuyAndSell()
        {
            var mercadobitcoin_order_book = await _mercadoBitcoinService.GetOrderBookAsync();
            var braziliex_order_book = await _braziliexService.GetOrderBookAsync();

            var mercadobitcoin_sell_book = mercadobitcoin_order_book.Asks;
            var braziliex_sell_book = braziliex_order_book.Asks;
            var mercadobitcoin_buy_book = mercadobitcoin_order_book.Bids;
            var braziliex_buy_book = braziliex_order_book.Bids;

            var mercadobitcoin_best_price_to_buy = decimal.Parse(mercadobitcoin_sell_book[0, 0].ToString());
            var braziliex_best_price_to_buy = braziliex_sell_book.FirstOrDefault();
            

            var buyAndSellViewModel = new BuyAndSellViewModel();
/*
            if(mercadobitcoin_best_price_to_buy < braziliex_best_price_to_buy.Price)
            {
                buyAndSellViewModel.ExchangeToBuy = "MercadoBitcoin";
                buyAndSellViewModel.PriceToBuy = decimal.Parse(mercadobitcoin_sell_book[0, 0].ToString());
                buyAndSellViewModel.AmountToBuy = double.Parse(mercadobitcoin_sell_book[0, 1].ToString());
            }
            else
            {
                buyAndSellViewModel.ExchangeToBuy = "Braziliex";
                buyAndSellViewModel.PriceToBuy = braziliex_best_price_to_buy.Price;
                buyAndSellViewModel.AmountToBuy = braziliex_best_price_to_buy.Amount;
            }

            var mercadobitcoin_best_price_to_sell = decimal.Parse(mercadobitcoin_buy_book[0, 0].ToString());
            var braziliex_best_price_to_sell = braziliex_buy_book.FirstOrDefault();

            if (mercadobitcoin_best_price_to_sell > braziliex_best_price_to_sell.Price)
            {
                buyAndSellViewModel.ExchangeToSell = "MercadoBitcoin";
                buyAndSellViewModel.PriceToSell = decimal.Parse(mercadobitcoin_buy_book[0, 0].ToString());
                buyAndSellViewModel.AmountToSell = double.Parse(mercadobitcoin_buy_book[0, 1].ToString());
            }
            else
            {
                buyAndSellViewModel.ExchangeToSell = "Braziliex";
                buyAndSellViewModel.PriceToSell = braziliex_best_price_to_sell.Price;
                buyAndSellViewModel.AmountToSell = braziliex_best_price_to_sell.Amount;
            }

            if(buyAndSellViewModel.PriceToBuy > buyAndSellViewModel.PriceToSell)
            {
                return Ok(new BuyAndSellViewModel());
            }

            buyAndSellViewModel.Gain = (((buyAndSellViewModel.PriceToSell - buyAndSellViewModel.PriceToBuy) * 100) / buyAndSellViewModel.PriceToBuy).ToString("P");
            */
            return Ok(buyAndSellViewModel);
        }

    }

    public class BuyAndSellViewModel
    {
        public List<BestPriceToSellViewModel> BestPriceToSellViewModel { get; set; }
        public List<BestPriceToBuyViewModel> BestPriceToBuyViewModel { get; set; }
        public BestPriceToSellViewModel SellAt { get; set; }
        public BestPriceToBuyViewModel BuyAt { get; set; }
        public decimal WithDrawal { get; set; }
        public decimal Gain { get; set; }
        public string PercentageGain { get; set; }
    }
}
