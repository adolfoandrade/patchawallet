using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public interface IUserService
    {
        Task<UserVM> GetByNameAsync(string userName);
        Task<UserVM> GetByIdAsync(string id);
    }
}
