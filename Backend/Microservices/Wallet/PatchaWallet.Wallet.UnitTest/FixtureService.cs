using MediatR;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace PatchaWallet.Wallet.UnitTest
{
    public class FixtureService
    {
        private readonly Mock<IMediator> _mockMediator;

        public FixtureService()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public void Gain_or_loss_value_should()
        {

        }

        [Fact]
        public void Wallet_componetns_with_gain_or_loss_values_should()
        {

        }

        [Fact]
        public void Wallet_goals_should()
        {

        }

        [Fact]
        public void Add_simulate_goals_should()
        {
            // Arrange
            var vm = new SimulateGoalVM();
            _mockMediator.Setup(x => x.Send(It.IsAny<CreateRequest<SimulateGoalVM>>(), default(CancellationToken))).ReturnsAsync(vm);

            // Action
            var _sut = new WalletService(_mockMediator.Object);
            var result = _sut.AddAsync(vm).Result;

            // Assert
            Assert.NotNull(result);
        }

    }
}
