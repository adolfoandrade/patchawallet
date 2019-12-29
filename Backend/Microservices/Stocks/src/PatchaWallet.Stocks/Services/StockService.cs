using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class StockService : IStockService
    {
        private readonly IMediator _mediator;

        public StockService(IMediator mediator) 
        {
            _mediator = mediator;
        }

        public async Task<StockVM> AddStockAsync(StockVM stockVM)
        {
            var stock = await _mediator.Send(new CreateRequest<StockVM>(stockVM));
            return stock;
        }

        public async Task<StockVM> GetStockAsync(string id)
        {
            Stock stock = await _mediator.Send(new GetSingleRequest<Stock>(id));
            var stockVM = stock.ToVM();
            return stockVM;
        }

        public async Task<IEnumerable<StockVM>> GetStocksAsync(int pageSize = 10, int nextCursor = 0, string search = "")
        {
            var collection = await _mediator.Send(new GetCollectionRequest<Stock>(pageSize, nextCursor, search));
            var vm = collection.ToVM();
            return vm;
        }

        public async Task<StockVM> AddStocksAsync(IEnumerable<StockVM> stocks)
        {
            var stock = await _mediator.Send(new CreateRangeRequest<StockVM>(stocks));
            return stock;
        }
    }
}
