using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.HostedService
{
    public interface IScopedProcessingService
    {
        void DoWork();
    }
}
