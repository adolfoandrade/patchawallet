using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api
{ 
    public class GetSingleStockTransactionHandler : IGetSingleHandler<StockTransaction>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetSingleStockTransactionHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<StockTransaction> Handle(GetSingleRequest<StockTransaction> request, CancellationToken cancellationToken)
        {
            return await _client.StockTransactions.GetDocumentQuery().Where(c => c.Id == request.Id).ToAsyncEnumerable().FirstOrDefault();
        }
        #endregion
    }
}
