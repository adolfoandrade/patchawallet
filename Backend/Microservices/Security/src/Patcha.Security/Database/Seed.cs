using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patcha.Security
{
    public static class Seed
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource
                {
                    Name = "wallets",
                    Description = "Wallets Service",
                    DisplayName = "Wallets",
                    Scopes = { new Scope("wallets") },
                    ApiSecrets = { new Secret("secret".Sha512()) },
                    UserClaims = new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, "office" }
                },
                new ApiResource
                {
                    Name = "trades",
                    Description = "Trades Service",
                    DisplayName = "TradesService",
                    Scopes = { new Scope("trades") },
                    ApiSecrets = { new Secret("secret".Sha512()) },
                    UserClaims = new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, "office" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "wallet",
                    ClientName = "Wallet",
                    ClientSecrets = { new Secret("secret".Sha512()) },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "wallets" },
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenLifetime = 60 * 60 * 24,
                },
                new Client
                {
                    ClientId = "trade",
                    ClientName = "Trade",
                    ClientSecrets = { new Secret("secret".Sha512()) },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "trades" },
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenLifetime = 60 * 60 * 24,
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApplicationUser> GetUsers()
        {
            return new[]
            {
                new ApplicationUser { UserName = "admin" }
            };
        }
    }

}
