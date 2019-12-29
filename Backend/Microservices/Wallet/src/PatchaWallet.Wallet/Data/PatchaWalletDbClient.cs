using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace PatchaWallet.Wallet
{
    public class PatchaWalletDbClient : IDocumentDbClient
    {
        private const string SIMULATEGOALS_COLLECTION_ID = "simulategoals";

        private readonly MongoDbOptions _options;
        private readonly IMongoClient _client;

        public IDocumentCollection<SimulateGoalDocument> SimulateGoalsCollection { get; }

        public PatchaWalletDbClient(IOptions<MongoDbOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _client = new MongoClient(_options.Connection);

            SimulateGoalsCollection = new DocumentCollection<SimulateGoalDocument>(_client, _options.DatabaseId, SIMULATEGOALS_COLLECTION_ID);
        }

    }
}
