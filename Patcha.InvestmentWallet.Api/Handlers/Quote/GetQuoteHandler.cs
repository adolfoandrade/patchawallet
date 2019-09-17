using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public class GetQuoteHandler : IGetCollectionHandler<Quote>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetQuoteHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Quote>> Handle(GetCollectionRequest<Quote> request, CancellationToken cancellationToken)
        {
            return await _client.Quotes.GetDocumentQuery().ToAsyncEnumerable().ToArray();
        }
        #endregion
    }
}
