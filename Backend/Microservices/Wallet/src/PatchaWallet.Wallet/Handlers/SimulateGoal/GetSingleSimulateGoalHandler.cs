using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class GetSingleSimulateGoalHandler : IGetSingleHandler<SimulateGoalVM>
    {
        private readonly PatchaWalletDbClient _client;

        public GetSingleSimulateGoalHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<SimulateGoalVM> Handle(GetSingleRequest<SimulateGoalVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var document = _client.SimulateGoalsCollection.GetDocumentQuery().FirstOrDefault(c => c.Id == request.Id);
                var vm = document.ToVM();
                return vm;
            });
        }
    }
}
