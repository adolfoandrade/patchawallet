using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api
{
    public class GetSingleStockHandler : IGetSingleHandler<Stock>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetSingleStockHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<Stock> Handle(GetSingleRequest<Stock> request, CancellationToken cancellationToken)
        {
            return await _client.Stocks.GetDocumentQuery().Where(c => c.Id == request.Id).ToAsyncEnumerable().FirstOrDefault();
        }
        #endregion
    }
}
