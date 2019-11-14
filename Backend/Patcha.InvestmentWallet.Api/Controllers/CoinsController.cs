using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;

namespace Patcha.InvestmentWallet.Api
{
    [Route("api/[controller]")]
    public class CoinsController : ControllerBase
    {
        private readonly ICoinsService _coinsService;

        public CoinsController(ICoinsService coinsService,
            IMediator mediator)
        {
            _coinsService = coinsService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string order, int? perPage, int? page, string localization, bool? sparkline)
        {
            var coins = await _coinsService.GetAllCoinsDataAsync(order, perPage, page, localization, sparkline);
            return Ok(coins);
        }
    }
}
