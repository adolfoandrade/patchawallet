using PatchaWallet.Stocks.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class GetStockHandler : IGetCollectionHandler<Stock>
    {
        private readonly PatchaWalletDbClient _client;

        public GetStockHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<Stock>> Handle(GetCollectionRequest<Stock> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                return _client.StocksCollection.GetDocumentQuery().AsEnumerable();
            });
        }

    }
}
