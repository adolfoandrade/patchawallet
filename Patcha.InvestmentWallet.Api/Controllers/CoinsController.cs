using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinsService _coinsService;
        private readonly IMediator _mediator;

        public CoinsController(ICoinsService coinsService,
            IMediator mediator)
        {
            _coinsService = coinsService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string order, int? perPage, int? page, string localization, bool? sparkline)
        {
            var coins = await _coinsService.GetAllCoinsDataAsync();
            return Ok(coins);
        }
    }
}
