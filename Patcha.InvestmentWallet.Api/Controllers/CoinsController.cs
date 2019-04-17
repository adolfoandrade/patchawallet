using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;

namespace Patcha.InvestmentWallet.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoinsController : Controller
    {
        private readonly ICoinsService _coinsService;

        public CoinsController(ICoinsService coinsService)
        {
            _coinsService = coinsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string order, int? perPage, int? page, string localization, bool? sparkline)
        {
            var data = await _coinsService.GetAllCoinsDataAsync();

            return Ok(data);
        }
    }
}
