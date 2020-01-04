﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Wallet
{
    public interface IWalletService : IDisposable
    {
        Task<SimulateGoalResultVM> GetAsync(string id);
        Task<SimulateGoalResultVM> AddAsync(SimulateGoalVM simulateGoalVM);
    }
}
