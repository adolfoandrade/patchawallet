using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api {
    public class GetStockTradeRequest : IGetCollectionHandler<StockTrade> {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetStockTradeRequest (PatchaWalletDbClient client) {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<StockTrade>> Handle (GetCollectionRequest<StockTrade> request, CancellationToken cancellationToken) {
            return  await _client.StockTrades.GetDocumentQuery ().ToAsyncEnumerable ().ToArray();
        }
        #endregion
    }
}