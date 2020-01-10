using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PatchaWallet.Stocks
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/transactions")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ITransactionService _transactionService;
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(
            IWebHostEnvironment hostingEnvironment,
            ITransactionService transactionService,
            IDomainNotificationHandler<DomainNotification> notifications,
            ILogger<TransactionsController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _transactionService = transactionService;
            _notifications = notifications;
            _logger = logger;
        }

        [HttpPost("cei")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> ImportFromCEI()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string newPath = Path.Combine(contentRootPath + "\n" + webRootPath, folderName);

            var negotiations_to_import = await _transactionService.ImportFromCEIAsync(file, newPath);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(negotiations_to_import);
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> Get(string id)
        {
            var stock_transaction = await _transactionService.GetAsync(id);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(stock_transaction);
        }

        [HttpPost]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> Post(CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            var stock_trasaction = await _transactionService.CreateAsync(createOrUpdateStockTransactionVM);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(stock_trasaction);
        }

        [HttpPut("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> Put(string id, CreateOrUpdateStockTransactionVM createOrUpdateStockTransactionVM)
        {
            createOrUpdateStockTransactionVM.Id = id;
            var stock_trasaction = await _transactionService.CreateAsync(createOrUpdateStockTransactionVM);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok(stock_trasaction);
        }

        [HttpDelete("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<IActionResult> Delete(string id)
        {
            await _transactionService.DeleteAsync(id);

            if (_notifications.HasNotifications)
                return BadRequest(_notifications.Notifications);

            return Ok();
        }

    }
}
