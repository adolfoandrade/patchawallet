using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PatchaWallet.Stocks
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/stocks")]
    [ApiController]
    [Authorize]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("{pageSize}/{nextCursor}")]
        public async Task<IActionResult> Get(int pageSize = 10, int nextCursor = 0, string search = "")
        {
            var result = await _stockService.GetStocksAsync(pageSize, nextCursor, search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> Get(string id)
        {
            StockVM stock = await _stockService.GetStockAsync(id);          
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Post(StockVM stock)
        {
            var result = await _stockService.AddStockAsync(stock);
            return Ok(result);
        }

        [HttpPost("add/range")]
        public async Task<IActionResult> PostRange([FromBody]IEnumerable<StockVM> stocks)
        {
            if (!stocks.Any())
                return NotFound();

            var result = await _stockService.AddStocksAsync(stocks);

            return Ok(result);
        }
    }
}
