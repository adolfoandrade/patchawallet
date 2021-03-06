using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Patcha.InvestmentWallet.Data.DocumentDb;

namespace Patcha.InvestmentWallet.Api.Extensions {
    internal static class ApplicationBuilderExtensions {
        public static IApplicationBuilder UseMongoDbStorage (this IApplicationBuilder app) {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory> ().CreateScope ()) {
                PatchaWalletDbClient client = serviceScope.ServiceProvider.GetService<PatchaWalletDbClient> ();
                client.EnsureDatabaseCreated ();
            }
            return app;
        }
    }
}