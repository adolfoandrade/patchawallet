using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api
{
    public class CheckExistsHandler : ICheckExistsHandler<StockTransaction>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public CheckExistsHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<StockTransaction> Handle(CheckExistsRequest<StockTransaction> request, CancellationToken cancellationToken)
        {
            var transaction = await _client.StockTransactions.GetDocumentQuery().ToAsyncEnumerable()
            .FirstOrDefault(c => c.Id == request.Item.Id);

            if (transaction == null)
                transaction = _client.StockTransactions.GetDocumentQuery()
                .FirstOrDefault(c => c.When == request.Item.When
                                    && c.Stock.Symbol == request.Item.Stock.Symbol
                                    && c.Price == request.Item.Price
                                    && c.TradeType == request.Item.TradeType
                                    && c.Commission == request.Item.Commission);

            return transaction;
        }
        #endregion
    }
}
