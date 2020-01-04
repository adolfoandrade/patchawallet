using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Swashbuckle.AspNetCore.Swagger;

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
                IdentityModelEventSource.ShowPII = true;
            }

            application.UseIdentityServer();
            application.UseStaticFiles();
            application.UseMvcWithDefaultRoute();
            application.UseAuthentication();
            
            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
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
                //.AddDeveloperSigningCredential();
                .AddCertificate(appSettings, Environment);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Protected API", Version = "v1" });
            });

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
