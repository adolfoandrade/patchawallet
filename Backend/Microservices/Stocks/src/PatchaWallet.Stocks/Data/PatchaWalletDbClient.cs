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
        private const string COIN_COLLECTION_ID = "coins";
        private const string COIN_TRADE_COLLECTION_ID = "cointrades";
        private const string STOCK_TRADE_COLLECTION_ID = "stocktransactions";
        private const string QUOTE_COLLECTION_ID = "quotes";
        private const string USER_COLLECTION_ID = "users";

        private readonly MongoDbOptions _options;
        private readonly IMongoClient _client;

        public DocumentCollection<StockDocument> Stocks { get; }
        public DocumentCollection<CoinDocument> Coins { get; }
        public DocumentCollection<CoinTradeDocument> CoinTrades { get; }
        public DocumentCollection<StockTransactionDocument> StockTransactions { get; }
        public DocumentCollection<QuoteDocument> Quotes { get; }
        public DocumentCollection<UserDocument> Users { get; }

        public PatchaWalletDbClient(IOptions<MongoDbOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _client = new MongoClient(_options.Connection);

            Stocks = new DocumentCollection<StockDocument>(_client, _options.DatabaseId, STOCK_COLLECTION_ID);
            Coins = new DocumentCollection<CoinDocument>(_client, _options.DatabaseId, COIN_COLLECTION_ID);
            CoinTrades = new DocumentCollection<CoinTradeDocument>(_client, _options.DatabaseId, COIN_TRADE_COLLECTION_ID);
            StockTransactions = new DocumentCollection<StockTransactionDocument>(_client, _options.DatabaseId, STOCK_TRADE_COLLECTION_ID);
            Quotes = new DocumentCollection<QuoteDocument>(_client, _options.DatabaseId, QUOTE_COLLECTION_ID);
            Users = new DocumentCollection<UserDocument>(_client, _options.DatabaseId, USER_COLLECTION_ID);
        }

    }
}