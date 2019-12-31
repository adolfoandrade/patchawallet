using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PatchaWallet.Wallet.UnitTest
{
    public class FixtureData : FixtureBase
    {
        private readonly Mock<IDocumentDbClient> _mockDocumentDbClient;
        private readonly Mock<IDocumentCollection<SimulateGoalDocument>> _mockDocumentCollection;
        
        public FixtureData()
        {
            _mockDocumentDbClient = new Mock<IDocumentDbClient>();
            _mockDocumentCollection = new Mock<IDocumentCollection<SimulateGoalDocument>>();
        }

        [Fact]
        public void Load_db_should()
        {
            MongoDbOptions mongoDbOptions = new MongoDbOptions() 
            {
                Connection = "mongodb://localhost:27017", 
                DatabaseId = "dpsp_unittests" 
            };
            var mockMongoDbOptions = new Mock<IOptions<MongoDbOptions>>();
            mockMongoDbOptions.Setup(ap => ap.Value).Returns(mongoDbOptions);
            
            var repository = new PatchaWalletDbClient(mockMongoDbOptions.Object);
        }
    }
}
