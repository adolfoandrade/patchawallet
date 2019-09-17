using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Documents;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api {
    public class CreateStockTradeHandler : ICreateHandler<StockTrade> {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public CreateStockTradeHandler (PatchaWalletDbClient client) {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<StockTrade> Handle (CreateRequest<StockTrade> request, CancellationToken cancellationToken) {
            var trade = _client.StockTrades.GetDocumentQuery ()
                .FirstOrDefault (c => c.Id == request.Item.Id);

            StockTradeDocument purchaseDocument = new StockTradeDocument {
                Id = ObjectId.GenerateNewId ().ToString (),
                Stock = request.Item.Stock,
                Amount = request.Item.Amount,
                Price = request.Item.Price,
                TradeType = request.Item.TradeType,
                Commission = request.Item.Commission,
                When = request.Item.When
            };

            await _client.StockTrades.CreateDocumentAsync (purchaseDocument);

            return purchaseDocument;
        }
        #endregion
    }
}