using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PatchaWallet.Stocks.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class PatchaWalletDbClient
    {
        private const string STOCK_COLLECTION_ID = "stocks";

        private readonly MongoDbOptions _options;
        private readonly IMongoClient _client;

        public IDocumentCollection<Stock> StocksCollection { get; }

        public PatchaWalletDbClient(IOptions<MongoDbOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _client = new MongoClient(_options.Connection);

            StocksCollection = new DocumentCollection<Stock>(_client, _options.DatabaseId, STOCK_COLLECTION_ID);
        }

    }
}
