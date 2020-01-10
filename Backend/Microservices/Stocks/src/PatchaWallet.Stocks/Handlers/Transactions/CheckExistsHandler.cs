using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class CheckExistsHandler : ICheckExistsHandler<StockTransactionVM>
    {
        private readonly PatchaWalletDbClient _client;

        public CheckExistsHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<StockTransactionVM> Handle(CheckExistsRequest<StockTransactionVM> request, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => {
                var transaction = _client.StockTransactions.GetDocumentQuery()
                                                                 .FirstOrDefault(c => c.Id == request.Item.Id);

                if (transaction == null)
                    transaction = _client.StockTransactions.GetDocumentQuery()
                    .FirstOrDefault(c => c.When == request.Item.When
                                        && c.Stock.Symbol == request.Item.Stock.Symbol
                                        && c.Price == request.Item.Price
                                        && c.TradeType == request.Item.TradeType
                                        && c.Commission == request.Item.Commission);

                return transaction.ToVM();
            });
        }
    }
}
