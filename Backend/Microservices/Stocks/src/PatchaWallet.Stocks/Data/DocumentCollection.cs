using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks.Data
{
    public class DocumentCollection<T> : IDocumentCollection<T> where T : IDocument
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<T> _collection;

        public DocumentCollection(IMongoClient client, string databaseId, string collectionId)
        {
            _client = client;

            var db = _client.GetDatabase(databaseId);
            _collection = db.GetCollection<T>(collectionId);
        }

        public IQueryable<T> GetDocumentQuery()
        {
            return _collection.Find<T>(document => true).ToList().AsQueryable();
        }

        public Task CreateDocumentAsync(T document)
        {
            return _collection.InsertOneAsync(document);
        }

        public Task CreateDocumentAsync(IEnumerable<T> documents)
        {
            return _collection.InsertManyAsync(documents);
        }

        public Task ReplaceDocumentAsync(string id, T document)
        {
            return _collection.ReplaceOneAsync(d => d.Id == id, document);
        }

        public Task DeleteDocumentAsync(string documentId)
        {
            return _collection.DeleteOneAsync(d => d.Id == documentId);
        }
    }
}
