using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace PatchaWallet.Wallet
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceCollectionExtensions
    {
        private const string MONGODB_CONFIGURATION_SECTION = "MongoDbConnection";

        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(MONGODB_CONFIGURATION_SECTION));
            services.AddScoped<IDocumentDbClient, PatchaWalletDbClient>();
            services.AddScoped<PatchaWalletDbClient>();
            return services;
        }
    }
}
