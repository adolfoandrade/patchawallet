using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class GetStockTradeHandler : IGetCollectionHandler<StockTransactionVM>
    {
        private readonly PatchaWalletDbClient _client;

        public GetStockTradeHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<StockTransactionVM>> Handle(GetCollectionRequest<StockTransactionVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var documents = _client.StockTransactions.GetDocumentQuery();
                return documents.ToVM();
            });
        }
    }
}