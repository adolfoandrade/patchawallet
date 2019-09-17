using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces
{
    public interface IBestPriceTo<T>
    {
        Task<BestPriceToBuyViewModel> GetBestPriceToBuyAsync(T orderBook, decimal min_value = 2000);
        Task<BestPriceToSellViewModel> GetBestPriceToSellAsync(T orderBook, double btc_amount);
    }
}
