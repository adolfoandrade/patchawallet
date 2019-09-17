using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Patcha.Coins;
using Patcha.InvestmentWallet.Api.Interfaces.Bitblue;
using Patcha.InvestmentWallet.Api.Interfaces.Bitcointoyou;
using Patcha.InvestmentWallet.Api.Interfaces.Bitcointrade;
using Patcha.InvestmentWallet.Api.Interfaces.Braziliex;
using Patcha.InvestmentWallet.Api.Interfaces.Flowbtc;
using Patcha.InvestmentWallet.Api.Interfaces.IIIxbit;
using Patcha.InvestmentWallet.Api.Interfaces.MercadoBitcoin;
using Patcha.InvestmentWallet.Api.Interfaces.Negociecoins;
using Patcha.InvestmentWallet.Api.Interfaces.TemBTC;
using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;

namespace Patcha.InvestmentWallet.Api.Controllers {
    
    [Route ("api/[controller]")]
    [ApiController]
    public class TradesCoinsController : ControllerBase {
        private readonly IMediator _mediator;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMercadoBitcoinService _mercadoBitcoinService;
        private readonly IBraziliexService _braziliexService;
        private readonly ITemBTCService _temBTCService;
        private readonly IBitcointoyouService _bitcointoyouService;
        private readonly IIIIxbitService _3xbitService;
        private readonly IBitblueService _bitblueService;
        private readonly INegociecoinsService _negociecoinsService;
        private readonly IBitcointradeService _bitcointradeService;
        private readonly IFlowbtcService _flowbtcService;
        private readonly ILogger<NegotiationsController> _logger;

        public TradesCoinsController (IMediator mediator,
            IHostingEnvironment hostingEnvironment,
            IMercadoBitcoinService mercadoBitcoinService,
            IBraziliexService braziliexService,
            ITemBTCService temBTCService,
            IBitcointoyouService bitcointoyouService,
            IIIIxbitService _3xbitService,
            IBitblueService bitblueService,
            INegociecoinsService negociecoinsService,
            IBitcointradeService bitcointradeService,
            IFlowbtcService flowbtcService,
            ILogger<NegotiationsController> logger) {
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _mercadoBitcoinService = mercadoBitcoinService;
            _braziliexService = braziliexService;
            _temBTCService = temBTCService;
            _bitcointoyouService = bitcointoyouService;
            this._3xbitService = _3xbitService;
            _bitblueService = bitblueService;
            _negociecoinsService = negociecoinsService;
            _bitcointradeService = bitcointradeService;
            _flowbtcService = flowbtcService;
            _logger = logger;
        }

        private async Task<BuyAndSellViewModel> buyAndSellBTC (decimal min_value) {
            var mercadobitcoin_order_book_task = _mercadoBitcoinService.GetOrderBookAsync ();
            var braziliex_order_book_task = _braziliexService.GetOrderBookAsync ();
            var temBTC_order_book_task = _temBTCService.GetOrderBookAsync ();
            //var bitcointoyou_order_book_task = _bitcointoyouService.GetOrderBookAsync();
            var _3xbit_order_book_task = _3xbitService.GetOrderBookAsync (IIIxbitCoins.BITCOIN);
            var bitblue_order_book_task = _bitblueService.GetOrderBookAsync ();
            var negociecoins_order_book_task = _negociecoinsService.GetOrderBookAsync ();
            var bitcointrade_order_book_task = _bitcointradeService.GetOrderBookAsync ();
            //var flowbtc_order_book_task = _flowbtcService.GetOrderBookAsync(CoinsFlowbtc.BITCOIN);

            var best_price_to_buy = new List<BestPriceToBuyViewModel> ();

            best_price_to_buy.Add (await _braziliexService.GetBestPriceToBuyAsync (braziliex_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _mercadoBitcoinService.GetBestPriceToBuyAsync (mercadobitcoin_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _temBTCService.GetBestPriceToBuyAsync (temBTC_order_book_task.Result, min_value));
            // best_price_to_buy.Add(await _bitcointoyouService.GetBestPriceToBuyAsync(bitcointoyou_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _3xbitService.GetBestPriceToBuyAsync (_3xbit_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _bitblueService.GetBestPriceToBuyAsync (bitblue_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _bitcointradeService.GetBestPriceToBuyAsync (bitcointrade_order_book_task.Result, min_value));
            //best_price_to_buy.Add(await _flowbtcService.GetBestPriceToBuyAsync(flowbtc_order_book_task.Result, min_value));
            //try
            //{
            //    best_price_to_buy.Add(await _negociecoinsService.GetBestPriceToBuyAsync(negociecoins_order_book_task.Result));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}

            best_price_to_buy = best_price_to_buy.OrderBy (p => p.Price).ToList ();
            var buy_at = best_price_to_buy.FirstOrDefault ();

            var best_price_to_sell = new List<BestPriceToSellViewModel> ();

            best_price_to_sell.Add (await _braziliexService.GetBestPriceToSellAsync (braziliex_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _mercadoBitcoinService.GetBestPriceToSellAsync (mercadobitcoin_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _temBTCService.GetBestPriceToSellAsync (temBTC_order_book_task.Result, buy_at.Amount));
            // best_price_to_sell.Add(await _bitcointoyouService.GetBestPriceToSellAsync(bitcointoyou_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _3xbitService.GetBestPriceToSellAsync (_3xbit_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _bitblueService.GetBestPriceToSellAsync (bitblue_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _bitcointradeService.GetBestPriceToSellAsync (bitcointrade_order_book_task.Result, buy_at.Amount));
            //best_price_to_sell.Add(await _flowbtcService.GetBestPriceToSellAsync(flowbtc_order_book_task.Result, buy_at.Amount));
            //try
            //{
            //    best_price_to_sell.Add(await _negociecoinsService.GetBestPriceToSellAsync(negociecoins_order_book_task.Result, buy_at.Amount));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}          

            best_price_to_sell = best_price_to_sell.OrderByDescending (p => p.Price).ToList ();

            var sell_at = best_price_to_sell.FirstOrDefault ();
            var gainMoney = (sell_at.Valeu - buy_at.Valeu);
            var gain = ((gainMoney - (buy_at.Fee + sell_at.Fee)) * 100) / buy_at.Price;

            var response = new BuyAndSellViewModel () {
                BestPriceToBuyViewModel = best_price_to_buy,
                BestPriceToSellViewModel = best_price_to_sell,
                BuyAt = buy_at,
                SellAt = sell_at,
                WithDrawal = buy_at.Fee + sell_at.Fee,
                Gain = gainMoney - (buy_at.Fee + sell_at.Fee),
                PercentageGain = gain.ToString () + "%"
            };

            return response;
        }

        private async Task<BuyAndSellViewModel> buyAndSellLTC (decimal min_value) {
            var mercadobitcoin_order_book_task = _mercadoBitcoinService.GetOrderBookAsync (CoinsMercadoBitcoin.LITECOIN);
            var braziliex_order_book_task = _braziliexService.GetOrderBookAsync (CoinsBraziliex.LITECOIN);
            var temBTC_order_book_task = _temBTCService.GetOrderBookAsync (CoinsTemBTC.LITECOIN);
            var _3xbit_order_book_task = _3xbitService.GetOrderBookAsync (IIIxbitCoins.LITECOIN);
            var negociecoins_order_book_task = _negociecoinsService.GetOrderBookAsync (CoinsNegociecoins.LITECOIN);
            var flowbtc_order_book_task = _flowbtcService.GetOrderBookAsync (CoinsFlowbtc.LITECOIN);

            var best_price_to_buy = new List<BestPriceToBuyViewModel> ();

            best_price_to_buy.Add (await _braziliexService.GetBestPriceToBuyAsync (braziliex_order_book_task.Result));
            best_price_to_buy.Add (await _mercadoBitcoinService.GetBestPriceToBuyAsync (mercadobitcoin_order_book_task.Result));
            best_price_to_buy.Add (await _temBTCService.GetBestPriceToBuyAsync (temBTC_order_book_task.Result));
            best_price_to_buy.Add (await _3xbitService.GetBestPriceToBuyAsync (_3xbit_order_book_task.Result));
            best_price_to_buy.Add (await _negociecoinsService.GetBestPriceToBuyAsync (negociecoins_order_book_task.Result));
            best_price_to_buy.Add (await _flowbtcService.GetBestPriceToBuyAsync (flowbtc_order_book_task.Result, min_value));

            best_price_to_buy = best_price_to_buy.OrderBy (p => p.Price).ToList ();
            var buy_at = best_price_to_buy.FirstOrDefault ();

            var best_price_to_sell = new List<BestPriceToSellViewModel> ();

            best_price_to_sell.Add (await _braziliexService.GetBestPriceToSellAsync (braziliex_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _mercadoBitcoinService.GetBestPriceToSellAsync (mercadobitcoin_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _temBTCService.GetBestPriceToSellAsync (temBTC_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _3xbitService.GetBestPriceToSellAsync (_3xbit_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _negociecoinsService.GetBestPriceToSellAsync (negociecoins_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _flowbtcService.GetBestPriceToSellAsync (flowbtc_order_book_task.Result, buy_at.Amount));

            best_price_to_sell = best_price_to_sell.OrderByDescending (p => p.Price).ToList ();

            var sell_at = best_price_to_sell.FirstOrDefault ();
            var gainMoney = (sell_at.Valeu - buy_at.Valeu);
            var gain = ((gainMoney - (buy_at.Fee + sell_at.Fee)) * 100) / buy_at.Price;

            var response = new BuyAndSellViewModel () {
                BestPriceToBuyViewModel = best_price_to_buy,
                BestPriceToSellViewModel = best_price_to_sell,
                BuyAt = buy_at,
                SellAt = sell_at,
                WithDrawal = buy_at.Fee + sell_at.Fee,
                Gain = gainMoney - (buy_at.Fee + sell_at.Fee),
                PercentageGain = gain.ToString () + "%"
            };

            return response;
        }

        private async Task<BuyAndSellViewModel> buyAndSellETH (decimal min_value) {
            var mercadobitcoin_order_book_task = _mercadoBitcoinService.GetOrderBookAsync (CoinsMercadoBitcoin.ETHERUM);
            var braziliex_order_book_task = _braziliexService.GetOrderBookAsync (CoinsBraziliex.ETHERUM);
            var _3xbit_order_book_task = _3xbitService.GetOrderBookAsync (IIIxbitCoins.ETHERUM);
            var bitblue_order_book_task = _bitblueService.GetOrderBookAsync (CoinsBitblue.ETHERUM);
            var negociecoins_order_book_task = _negociecoinsService.GetOrderBookAsync (CoinsNegociecoins.ETHERUM);
            var flowbtc_order_book_task = _flowbtcService.GetOrderBookAsync (CoinsFlowbtc.ETHERUM);

            var best_price_to_buy = new List<BestPriceToBuyViewModel> ();

            best_price_to_buy.Add (await _braziliexService.GetBestPriceToBuyAsync (braziliex_order_book_task.Result));
            best_price_to_buy.Add (await _mercadoBitcoinService.GetBestPriceToBuyAsync (mercadobitcoin_order_book_task.Result));
            best_price_to_buy.Add (await _bitblueService.GetBestPriceToBuyAsync (bitblue_order_book_task.Result));
            best_price_to_buy.Add (await _3xbitService.GetBestPriceToBuyAsync (_3xbit_order_book_task.Result));
            best_price_to_buy.Add (await _flowbtcService.GetBestPriceToBuyAsync (flowbtc_order_book_task.Result, min_value));
            //best_price_to_buy.Add(await _negociecoinsService.GetBestPriceToBuyAsync(negociecoins_order_book_task.Result));

            best_price_to_buy = best_price_to_buy.OrderBy (p => p.Price).ToList ();
            var buy_at = best_price_to_buy.FirstOrDefault ();

            var best_price_to_sell = new List<BestPriceToSellViewModel> ();

            best_price_to_sell.Add (await _braziliexService.GetBestPriceToSellAsync (braziliex_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _mercadoBitcoinService.GetBestPriceToSellAsync (mercadobitcoin_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _bitblueService.GetBestPriceToSellAsync (bitblue_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _3xbitService.GetBestPriceToSellAsync (_3xbit_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _flowbtcService.GetBestPriceToSellAsync (flowbtc_order_book_task.Result, buy_at.Amount));
            //best_price_to_sell.Add(await _negociecoinsService.GetBestPriceToSellAsync(negociecoins_order_book_task.Result, buy_at.Amount));            

            best_price_to_sell = best_price_to_sell.OrderByDescending (p => p.Price).ToList ();

            var sell_at = best_price_to_sell.FirstOrDefault ();
            var gainMoney = (sell_at.Valeu - buy_at.Valeu);
            var gain = ((gainMoney - (buy_at.Fee + sell_at.Fee)) * 100) / buy_at.Price;

            var response = new BuyAndSellViewModel () {
                BestPriceToBuyViewModel = best_price_to_buy,
                BestPriceToSellViewModel = best_price_to_sell,
                BuyAt = buy_at,
                SellAt = sell_at,
                WithDrawal = buy_at.Fee + sell_at.Fee,
                Gain = gainMoney - (buy_at.Fee + sell_at.Fee),
                PercentageGain = gain.ToString () + "%"
            };

            return response;
        }

        private async Task<BuyAndSellViewModel> buyAndSellXRP (decimal min_value) {
            var mercadobitcoin_order_book_task = _mercadoBitcoinService.GetOrderBookAsync (CoinsMercadoBitcoin.RIPPLE);
            var braziliex_order_book_task = _braziliexService.GetOrderBookAsync (CoinsBraziliex.RIPPLE);
            var flowbtc_order_book_task = _flowbtcService.GetOrderBookAsync (CoinsFlowbtc.RIPPLE);

            var best_price_to_buy = new List<BestPriceToBuyViewModel> ();

            best_price_to_buy.Add (await _braziliexService.GetBestPriceToBuyAsync (braziliex_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _mercadoBitcoinService.GetBestPriceToBuyAsync (mercadobitcoin_order_book_task.Result, min_value));
            best_price_to_buy.Add (await _flowbtcService.GetBestPriceToBuyAsync (flowbtc_order_book_task.Result, min_value));
            //try
            //{
            //    best_price_to_buy.Add(await _negociecoinsService.GetBestPriceToBuyAsync(negociecoins_order_book_task.Result));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}

            best_price_to_buy = best_price_to_buy.OrderBy (p => p.Price).ToList ();
            var buy_at = best_price_to_buy.FirstOrDefault ();

            var best_price_to_sell = new List<BestPriceToSellViewModel> ();

            best_price_to_sell.Add (await _braziliexService.GetBestPriceToSellAsync (braziliex_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _mercadoBitcoinService.GetBestPriceToSellAsync (mercadobitcoin_order_book_task.Result, buy_at.Amount));
            best_price_to_sell.Add (await _flowbtcService.GetBestPriceToSellAsync (flowbtc_order_book_task.Result, buy_at.Amount));
            //try
            //{
            //    best_price_to_sell.Add(await _negociecoinsService.GetBestPriceToSellAsync(negociecoins_order_book_task.Result, buy_at.Amount));
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex.Message);
            //}          

            best_price_to_sell = best_price_to_sell.OrderByDescending (p => p.Price).ToList ();

            var sell_at = best_price_to_sell.FirstOrDefault ();
            var gainMoney = (sell_at.Valeu - buy_at.Valeu);
            var gain = ((gainMoney - (buy_at.Fee + sell_at.Fee)) * 100) / buy_at.Price;

            var response = new BuyAndSellViewModel () {
                BestPriceToBuyViewModel = best_price_to_buy,
                BestPriceToSellViewModel = best_price_to_sell,
                BuyAt = buy_at,
                SellAt = sell_at,
                WithDrawal = buy_at.Fee + sell_at.Fee,
                Gain = gainMoney - (buy_at.Fee + sell_at.Fee),
                PercentageGain = gain.ToString () + "%"
            };

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> Get (string coin, decimal minValue) {
            BuyAndSellViewModel response = null;

            switch (coin) {
                case "ltc":
                    response = await buyAndSellLTC (minValue);
                    break;
                case "eth":
                    response = await buyAndSellETH (minValue);
                    break;
                case "xrp":
                    response = await buyAndSellXRP (minValue);
                    break;
                default:
                    response = await buyAndSellBTC (minValue);
                    break;
            }

            if (response.Gain < 0) {
                return Ok (new { Message = "No opportunity trade was found." });
            }

            return Ok (response);
        }

        [HttpGet ("buyandsell")]
        public async Task<IActionResult> BuyAndSell () {
            var mercadobitcoin_order_book = await _mercadoBitcoinService.GetOrderBookAsync ();
            var braziliex_order_book = await _braziliexService.GetOrderBookAsync ();

            var mercadobitcoin_sell_book = mercadobitcoin_order_book.Asks;
            var braziliex_sell_book = braziliex_order_book.Asks;
            var mercadobitcoin_buy_book = mercadobitcoin_order_book.Bids;
            var braziliex_buy_book = braziliex_order_book.Bids;

            var mercadobitcoin_best_price_to_buy = decimal.Parse (mercadobitcoin_sell_book[0, 0].ToString ());
            var braziliex_best_price_to_buy = braziliex_sell_book.FirstOrDefault ();

            var buyAndSellViewModel = new BuyAndSellViewModel ();
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
            return Ok (buyAndSellViewModel);
        }

    }

    public class BuyAndSellViewModel {
        public List<BestPriceToSellViewModel> BestPriceToSellViewModel { get; set; }
        public List<BestPriceToBuyViewModel> BestPriceToBuyViewModel { get; set; }
        public BestPriceToSellViewModel SellAt { get; set; }
        public BestPriceToBuyViewModel BuyAt { get; set; }
        public decimal WithDrawal { get; set; }
        public decimal Gain { get; set; }
        public string PercentageGain { get; set; }
    }
}