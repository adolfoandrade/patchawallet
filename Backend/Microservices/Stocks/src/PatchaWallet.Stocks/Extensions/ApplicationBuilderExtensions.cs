using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PatchaWallet.Stocks
{
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
