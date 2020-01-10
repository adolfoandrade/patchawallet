using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class DeleteStockTransactionHandler : IDeleteHandler<StockTransactionVM>
    {
        private readonly PatchaWalletDbClient _client;

        public DeleteStockTransactionHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<Unit> Handle(DeleteRequest<StockTransactionVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                _client.StockTransactions.DeleteDocumentAsync(request.Item.Id);
                return Unit.Value;
            });
        }
    }
}
