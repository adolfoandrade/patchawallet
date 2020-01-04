using MongoDB.Bson;
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

        [Fact]
        public void Get_simulate_goal_should()
        {
            // Arrange
            var mockVM = LoadJson<SimulateGoalVM>("SimulateMonthGoal");
            var mockResultVM = LoadJson<SimulateGoalResultVM>("SimulateAnnualGoalResult");
            var id = ObjectId.GenerateNewId().ToString();
            mockVM.Id = id;
            _mockWalletService.Setup(x => x.GetAsync(id)).ReturnsAsync(new SimulateGoalResultVM());

            // Action
            var _sut = new SimulateController(_mockWalletService.Object);
            var result = _sut.Get(id).Result;

            // Assert
            Assert.NotNull(result);
        }
    }
}
