using MongoDB.Bson;
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
    public class UpdateCompanyHandler : IUpdateHandler<InvestmentCompany>
    {
        private readonly PatchaWalletDbClient _client;

        public UpdateCompanyHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        public async Task<InvestmentCompany> Handle(UpdateRequest<InvestmentCompany> request, CancellationToken cancellationToken)
        {
            InvestmentCompany companyDocument = await _client.Companies.GetDocumentQuery().Where(c => c.Id == request.Id).Take(1).ToAsyncEnumerable().FirstOrDefault();

            if (companyDocument != null)
            {
                var company = await _client.Companies.GetDocumentQuery().Where(c => c.Id == request.Update.Id).ToAsyncEnumerable().FirstOrDefault();

                company.Id = ObjectId.GenerateNewId().ToString();
                company.Name = request.Update.Name;
                company.Symbol = request.Update.Symbol;
                company.Type = request.Update.Type;
                company.Region = request.Update.Region;
                company.MarketOpen = request.Update.MarketOpen;
                company.MarketClose = request.Update.MarketClose;
                company.TimeZone = request.Update.TimeZone;
                company.Currency = request.Update.Currency;
                company.Quote = request.Update.Quote;

                await _client.Companies.ReplaceDocumentAsync(request.Id, company);
            }

            return companyDocument;
        }
    }
}
