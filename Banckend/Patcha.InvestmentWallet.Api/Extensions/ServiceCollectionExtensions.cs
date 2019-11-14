using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Patcha.InvestmentWallet.Data.DocumentDb;

namespace Patcha.InvestmentWallet.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        private const string COSMOSDB_CONFIGURATION_SECTION = "CosmosDbConnection";
        private const string MONGODB_CONFIGURATION_SECTION = "MongoDbConnection";

        public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DocumentDbOptions>(configuration.GetSection(COSMOSDB_CONFIGURATION_SECTION));

            services.AddScoped<PatchaWalletDbClient>();

            return services;
        }

        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbOptions>(configuration.GetSection(MONGODB_CONFIGURATION_SECTION));

            services.AddScoped<PatchaWalletDbClient>();

            return services;
        }
    }
}
