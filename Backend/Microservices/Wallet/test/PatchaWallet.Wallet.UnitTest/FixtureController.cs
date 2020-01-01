using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PatchaWallet.Wallet.UnitTest
{
    public class FixtureController : FixtureBase
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
            var responseVM = new SimulateGoalResultVM();
            _mockWalletService.Setup(x => x.AddAsync(vm)).ReturnsAsync(responseVM);

            // Action
            var _sut = new SimulateController(_mockWalletService.Object);
            var result = _sut.Post(vm).Result;

            // Assert
            Assert.NotNull(result);
        }
    }
}
