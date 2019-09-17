using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers.InvestmentCompanies
{
    public class GetCompaniesHandler : IGetCollectionHandler<Stock>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetCompaniesHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Stock>> Handle(GetCollectionRequest<Stock> request, CancellationToken cancellationToken)
        {
            return await _client.Stocks.GetDocumentQuery().ToAsyncEnumerable().ToArray();
        }
        #endregion
    }
}
