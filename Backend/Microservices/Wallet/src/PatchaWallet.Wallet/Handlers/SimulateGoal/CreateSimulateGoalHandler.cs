using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public class CreateSimulateGoalHandler : ICreateHandler<SimulateGoalVM>
    {
        private readonly IDocumentDbClient _client;

        public CreateSimulateGoalHandler(IDocumentDbClient client)
        {
            _client = client;
        }

        public async Task<SimulateGoalVM> Handle(CreateRequest<SimulateGoalVM> request, CancellationToken cancellationToken)
        {
            SimulateGoalDocument document = request.Item.ToDocument();
            document.Id = ObjectId.GenerateNewId().ToString();
            await _client.SimulateGoalsCollection.CreateDocumentAsync(document);
            request.Item.Id = document.Id;
            return request.Item;
        }

    }
}
