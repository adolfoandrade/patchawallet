using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Patcha.InvestmentWallet.Domain.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Patcha.InvestmentWallet.Data.DocumentDb
{
    public class PatchaWalletDbClient
    {
        #region Fields
        //private const string INFLACTIONS_COLLECTION_ID = "InflactionsCollection";
        private const string INVESTMENT_COMPANY_COLLECTION_ID = "InvestmentCompaniesCollection";
        private const string PURCHASES_COLLECTION_ID = "PurchasesCollection";

        private readonly MongoDbOptions _options;
        private readonly IMongoClient _client;
        #endregion

        #region Properties
        public DocumentCollection<InvestmentCompanyDocument> Companies { get; }
        public DocumentCollection<PurchaseDocument> Purchases { get; }
        #endregion

        #region Constructor
        public PatchaWalletDbClient(IOptions<MongoDbOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _client = new MongoClient(_options.Connection);

            Companies = new DocumentCollection<InvestmentCompanyDocument>(_client, _options.DatabaseId, INVESTMENT_COMPANY_COLLECTION_ID);
            Purchases = new DocumentCollection<PurchaseDocument>(_client, _options.DatabaseId, PURCHASES_COLLECTION_ID);
        }
        #endregion

        #region Methods
        public void EnsureDatabaseCreated()
        {
            //_client.CreateDatabaseIfNotExistsAsync(new Database { Id = _options.DatabaseId }).Wait();
           // _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_options.DatabaseId), new DocumentCollection { Id = INVESTMENT_COMPANY_COLLECTION_ID }).Wait();
            //_client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_options.DatabaseId), new DocumentCollection { Id = PURCHASES_COLLECTION_ID }).Wait();
        }

        public void EnsureDatabaseSeeded()
        {/*
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
