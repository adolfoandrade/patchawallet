using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatchaWallet.Stocks.Data;

namespace PatchaWallet.Stocks
{
    internal static class ServiceCollectionExtensions
    {
        private const string MONGODB_CONFIGURATION_SECTION = "MongoDbConnection";

        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(MONGODB_CONFIGURATION_SECTION));

            services.AddScoped<PatchaWalletDbClient>();

            return services;
        }
    }
}
