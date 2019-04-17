using MongoDB.Bson;
using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Documents;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers.InvestmentCompanies
{
    public class CreateCompanyHandler : ICreateHandler<InvestmentCompany>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public CreateCompanyHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<InvestmentCompany> Handle(CreateRequest<InvestmentCompany> request, CancellationToken cancellationToken)
        {
            InvestmentCompanyDocument companyDocument = new InvestmentCompanyDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = request.Item.Name,
                Symbol = request.Item.Symbol,
                Type = request.Item.Type,
                Region = request.Item.Region,
                MarketOpen = request.Item.MarketOpen,
                MarketClose = request.Item.MarketClose,
                TimeZone = request.Item.TimeZone,
                Currency = request.Item.Currency
            };

            await _client.Companies.CreateDocumentAsync(companyDocument);

            return companyDocument;
        }
        #endregion
    }
}
