using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace PatchaWallet.Wallet
{
    [ExcludeFromCodeCoverage]
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMongoDbStorage(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                PatchaWalletDbClient client = serviceScope.ServiceProvider.GetService<PatchaWalletDbClient>();
            }
            return app;
        }
    }
}
