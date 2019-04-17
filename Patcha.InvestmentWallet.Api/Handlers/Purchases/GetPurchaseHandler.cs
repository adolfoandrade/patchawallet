using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers.Purchases
{
    public class GetPurchaseHandler : IGetSingleHandler<Trade>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetPurchaseHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<Trade> Handle(GetSingleRequest<Trade> request, CancellationToken cancellationToken)
        {
            return await _client.Purchases.GetDocumentQuery().Where(c => c.Id == request.Id).ToAsyncEnumerable().FirstOrDefault();
        }
        #endregion
    }
}
