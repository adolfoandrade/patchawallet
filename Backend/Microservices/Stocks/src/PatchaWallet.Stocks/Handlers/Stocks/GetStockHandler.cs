using PatchaWallet.Stocks.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class GetStockHandler : IGetCollectionHandler<StockVM>
    {
        private readonly PatchaWalletDbClient _client;

        public GetStockHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<StockVM>> Handle(GetCollectionRequest<StockVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                return _client.Stocks.GetDocumentQuery().ToList().ToVM().AsEnumerable();
            });
        }

    }
}
