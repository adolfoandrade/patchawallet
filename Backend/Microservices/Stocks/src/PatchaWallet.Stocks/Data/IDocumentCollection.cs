using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface IDocumentCollection<T> where T : IDocument
    {
        IQueryable<T> GetDocumentQuery();
        Task CreateDocumentAsync(T document);
        Task CreateDocumentAsync(IEnumerable<T> documents);
        Task ReplaceDocumentAsync(string id, T document);
        Task DeleteDocumentAsync(string documentId);
    }
}
