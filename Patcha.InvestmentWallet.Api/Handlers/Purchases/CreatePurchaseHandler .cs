using MongoDB.Bson;
using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Documents;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers.Purchases
{
    public class CreatePurchaseHandler : ICreateHandler<Trade>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public CreatePurchaseHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<Trade> Handle(CreateRequest<Trade> request, CancellationToken cancellationToken)
        {
            var company = await _client.Companies.GetDocumentQuery().Where(c => c.Id == request.Item.InvestmentCompany.Id).ToAsyncEnumerable().FirstOrDefault();

            PurchaseDocument purchaseDocument = new PurchaseDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                InvestmentCompany = company,
                Amount = request.Item.Amount,
                Price = request.Item.Price,
                PurchaseType = request.Item.PurchaseType,
                Commission = request.Item.Commission,
                When = request.Item.When
            };

            await _client.Purchases.CreateDocumentAsync(purchaseDocument);

            return purchaseDocument;
        }
        #endregion
    }
}
