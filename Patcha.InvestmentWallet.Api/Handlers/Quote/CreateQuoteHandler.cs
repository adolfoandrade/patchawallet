using MongoDB.Bson;
using Patcha.InvestmentWallet.Data.DocumentDb;
using Patcha.InvestmentWallet.Domain.Documents;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.Handlers
{
    public class UpdateQuoteHandler : ICreateHandler<Quote>
    {
        #region Fields
        private readonly PatchaWalletDbClient _client;
        #endregion

        #region Constructor
        public UpdateQuoteHandler(PatchaWalletDbClient client)
        {
            _client = client;
        }
        #endregion

        #region Methods
        public async Task<Quote> Handle(CreateRequest<Quote> request, CancellationToken cancellationToken)
        {
            var quote = await _client.Quotes.GetDocumentQuery().Where(c => c.Id == request.Item.Id || c.Symbol == request.Item.Symbol).ToAsyncEnumerable().FirstOrDefault();

            if(quote == null)
            {
                QuoteDocument quoteDocument = new QuoteDocument
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Symbol = request.Item.Symbol,
                    Open = request.Item.Open,
                    PreviousClose = request.Item.PreviousClose,
                    Hight = request.Item.Hight,
                    Low = request.Item.Low,
                    Price = request.Item.Price,
                    Volume = request.Item.Volume,
                    Change = request.Item.Change,
                    ChangePercent = request.Item.ChangePercent,
                    LastRequest = DateTime.Now,
                    LatestTradingDay = request.Item.LatestTradingDay
                };
                await _client.Quotes.CreateDocumentAsync(quoteDocument);

                return quoteDocument;
            }
            else
            {

                quote.Symbol = request.Item.Symbol;
                quote.Open = request.Item.Open;
                quote.PreviousClose = request.Item.PreviousClose;
                quote.Hight = request.Item.Hight;
                quote.Low = request.Item.Low;
                quote.Price = request.Item.Price;
                quote.Volume = request.Item.Volume;
                quote.Change = request.Item.Change;
                quote.ChangePercent = request.Item.ChangePercent;
                quote.LastRequest = DateTime.Now;
                quote.LatestTradingDay = request.Item.LatestTradingDay;
                await _client.Quotes.ReplaceDocumentAsync(quote.Id, quote);
                return quote;
            }
            
        }
        #endregion
    }
}
