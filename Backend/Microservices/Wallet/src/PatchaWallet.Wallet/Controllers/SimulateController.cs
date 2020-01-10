using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatchaWallet.Wallet.AspNetIdentity;

namespace PatchaWallet.Wallet
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/wallet/simulate")]
    [ApiController]
    [Authorize]
    public class SimulateController : ControllerBase
    {
        private readonly IWalletService _walletService;
        private readonly IUser _user;

        public SimulateController(IWalletService walletService,
            IUser user)
        {
            _walletService = walletService;
            _user = user;
        }

        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(typeof(SimulateGoalResultVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await _walletService.GetAsync(id);

            return Ok(result);
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SimulateGoalResultVM), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(DomainNotification), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(SimulateGoalVM simulateGoalVM)
        {
            var user = _user.Name;

            var result = await _walletService.AddAsync(simulateGoalVM);

            return Ok(result);
        }

        [HttpPut]
        [ApiExplorerSettings(GroupName = "v1")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(SimulateGoalResultVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DomainNotification), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Put(string id, SimulateGoalVM simulateGoalVM)
        {
            var result = await _walletService.AddAsync(simulateGoalVM);

            return Ok(result);
        }
    }
}
