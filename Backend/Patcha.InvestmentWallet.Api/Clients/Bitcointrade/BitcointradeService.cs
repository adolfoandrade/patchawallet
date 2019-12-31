﻿using Patcha.Coins;
using Patcha.InvestmentWallet.Api.Interfaces.Bitcointrade;
using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Services.Bitcointrade
{
    public class BitcointradeService : BaseApi, IBitcointradeService
    {
        public BitcointradeService(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<BestPriceToBuyViewModel> GetBestPriceToBuyAsync(BitcointradeOrderBook orderBook, decimal min_value = 2000)
        {
            return Task.Factory.StartNew(() => {
                double transaction_fee_percent = (0.5000 / 100);
                var best_price_to_buy_vm = new BestPriceToBuyViewModel();
                var prices_to_buy = orderBook.Data.Asks;
                var best_price_to_buy = prices_to_buy.Where(p => ((decimal)p.Amount * p.Price) > min_value).FirstOrDefault();

                if (best_price_to_buy != null)
                {
                    best_price_to_buy_vm.Exchange = "Bitcointrade";
                    best_price_to_buy_vm.Amount = (double)(min_value / best_price_to_buy.Price);
                    best_price_to_buy_vm.Valeu = (decimal)best_price_to_buy_vm.Amount * best_price_to_buy.Price;
                    best_price_to_buy_vm.Price = best_price_to_buy.Price;
                    var fee = best_price_to_buy_vm.Valeu * (decimal)transaction_fee_percent;
                    best_price_to_buy_vm.Fee = fee;
                }

                return best_price_to_buy_vm;
            });
        }

        public Task<BestPriceToSellViewModel> GetBestPriceToSellAsync(BitcointradeOrderBook orderBook, double btc_amount)
        {
            return Task.Factory.StartNew(() => {
                decimal min_value = 2000;
                double withdrawal_fee_percent = (0.75 / 100);
                decimal withdrawal_fee_brl = 9;
                var best_price_to_sell_vm = new BestPriceToSellViewModel();
                var prices_to_sell = orderBook.Data.Bids;
                var best_price_to_sell = prices_to_sell.Where(p => p.Amount >= btc_amount || ((decimal)p.Amount * p.Price) > min_value).FirstOrDefault();

                if (prices_to_sell != null)
                {
                    best_price_to_sell_vm.Exchange = "Bitcointrade";
                    best_price_to_sell_vm.Price = best_price_to_sell.Price;
                    best_price_to_sell_vm.Amount = best_price_to_sell.Amount;
                    var fee = (((decimal)btc_amount * best_price_to_sell.Price) * (decimal)withdrawal_fee_percent) + withdrawal_fee_brl;
                    best_price_to_sell_vm.Valeu = (decimal)btc_amount * best_price_to_sell.Price;
                    best_price_to_sell_vm.Fee = fee;
                }

                return best_price_to_sell_vm;
            });
        }

        public async Task<BitcointradeOrderBook> GetOrderBookAsync(string coin = "BRLBTC")
        {
            return await GetAsync<BitcointradeOrderBook>(BitcointradeQueryStringService.AppendQueryString("public/" + coin + "/orders"));
        }
    }
}