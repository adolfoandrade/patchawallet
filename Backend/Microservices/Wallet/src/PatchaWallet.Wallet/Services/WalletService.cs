using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public sealed class WalletService : IWalletService
    {
        private readonly IMediator _mediator;

        public WalletService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SimulateGoalResultVM> AddAsync(SimulateGoalVM simulateGoalVM)
        {
            var request = await _mediator.Send(new CreateRequest<SimulateGoalVM>(simulateGoalVM));
            var result = Simulate(request);
            return result;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public async Task<SimulateGoalResultVM> GetAsync(string id)
        {
            var request = await _mediator.Send(new GetSingleRequest<SimulateGoalVM>(id));
            var response = Simulate(request);
            return response;
        }

        private SimulateGoalResultVM Simulate(SimulateGoalVM simulateGoalVM)
        {
            SimulateGoalResultVM simulateGoalResultVM = new SimulateGoalResultVM();
            simulateGoalResultVM.Goals = new List<GoalResult>();
            simulateGoalResultVM.AnnualPercente = simulateGoalVM.AnnualPercente;
            simulateGoalResultVM.BeginValue = simulateGoalVM.BeginValue;
            foreach (var contribution in simulateGoalVM.Contributions)
            {
                GoalResult goalResult = new GoalResult();

                var percentage = 0.0;
                switch (simulateGoalVM.DateKind)
                {
                    case DateKindEnum.Year:
                        goalResult.Date = contribution.Date;
                        percentage = simulateGoalVM.AnnualPercente;
                        break;
                    case DateKindEnum.Month:
                        goalResult.Date = contribution.Date;
                        percentage = simulateGoalVM.AnnualPercente / 12;
                        break;
                    case DateKindEnum.Week:
                        goalResult.Date = contribution.Date;
                        percentage = simulateGoalVM.AnnualPercente / 52;
                        break;
                    case DateKindEnum.WorkDay:
                        throw new NotImplementedException();
                    default:
                        goalResult.Date = contribution.Date;
                        percentage = simulateGoalVM.AnnualPercente;
                        break;
                }

                if (simulateGoalResultVM.Goals.Any())
                {
                    goalResult.CurrentValue = simulateGoalResultVM.Goals.LastOrDefault().Goal;
                }
                else
                {
                    goalResult.CurrentValue = simulateGoalVM.BeginValue;
                }

                goalResult.Contribution = contribution.Value;
                var finalValue = goalResult.Contribution + goalResult.CurrentValue;
                var percentageValue = finalValue * ((decimal)percentage / 100);
                goalResult.Goal = finalValue + percentageValue;

                simulateGoalResultVM.Goals.Add(goalResult);
            }
            simulateGoalResultVM.FinalGoal = simulateGoalResultVM.Goals.LastOrDefault();
            return simulateGoalResultVM;
        }

    }
}
