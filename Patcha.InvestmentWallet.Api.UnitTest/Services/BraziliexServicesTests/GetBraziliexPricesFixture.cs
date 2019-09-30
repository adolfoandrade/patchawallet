using Newtonsoft.Json;
using Patcha.Coins;
using Patcha.InvestmentWallet.Api.Services.Braziliex;
using Patcha.InvestmentWallet.Api.UnitTest.Mock;
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
            Assert.Equal(0.10000500025001251, best_price_to_buy.Amount);
            Assert.Equal(1999.999999999989988m, best_price_to_buy.Valeu);
            Assert.Equal(9.999999999999949940m, best_price_to_buy.Fee);
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
            Assert.Equal(19889.01m, best_price_to_sell.Price);
            Assert.Equal(0.2, best_price_to_sell.Amount);
            Assert.Equal(1989.00045002249116812m, best_price_to_sell.Valeu);
            Assert.Equal(23.917503375168683760900m, best_price_to_sell.Fee);
        }

    }
}
