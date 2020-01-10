using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchaWallet.Stocks
{
    public class UserService : IUserService
    {
        private readonly PatchaWalletDbClient _client;

        public UserService(PatchaWalletDbClient client)
        {
            _client = client;
        }

        public Task<UserVM> GetByIdAsync(string id)
        {
            return Task.Factory.StartNew(() => { 
                return _client.Users.GetDocumentQuery().FirstOrDefault(x => x.Id == id).ToVM();
            });
        }

        public Task<UserVM> GetByNameAsync(string userName)
        {
            return Task.Factory.StartNew(() => {
                return _client.Users.GetDocumentQuery().FirstOrDefault(x => x.UserName == userName).ToVM();
            });
        }
    }
}
