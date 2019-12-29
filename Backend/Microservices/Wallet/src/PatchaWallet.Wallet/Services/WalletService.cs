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

        public async Task<SimulateGoalVM> AddAsync(SimulateGoalVM simulateGoalVM)
        {
            var request = await _mediator.Send(new CreateRequest<SimulateGoalVM>(simulateGoalVM));
            return request;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
