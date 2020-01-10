using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks.Handlers.Stocks
{
    public class GetSingleStockHandler : IGetSingleHandler<StockVM>
    {
        private readonly PatchaWalletDbClient _client;

        public GetSingleStockHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<StockVM> Handle(GetSingleRequest<StockVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var document = _client.Stocks.GetDocumentQuery().FirstOrDefault(c => c.Id == request.Id);
                return document.ToVM();
            });
        }
    }
}
