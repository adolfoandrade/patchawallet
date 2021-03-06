﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PatchaWallet.Wallet
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/wallet/simulate")]
    [ApiController]
    //[Authorize]
    public class SimulateController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public SimulateController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SimulateGoalVM simulateGoalVM)
        {
            var result = await _walletService.AddAsync(simulateGoalVM);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string id, SimulateGoalVM simulateGoalVM)
        {
            var result = await _walletService.AddAsync(simulateGoalVM);

            return Ok(result);
        }
    }
}
