using Microsoft.Azure.Documents.Client;
using MongoDB.Driver;
using Patcha.InvestmentWallet.Domain.Documents;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Data.DocumentDb
{
    public class DocumentCollection<T> where T : IDocument
    {
        #region Fields
        private readonly IMongoClient _client;
        private readonly IMongoCollection<T> _collection;
        #endregion

        #region Constructor
        public DocumentCollection(IMongoClient client, string databaseId, string collectionId)
        {
            _client = client;

            var db = _client.GetDatabase(databaseId);
            _collection = db.GetCollection<T>(collectionId);
        }
        #endregion

        #region Methods
        public IQueryable<T> GetDocumentQuery()
        {
            return _collection.Find<T>(document => true).ToList().AsQueryable();
        }

        public Task CreateDocumentAsync(T document)
        {
            return _collection.InsertOneAsync(document);
        }

        public Task ReplaceDocumentAsync(string id, T document)
        {
            return _collection.ReplaceOneAsync(d => d.Id == id, document);
        }

        public Task DeleteDocumentAsync(string documentId)
        {
            return _collection.DeleteOneAsync(d => d.Id == documentId);
        }
        #endregion
    }
}
