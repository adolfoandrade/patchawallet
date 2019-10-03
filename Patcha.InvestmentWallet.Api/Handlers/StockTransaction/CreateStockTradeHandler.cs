using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Documents;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api
{
    public class CreateStockTransactionHandler : ICreateHandler<StockTransaction>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public CreateStockTransactionHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<StockTransaction> Handle(CreateRequest<StockTransaction> request, CancellationToken cancellationToken)
        {

            StockTransactionDocument purchaseDocument = new StockTransactionDocument
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Stock = request.Item.Stock,
                Amount = request.Item.Amount,
                Price = request.Item.Price,
                TradeType = request.Item.TradeType,
                Commission = request.Item.Commission,
                When = request.Item.When
            };

            await _client.StockTransactions.CreateDocumentAsync(purchaseDocument);

            return purchaseDocument;
        }
        #endregion
    }
}