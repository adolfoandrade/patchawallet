using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PatchaWallet.Wallet.UnitTest
{
    public class FixtureController
    {
        private readonly Mock<IWalletService> _mockWalletService;

        public FixtureController()
        {
            _mockWalletService = new Mock<IWalletService>();
        }

        [Fact]
        public void Post_simulate_goals_should()
        {
            // Arrange
            var vm = new SimulateGoalVM();
            _mockWalletService.Setup(x => x.AddAsync(vm)).ReturnsAsync(vm);

            // Action
            var _sut = new WalletsController(_mockWalletService.Object);
            var result = _sut.Post(vm).Result;

            // Assert
            Assert.NotNull(result);
        }
    }
}
