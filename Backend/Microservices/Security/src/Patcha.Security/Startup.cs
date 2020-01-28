using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Patcha.Security
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }   

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<AppSettings>(Configuration);
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<AppSettings>>().Value);
            var appSettings = services.BuildServiceProvider().GetService<AppSettings>();
            var IdentityServer = appSettings.ConnectionStrings.IdentityServer;

            services.AddHostedService<BackgroundHostedService>();

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
                .AddDeveloperSigningCredential();

            services.AddHealthChecks();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "PATCHAWALLET SECURITY API", Version = "v1" });
            });
        }

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
            application.UseAuthentication();
            application.UseSwagger();

            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PATCHAWALLET SECURITY API V1");
            });

            application.UseHealthChecks("/hc",
              new HealthCheckOptions
              {
                  ResponseWriter = async (context, report) =>
                  {
                      var result = JsonConvert.SerializeObject(
                          new
                          {
                              environment = Environment.EnvironmentName,
                              status = report.Status.ToString(),
                              services = report.Entries.Select(e => new { key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) })
                          });
                      context.Response.ContentType = MediaTypeNames.Application.Json;
                      await context.Response.WriteAsync(result);
                  }
              });
        }

    }
}
