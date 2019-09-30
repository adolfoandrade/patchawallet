using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api {
    public class GetCoinTradeRequest : IGetCollectionHandler<CoinTrade> {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public GetCoinTradeRequest (PatchaWalletDbClient client) {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<CoinTrade>> Handle (GetCollectionRequest<CoinTrade> request, CancellationToken cancellationToken) {
            return await _client.CoinTrades.GetDocumentQuery ().ToAsyncEnumerable ().ToArray ();
        }
        #endregion
    }
}