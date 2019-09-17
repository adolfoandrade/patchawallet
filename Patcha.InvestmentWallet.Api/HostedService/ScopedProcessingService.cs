using MediatR;
using Microsoft.Extensions.Logging;
using Patcha.InvestmentWallet.Api.Interfaces.AlphaVantage;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Entities.Response;
using Patcha.InvestmentWallet.Domain.AlphaVantage.Parameters;
using Patcha.InvestmentWallet.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patcha.InvestmentWallet.Api.HostedService
{
    public class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger _logger;
        private readonly IGlobalQuoteService _globalQuoteService;
        private readonly IMediator _mediator;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger,
            IGlobalQuoteService globalQuoteService,
            IMediator mediator)
        {
            _logger = logger;
            _globalQuoteService = globalQuoteService;
            _mediator = mediator;
        }

        public void DoWork()
        {
            _logger.LogInformation("Scoped Processing Service is working.");

            var companies = _mediator.Send(new GetCollectionRequest<Stock>()).Result;

            foreach (var company in companies)
            {
                GlobalQuoteResult result = null;
                do
                {
                    Thread.Sleep(60001);
                    result = _globalQuoteService.GetGlobalQuote(Functions.GLOBAL_QUOTE, company.Code).Result;
                }
                while (result.GlobalQuote == null);
                
                if (result != null && result.GlobalQuote != null)
                {
                    var quote = new Quote()
                    {
                        Id = company.Id,
                        Symbol = result.GlobalQuote.Symbol,
                        Open = result.GlobalQuote.Open,
                        PreviousClose = result.GlobalQuote.PreviousClose,
                        Hight = result.GlobalQuote.Hight,
                        Low = result.GlobalQuote.Low,
                        Price = result.GlobalQuote.Price,
                        Volume = result.GlobalQuote.Volume,
                        Change = result.GlobalQuote.Change,
                        ChangePercent = result.GlobalQuote.ChangePercent,
                        LastRequest = DateTime.Now,
                        LatestTradingDay = result.GlobalQuote.LatestTradingDay
                    };

                    var inserted = _mediator.Send(new CreateRequest<Quote>(quote)).Result;
                }
            }
        }
    }
}
