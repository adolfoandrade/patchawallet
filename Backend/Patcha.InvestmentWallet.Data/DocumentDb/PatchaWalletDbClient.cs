using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Patcha.InvestmentWallet.Domain.Documents;

namespace Patcha.InvestmentWallet.Data.DocumentDb {
    public class PatchaWalletDbClient 
    {
        #region Fields
        private const string STOCK_COLLECTION_ID = "stocks";
        private const string COIN_COLLECTION_ID = "coins";
        private const string COIN_TRADE_COLLECTION_ID = "cointrades";
        private const string STOCK_TRADE_COLLECTION_ID = "stocktransactions";
        private const string QUOTE_COLLECTION_ID = "quotes";

        private readonly MongoDbOptions _options;
        private readonly IMongoClient _client;
        #endregion

        #region Properties
        public DocumentCollection<StockDocument> Stocks { get; }
        public DocumentCollection<CoinDocument> Coins { get; }
        public DocumentCollection<CoinTradeDocument> CoinTrades { get; }
        public DocumentCollection<StockTransactionDocument> StockTransactions { get; }
        public DocumentCollection<QuoteDocument> Quotes { get; }
        #endregion

        #region Constructor
        public PatchaWalletDbClient (IOptions<MongoDbOptions> optionsAccessor) {
            _options = optionsAccessor.Value;
            _client = new MongoClient (_options.Connection);

            Stocks = new DocumentCollection<StockDocument> (_client, _options.DatabaseId, STOCK_COLLECTION_ID);
            Coins = new DocumentCollection<CoinDocument> (_client, _options.DatabaseId, COIN_COLLECTION_ID);
            CoinTrades = new DocumentCollection<CoinTradeDocument> (_client, _options.DatabaseId, COIN_TRADE_COLLECTION_ID);
            StockTransactions = new DocumentCollection<StockTransactionDocument> (_client, _options.DatabaseId, STOCK_TRADE_COLLECTION_ID);
            Quotes = new DocumentCollection<QuoteDocument> (_client, _options.DatabaseId, QUOTE_COLLECTION_ID);
        }
        #endregion

        #region Methods
        public void EnsureDatabaseCreated () {
            //_client.CreateDatabaseIfNotExistsAsync(new Database { Id = _options.DatabaseId }).Wait();
            //_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_options.DatabaseId), new DocumentCollection { Id = INVESTMENT_COMPANY_COLLECTION_ID }).Wait();
            //_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_options.DatabaseId), new DocumentCollection { Id = PURCHASES_COLLECTION_ID }).Wait();
        }

        public void EnsureDatabaseSeeded () {
            /*
                        Uri charactersCollectionUri = UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, INFLACTIONS_COLLECTION_ID);
                        if (!_client.CreateDocumentQuery<Inflation>(charactersCollectionUri, new FeedOptions { MaxItemCount = -1, }).Where(c => c.Id != "").Take(1).ToList().Any())
                        {
                            //Task.WaitAll(
                            //    _client.CreateDocumentAsync(charactersCollectionUri, new Inflation { Id = Guid.NewGuid().ToString("N"), Name = "Luke Skywalker", Gender = Genders.Male, Height = 172, Weight = 77, BirthYear = "19BBY", SkinColor = SkinColors.Fair, HairColor = HairColors.Blond, EyeColor = EyeColors.Blue, CreatedDate = DateTime.UtcNow, LastUpdatedDate = DateTime.UtcNow })                
                            //);
                        }*/
            /*
            Uri purchasesCollectionUri = UriFactory.CreateDocumentCollectionUri(_options.DatabaseId, PURCHASES_COLLECTION_ID);
            if (!_client.CreateDocumentQuery<PurchaseDocument>(purchasesCollectionUri, new FeedOptions { MaxItemCount = -1, }).Where(c => c.Id != "").Take(1).ToList().Any())
            {
                Task.WaitAll(
                    //_client.CreateDocumentAsync(charactersCollectionUri, 
                    //new Purchase {
                    //    Id = Guid.NewGuid().ToString("N"),
                    //    InvestmentCompany = new InvestmentCompany() {
                    //        Name = "Klabin",
                    //        Code = "KLBN11",

                    //    },
                    //    Gender = Genders.Male,
                    //    Height = 172,
                    //    Weight = 77,
                    //    BirthYear = "19BBY",
                    //    SkinColor = SkinColors.Fair,
                    //    HairColor = HairColors.Blond,
                    //    EyeColor = EyeColors.Blue,
                    //    CreatedDate = DateTime.UtcNow,
                    //    LastUpdatedDate = DateTime.UtcNow
                    //})
                );
            }*/
        }
        #endregion
    }
}