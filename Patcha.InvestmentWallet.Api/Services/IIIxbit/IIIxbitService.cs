using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Patcha.Coins;
using Patcha.InvestmentWallet.Api.Interfaces.IIIxbit;
using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;

namespace Patcha.InvestmentWallet.Api.Services.IIIxbit
{
    public class IIIxbitService : BaseApi, IIIIxbitService
    {
        public IIIxbitService(HttpClient httpClient) : base(httpClient)
        {
        }

        public Task<BestPriceToBuyViewModel> GetBestPriceToBuyAsync(IIIxbitOrderBook orderBook, decimal min_value = 2000)
        {
            return Task.Factory.StartNew(() => {
                double transaction_fee_percent = (0.50 / 100);
                var best_price_to_buy_vm = new BestPriceToBuyViewModel();
                var prices_to_buy = orderBook.SellOrders;

                var best_price_to_buy = prices_to_buy.Where(p => ((decimal)p.Quantity * (p.Price * orderBook.ExchangeRate)) > min_value).FirstOrDefault();
                best_price_to_buy.Price *= orderBook.ExchangeRate;
                best_price_to_buy_vm.Exchange = "3xBit";

                if (best_price_to_buy != null)
                {
                    best_price_to_buy_vm.Exchange = "3xBit";
                    best_price_to_buy_vm.ExchangeRate = orderBook.ExchangeRate;
                    best_price_to_buy_vm.Amount = (double)(min_value / best_price_to_buy.Price);
                    best_price_to_buy_vm.Price = best_price_to_buy.Price;
                    best_price_to_buy_vm.Valeu = (decimal)best_price_to_buy_vm.Amount * best_price_to_buy.Price;
                    var fee = best_price_to_buy_vm.Valeu * (decimal)transaction_fee_percent;             
                    best_price_to_buy_vm.Fee = fee;
                }

                return best_price_to_buy_vm;
            });
        }

        public Task<BestPriceToSellViewModel> GetBestPriceToSellAsync(IIIxbitOrderBook orderBook, double btc_amount)
        {
            return Task.Factory.StartNew(() => {
                decimal min_value = 1000;
                double withdrawal_fee_percent = (0.25 / 100);
                decimal withdrawal_fee_brl = 9.90m;
                var best_price_to_sell_vm = new BestPriceToSellViewModel();
                var prices_to_sell = orderBook.BuyOrders;
                var best_price_to_sell = prices_to_sell.Where(p => ((decimal)p.Quantity * p.Price) > min_value).FirstOrDefault();

                if (best_price_to_sell != null)
                {
                    best_price_to_sell.Price *= orderBook.ExchangeRate;

                    if (best_price_to_sell != null)
                    {
                        best_price_to_sell_vm.Exchange = "3xBit";
                        best_price_to_sell_vm.ExchangeRate = orderBook.ExchangeRate;
                        best_price_to_sell_vm.Price = best_price_to_sell.Price;
                        best_price_to_sell_vm.Amount = best_price_to_sell.Quantity;
                        var fee = (((decimal)btc_amount * best_price_to_sell.Price) * (decimal)withdrawal_fee_percent) + withdrawal_fee_brl;
                        best_price_to_sell_vm.Valeu = ((decimal)btc_amount * best_price_to_sell.Price);
                        best_price_to_sell_vm.Fee = fee;
                    }
                }

                return best_price_to_sell_vm;
            });
        }

        public async Task<IIIxbitOrderBook> GetOrderBookAsync(string coin)
        {
            return await GetAsync<IIIxbitOrderBook>(IIIxbitQueryStringService.AppendQueryString("v1/orderbook/credit/" + coin));
        }
    }
}
