using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatchaWallet.Wallet.AspNetIdentity;

namespace PatchaWallet.Wallet
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/wallets")]
    [ApiController]
    [Authorize]
    public class WalletsController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly IUser _user;

        public WalletsController(IWalletService walletService,
            IUser user)
        {
            _walletService = walletService;
            _user = user;
        }

        [HttpGet]
        [ApiExplorerSettings(GroupName = "v1")]
        [ProducesResponseType(typeof(WalletComponentVM), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status401Unauthorized)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public void Post([FromBody]string value)
        {
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
