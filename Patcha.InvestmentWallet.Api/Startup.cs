﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Patcha.InvestmentWallet.Api.CoinGecko.Services;
using Patcha.InvestmentWallet.Api.Extensions;
using Patcha.InvestmentWallet.Api.Interfaces.AlphaVantage;
using Patcha.InvestmentWallet.Api.Interfaces.Bitcointoyou;
using Patcha.InvestmentWallet.Api.Interfaces.Braziliex;
using Patcha.InvestmentWallet.Api.Interfaces.CoinGecko;
using Patcha.InvestmentWallet.Api.Interfaces.IIIxbit;
using Patcha.InvestmentWallet.Api.Interfaces.MercadoBitcoin;
using Patcha.InvestmentWallet.Api.Interfaces.TemBTC;
using Patcha.InvestmentWallet.Api.Services.AlphaVantage;
using Patcha.InvestmentWallet.Api.Services.Bitcointoyou;
using Patcha.InvestmentWallet.Api.Services.Braziliex;
using Patcha.InvestmentWallet.Api.Services.IIIxbit;
using Patcha.InvestmentWallet.Api.Services.MercadoBitcoin;
using Patcha.InvestmentWallet.Api.Services.TemBTC;
using System.Net.Http;

namespace Patcha.InvestmentWallet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoDb(Configuration);

            services.AddMediatR(typeof(Startup).Assembly);

            services.AddTransient<HttpClient>();
            services.AddTransient<ICoinsService, CoinsService>();
            services.AddTransient<ISymbolSearchService, SymbolSearchService>();
            services.AddTransient<IGlobalQuoteService, GlobalQuoteService>();
            services.AddTransient<IMercadoBitcoinService, MercadoBitcoinService>();
            services.AddTransient<IBraziliexService, BraziliexService>();
            services.AddTransient<ITemBTCService, TemBTCService>();
            services.AddTransient<IBitcointoyouService, BitcointoyouService>();
            services.AddTransient<IIIIxbitService, IIIxbitService>();

            // Angular's default header name for sending the XSRF token.
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMongoDbStorage();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
