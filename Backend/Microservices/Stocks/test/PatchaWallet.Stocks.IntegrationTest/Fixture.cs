using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Xunit;

namespace PatchaWallet.Stocks.IntegrationTest
{
    public class Fixture : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public Fixture(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


    }
}
