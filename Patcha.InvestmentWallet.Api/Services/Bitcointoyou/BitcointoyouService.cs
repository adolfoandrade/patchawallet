using Patcha.InvestmentWallet.Api.Interfaces.Bitcointoyou;
using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;
using Patcha.InvestmentWallet.Core.Bitcointoyou.Entities.Response;
using System.Net.Http;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Services.Bitcointoyou
{
    public class BitcointoyouService : BaseApi, IBitcointoyouService
    {
        public BitcointoyouService(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<BestPriceToBuyViewModel> GetBestPriceToBuyAsync(BitcointoyouOrderBook orderBook)
        {
            return Task.Factory.StartNew(() =>
            {
                decimal min_value = 2000;
                double transaction_fee_percent = (0.60 / 100);
                var best_price_to_buy_vm = new BestPriceToBuyViewModel();
                var prices_to_buy = orderBook.Asks;
                decimal best_price_to_buy = 0;
                double amount_to_buy = 0;

                for (int i = 0; i < (prices_to_buy.Length / 2) -1; i++)
                {
                    var value = (prices_to_buy[i, 0] * prices_to_buy[i, 1]);

                    if (value > min_value)
                    {
                        best_price_to_buy = prices_to_buy[i, 0];
                        amount_to_buy = (double)prices_to_buy[i, 1];
                        break;
                    }
                }

                best_price_to_buy_vm.Exchange = "Bitcointoyou";
                if (best_price_to_buy > 0)
                {                    
                    best_price_to_buy_vm.Price = best_price_to_buy;
                    best_price_to_buy_vm.Amount = amount_to_buy;
                    best_price_to_buy_vm.Valeu = (decimal)amount_to_buy * best_price_to_buy;
                    var fee = best_price_to_buy_vm.Valeu * (decimal)transaction_fee_percent;
                    best_price_to_buy_vm.Fee = fee;
                }

                return best_price_to_buy_vm;
            });
        }

        public Task<BestPriceToSellViewModel> GetBestPriceToSellAsync(BitcointoyouOrderBook orderBook, double btc_amount)
        {
            return Task.Factory.StartNew(() => {
                decimal min_value = 2000;
                double withdrawal_fee_percent = (1.89 / 100);
                decimal withdrawal_fee_brl = 9.70m;
                var best_price_to_sell_vm = new BestPriceToSellViewModel();
                var prices_to_sell = orderBook.Bids;
                decimal best_price_to_sell = 0;
                double amount_to_sell = 0;

                for (int i = 0; i < (prices_to_sell.Length/2 )-1; i++)
                {
                    var value = (prices_to_sell[i, 0] * prices_to_sell[i, 1]);

                    if (value > min_value)
                    {
                        best_price_to_sell = prices_to_sell[i, 0];
                        amount_to_sell = (double)prices_to_sell[i, 1];
                        break;
                    }
                }

                best_price_to_sell_vm.Exchange = "Bitcointoyou";
                if (prices_to_sell != null)
                {
                    best_price_to_sell_vm.Price = best_price_to_sell;
                    best_price_to_sell_vm.Amount = btc_amount;
                    var fee = (((decimal)btc_amount * best_price_to_sell) * (decimal)withdrawal_fee_percent) + withdrawal_fee_brl;
                    best_price_to_sell_vm.Valeu = ((decimal)btc_amount * best_price_to_sell);
                    best_price_to_sell_vm.Fee = fee;
                }

                return best_price_to_sell_vm;
            });
        }

        public async Task<BitcointoyouOrderBook> GetOrderBookAsync()
        {
            return await GetAsync<BitcointoyouOrderBook>(BitcointoyouQueryStringService.AppendQueryString("orderbook.aspx"));
        }
    }
}
