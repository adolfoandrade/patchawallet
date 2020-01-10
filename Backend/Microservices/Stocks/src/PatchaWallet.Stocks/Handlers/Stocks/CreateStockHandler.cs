using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class CreateStockHandler : ICreateHandler<StockVM>
    {
        private readonly PatchaWalletDbClient _client;

        public CreateStockHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public async Task<StockVM> Handle(CreateRequest<StockVM> request, CancellationToken cancellationToken)
        {
            StockDocument stock = request.Item.ToDocument();
            stock.Id = ObjectId.GenerateNewId().ToString();

            await _client.Stocks.CreateDocumentAsync(stock);

            request.Item.Id = stock.Id;
            var stockVM = request.Item;

            return stockVM;
        }

    }
}
