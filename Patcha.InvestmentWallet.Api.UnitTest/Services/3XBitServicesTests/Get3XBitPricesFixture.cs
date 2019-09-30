using Newtonsoft.Json;
using Patcha.InvestmentWallet.Api.Services.IIIxbit;
using Patcha.InvestmentWallet.Api.UnitTest.Mock;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;
using Patcha.Coins;

namespace Patcha.InvestmentWallet.Api.UnitTest.Services._3XBitServicesTests
{
    public class Get3XBitPricesFixture
    {

        [Fact]
        public void Get_best_price_to_buy_shoud()
        {
            // Arrange
            var mockData = JsonConvert.DeserializeObject<IIIxbitOrderBook>(IIIXBitOrderBookMockData.MockData);
            var _sut = new IIIxbitService(new HttpClient());

            // Action
            var best_price_to_buy = _sut.GetBestPriceToBuyAsync(mockData).Result;

            // Assert
            Assert.Equal(3.887200m, best_price_to_buy.ExchangeRate);
            Assert.Equal(20101.52751200000000m, best_price_to_buy.Price);
            Assert.Equal(0.0994949263833836, best_price_to_buy.Amount);
            Assert.Equal(2000.0000000000000950496032000m, best_price_to_buy.Valeu);
            Assert.Equal(10.000000000000000475248016000m, best_price_to_buy.Fee);
        }

        [Fact]
        public void Get_best_price_to_sell_shoud()
        {
            // Arrange
            var mockData = JsonConvert.DeserializeObject<IIIxbitOrderBook>(IIIXBitOrderBookMockData.MockData);
            var _sut = new IIIxbitService(new HttpClient());

            // Action
            var best_price_to_buy = _sut.GetBestPriceToBuyAsync(mockData).Result;
            var best_price_to_sell = _sut.GetBestPriceToSellAsync(mockData, best_price_to_buy.Amount).Result;

            // Assert
            Assert.Equal(3.887200m, best_price_to_sell.ExchangeRate);
            Assert.Equal(19565.560376m, best_price_to_sell.Price);
            Assert.Equal(0.269916, best_price_to_sell.Amount);
            Assert.Equal(1946.6739892597671489682336000m, best_price_to_sell.Valeu);
            Assert.Equal(14.766684973149417872420584000m, best_price_to_sell.Fee);
        }
    }
}
