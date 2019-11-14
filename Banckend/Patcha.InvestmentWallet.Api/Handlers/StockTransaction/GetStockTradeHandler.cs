using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api {
    public class GetStockTradeHandler : IGetCollectionHandler<StockTransaction> {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetStockTradeHandler(PatchaWalletDbClient client) {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<StockTransaction>> Handle (GetCollectionRequest<StockTransaction> request, CancellationToken cancellationToken) {
            return  await _client.StockTransactions.GetDocumentQuery ().ToAsyncEnumerable ().ToArray();
        }
        #endregion
    }
}