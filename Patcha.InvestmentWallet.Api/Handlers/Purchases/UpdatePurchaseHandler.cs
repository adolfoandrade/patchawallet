using Patcha.InvestmentWallet.Api.Requests;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Documents;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers.Purchases
{
    public class UpdatePurchaseHandler : IUpdateHandler<Trade>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public UpdatePurchaseHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<Trade> Handle(UpdateRequest<Trade> request, CancellationToken cancellationToken)
        {
            PurchaseDocument purchaseDocument = await _client.Purchases.GetDocumentQuery().Where(c => c.Id == request.Id).Take(1).ToAsyncEnumerable().FirstOrDefault();

            if (purchaseDocument != null)
            {
                //CheckPreconditions(request, purchaseDocument);

                var company = await _client.Companies.GetDocumentQuery().Where(c => c.Id == request.Update.InvestmentCompany.Id).ToAsyncEnumerable().FirstOrDefault();

                purchaseDocument.InvestmentCompany = company;
                purchaseDocument.Amount = request.Update.Amount;
                purchaseDocument.Price = request.Update.Price;
                purchaseDocument.Commission = request.Update.Commission;
                purchaseDocument.When = request.Update.When;
                purchaseDocument.PurchaseType = request.Update.PurchaseType;

                await _client.Purchases.ReplaceDocumentAsync(request.Id, purchaseDocument);
            }

            return purchaseDocument;
        }
        /*
        private void CheckPreconditions<T>(UpdateRequest<T> message, T update) where T : IConditionalRequestMetadata
        {
            if ((message.IfMatch) != null && message.IfMatch.Any())
            {
                if ((message.IfMatch.Count() > 2) || (message.IfMatch.First() != "*"))
                {
                    if (!message.IfMatch.Contains(update.EntityTag))
                    {
                        throw new PreconditionFailedException();
                    }
                }
            }
            else if (message.IfUnmodifiedSince.HasValue)
            {
                DateTimeOffset lastModified = update.LastModified.Value.AddTicks(-(update.LastModified.Value.Ticks % TimeSpan.TicksPerSecond));

                if (lastModified > message.IfUnmodifiedSince.Value)
                {
                    throw new PreconditionFailedException();
                }
            }
        }*/
        #endregion
    }
}
