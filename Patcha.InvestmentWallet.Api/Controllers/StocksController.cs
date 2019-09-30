using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly List<StockTrade> _swingtrade = new List<StockTrade>{
            new StockTrade(){ Stock = new Stock(){}, Commission = 0, Amount = 100, Price = 10.24M, When = DateTime.Parse("2019/03/09"), TradeType = TradeTypeEnum.BUY },
            new StockTrade(){ Stock = new Stock(){}, Commission = 0, Amount = 100, Price = 10.17M, When = DateTime.Parse("2019/04/09"), TradeType = TradeTypeEnum.BUY },
            new StockTrade(){ Stock = new Stock(){}, Commission = 0, Amount = 100, Price = 10.52M, When = DateTime.Parse("2019/06/09"), TradeType = TradeTypeEnum.SELL }
        };
        #endregion

        #region Constructor
        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<IEnumerable<Stock>> Get()
        {
            return await _mediator.Send(new GetCollectionRequest<Stock>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> Get(string id)
        {
            Stock company = await _mediator.Send(new GetSingleRequest<Stock>(id));
            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

         [HttpPost]
        public async Task<IActionResult> Post(Stock company)
        {
            company = await _mediator.Send(new CreateRequest<Stock>(company));

            return CreatedAtAction(nameof(Get), new { company.Id }, company);
        }
        #endregion
    }
}
