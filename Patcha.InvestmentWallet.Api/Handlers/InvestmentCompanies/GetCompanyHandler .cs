using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers.InvestmentCompanies
{
    public class GetCompanyHandler : IGetSingleHandler<InvestmentCompany>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetCompanyHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<InvestmentCompany> Handle(GetSingleRequest<InvestmentCompany> request, CancellationToken cancellationToken)
        {
            return await _client.Companies.GetDocumentQuery().Where(c => c.Id == request.Id).ToAsyncEnumerable().FirstOrDefault();
        }
        #endregion
    }
}
