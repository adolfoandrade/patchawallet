using Newtonsoft.Json;
using Patcha.InvestmentWallet.Api.Services.IIIxbit;
using Patcha.InvestmentWallet.Api.UnitTest.Mock;
using Patcha.InvestmentWallet.Core.IIIxbit.Entities.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

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
            Assert.Equal(0.113231, best_price_to_buy.Amount);
            Assert.Equal(2276.116061711272m, best_price_to_buy.Valeu);
            Assert.Equal(11.38058030855636m, best_price_to_buy.Fee);
            Assert.True(best_price_to_buy.Valeu > 2000);
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
            Assert.Equal(2215.427966934856m, best_price_to_sell.Valeu);
            Assert.Equal(15.43856991733714m, best_price_to_sell.Fee);
            Assert.True(best_price_to_sell.Valeu > 2000);
        }
    }
}
