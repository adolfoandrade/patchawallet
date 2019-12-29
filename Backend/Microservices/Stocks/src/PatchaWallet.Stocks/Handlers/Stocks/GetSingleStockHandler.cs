using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks.Handlers.Stocks
{
    public class GetSingleStockHandler : IGetSingleHandler<Stock>
    {
        private readonly PatchaWalletDbClient _client;

        public GetSingleStockHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<Stock> Handle(GetSingleRequest<Stock> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                return _client.StocksCollection.GetDocumentQuery().FirstOrDefault(c => c.Id == request.Id);
            });
        }
    }
}
