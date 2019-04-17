using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Domain.Documents
{
    public class PurchaseDocument : Trade, IDocument
    {
    }
}
