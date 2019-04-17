using Patcha.InvestmentWallet.Api.ViewModels.TradesCoins;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Interfaces
{
    public interface IBestPriceTo<T>
    {
        Task<BestPriceToBuyViewModel> GetBestPriceToBuyAsync(T orderBook);
        Task<BestPriceToSellViewModel> GetBestPriceToSellAsync(T orderBook, double btc_amount);
    }
}
