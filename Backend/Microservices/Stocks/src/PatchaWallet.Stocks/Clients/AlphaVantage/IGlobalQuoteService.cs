using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface IGlobalQuoteService
    {
        Task<GlobalQuoteResult> GetGlobalQuote(string function, string symbol);
    }
}
