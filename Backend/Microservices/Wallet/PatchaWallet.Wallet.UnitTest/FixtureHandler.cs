using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace PatchaWallet.Wallet.UnitTest
{
    public class FixtureHandler
    {
        private readonly Mock<ICreateHandler<SimulateGoalVM>> _mockCreateHandler;
        private readonly Mock<IDocumentDbClient> _mockDocumentDbClient;
        private readonly Mock<PatchaWalletDbClient> _mockPatchaDbClient;
        private readonly Mock<IDocumentCollection<SimulateGoalDocument>> _mockDocumentCollection;

        public FixtureHandler()
        {
            _mockCreateHandler = new Mock<ICreateHandler<SimulateGoalVM>>();
            _mockPatchaDbClient = new Mock<PatchaWalletDbClient>();
            _mockDocumentDbClient = new Mock<IDocumentDbClient>();
            _mockDocumentCollection = new Mock<IDocumentCollection<SimulateGoalDocument>>();
        }

        [Fact]
        public void Create_request_simulate_goals_should()
        {
            // Arrange
            var vm = new SimulateGoalVM();
            var request = new CreateRequest<SimulateGoalVM>(vm);
            _mockCreateHandler.Setup(x => x.Handle(new CreateRequest<SimulateGoalVM>(vm), default(CancellationToken))).ReturnsAsync(vm);
            _mockDocumentCollection.Setup(x => x.CreateDocumentAsync(new SimulateGoalDocument()));
            _mockDocumentDbClient.Setup(x => x.SimulateGoalsCollection).Returns(_mockDocumentCollection.Object);
            
            // Action
            var _sut = new CreateSimulateGoalHandler(_mockDocumentDbClient.Object);
            var result = _sut.Handle(request, default(CancellationToken)).Result;

            // Assert
            Assert.NotNull(result);
        }
    }
}
