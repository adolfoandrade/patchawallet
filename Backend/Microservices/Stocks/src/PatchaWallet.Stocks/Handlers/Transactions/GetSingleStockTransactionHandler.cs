using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{ 
    public class GetSingleStockTransactionHandler : IGetSingleHandler<StockTransactionVM>
    {
        private readonly PatchaWalletDbClient _client;

        public GetSingleStockTransactionHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<StockTransactionVM> Handle(GetSingleRequest<StockTransactionVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var document =  _client.StockTransactions.GetDocumentQuery().FirstOrDefault(c => c.Id == request.Id);
                return document.ToVM();
            });
        }
    }
}
