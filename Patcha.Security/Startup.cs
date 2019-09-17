﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Patcha.Security
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{nameof(AppSettings)}.json")
                .AddJsonFile($"{nameof(AppSettings)}.{environment.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public void Configure(IApplicationBuilder application)
        {
            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
                application.UseDatabaseErrorPage();
            }

            application.UseIdentityServer();
            application.UseStaticFiles();
            application.UseMvcWithDefaultRoute();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<AppSettings>(Configuration);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value);
            var appSettings = services.BuildServiceProvider().GetService<AppSettings>();
            var IdentityServer = appSettings.ConnectionStrings.IdentityServer;

            services.AddHostedService<BackgroundHostedService>();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, string>(IdentityServer, "patchawallet")
                .AddDefaultTokenProviders();

            services
                .AddIdentityServer()
                .AddDatabase()
                .AddClientStore()
                .AddResourceStore()
                .AddAspNetIdentity<ApplicationUser>()
                .AddCertificate(appSettings, Environment);

            ///.AddCorsPolicyService<CorsPoliceService>()
            ///.AddProfileService<ProfileService>()

            ///.AddAuthorizeInteractionResponseGenerator<AuthorizeInteractionResponseGenerator>()
            ///.AddCustomAuthorizeRequestValidator<CustomAuthorizeRequestValidator>()
            ///.AddCustomTokenRequestValidator<CustomTokenRequestValidator>()
            ///.AddExtensionGrantValidator<ExtensionGrantValidator>()
            ///.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
            ///.AddSecretValidator<SecretValidator>();
        }

    }
}
