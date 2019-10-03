using MediatR;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api
{
    public class DeleteStockTransactionHandler : IDeleteHandler<StockTransaction>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public DeleteStockTransactionHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<Unit> Handle(DeleteRequest<StockTransaction> request, CancellationToken cancellationToken)
        {
            await _client.StockTransactions.DeleteDocumentAsync(request.Item.Id);
            return Unit.Value;
        }
        #endregion
    }
}
