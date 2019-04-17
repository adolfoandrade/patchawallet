using Newtonsoft.Json;
using Patcha.InvestmentWallet.Api.Services.Braziliex;
using Patcha.InvestmentWallet.Api.UnitTest.Mock;
using Patcha.InvestmentWallet.Core.Braziliex.Entities.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Patcha.InvestmentWallet.Api.UnitTest.Services.BraziliexServicesTests
{
    public class GetBraziliexPricesFixture
    {
        [Fact]
        public void Get_best_price_to_buy_shoud()
        {
            // Arrange
            var mockData = JsonConvert.DeserializeObject<BraziliexOrderBook>(BraziliexOrderBookMockData.MockData);
            var _sut = new BraziliexService(new HttpClient());

            // Action
            var best_price_to_buy = _sut.GetBestPriceToBuyAsync(mockData).Result;

            // Assert
            Assert.Equal(19999, best_price_to_buy.Price);
            Assert.Equal(0.91636323, best_price_to_buy.Amount);
            Assert.Equal(18326.34823677m, best_price_to_buy.Valeu);
            Assert.Equal(91.63174118385m, best_price_to_buy.Fee);
            Assert.True(best_price_to_buy.Valeu > 2000);
        }

        [Fact]
        public void Get_best_price_to_sell_shoud()
        {
            // Arrange
            var mockData = JsonConvert.DeserializeObject<BraziliexOrderBook>(BraziliexOrderBookMockData.MockData);
            var _sut = new BraziliexService(new HttpClient());

            // Action
            var best_price_to_buy = _sut.GetBestPriceToBuyAsync(mockData).Result;
            var best_price_to_sell = _sut.GetBestPriceToSellAsync(mockData, best_price_to_buy.Amount).Result;

            // Assert
            Assert.Equal(19751, best_price_to_sell.Price);
            Assert.Equal(1.15963292, best_price_to_sell.Amount);
            Assert.Equal(18099.09015573m, best_price_to_sell.Valeu);
            Assert.Equal(144.743176167975m, best_price_to_sell.Fee);
            Assert.True(best_price_to_sell.Valeu > 2000);
        }

    }
}
