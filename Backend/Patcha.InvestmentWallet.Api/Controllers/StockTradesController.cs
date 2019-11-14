using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Patcha.InvestmentWallet.Api.Interfaces;
using Patcha.InvestmentWallet.Domain.DomainNotification;
using Patcha.InvestmentWallet.Domain.Model;

namespace Patcha.InvestmentWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTradesController : ControllerBase
    {
        #region Fields
        private readonly IStockTradeService _stockTradeService;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly ILogger<StockTradesController> _logger;

        #endregion

        #region Constructor
        public StockTradesController(
            IStockTradeService stockTradeService,
            IDomainNotificationHandler<DomainNotification> notifications,
            ILogger<StockTradesController> logger)
        {
            _stockTradeService = stockTradeService;
            _notifications = notifications;
            _logger = logger;
        }
        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Get(int year = 0, int month = 0, int day = 0, string stock = null)
        {
            var vm = await _stockTradeService.GetAsync(year, month, day, stock);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(vm);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoryTrade(int year = 0, int month = 0, int day = 0, string stock = null)
        {
            var vm = await _stockTradeService.GetTradeHistory(year, month, day, stock);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(vm);
        }

        #endregion
    }

}
