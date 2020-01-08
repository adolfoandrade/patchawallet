using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PatchaWallet.Wallet
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/wallet/simulate")]
    [ApiController]
    [Authorize]
    public class SimulateController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public SimulateController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _walletService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(SimulateGoalVM simulateGoalVM)
        {
            var result = await _walletService.AddAsync(simulateGoalVM);

            return Ok(result);
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(string id, SimulateGoalVM simulateGoalVM)
        {
            var result = await _walletService.AddAsync(simulateGoalVM);

            return Ok(result);
        }
    }
}
