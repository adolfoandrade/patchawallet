using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace PatchaWallet.Stocks
{
    public class CreateStockTransactionHandler : ICreateHandler<StockTransactionVM>
    {
        private readonly PatchaWalletDbClient _client;

        public CreateStockTransactionHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<StockTransactionVM> Handle(CreateRequest<StockTransactionVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var transactionVM = request.Item;
                transactionVM.Id = ObjectId.GenerateNewId().ToString();
                var document = transactionVM.ToDocument();
                _client.StockTransactions.CreateDocumentAsync(document);

                return transactionVM;
            });
        }
    }
}