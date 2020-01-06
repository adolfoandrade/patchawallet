using IdentityServer4.Stores;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Patcha.Security
{
    public static class IIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCertificate(this IIdentityServerBuilder builder, AppSettings appSettings, IHostingEnvironment environment)
        {
            X509Certificate2 certificate = null;
            certificate = new X509Certificate2(Path.Combine(environment.ContentRootPath, "PatchaSecurity.pfx"), "", X509KeyStorageFlags.MachineKeySet);
            /*
            if (environment.IsDevelopment())
            {
                certificate = new X509Certificate2(Path.Combine(environment.ContentRootPath, "PatchaSecurity.pfx"), "", X509KeyStorageFlags.MachineKeySet);
            }
            else
            {
                using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                {
                    store.Open(OpenFlags.ReadOnly);

                    var collection = store.Certificates.Find(X509FindType.FindByThumbprint, appSettings.CertificateThumbprint, false);

                    if (collection.Count > 0)
                    {
                        certificate = collection[0];
                    }
                }
            }*/

            if (certificate == null)
            {
                throw new ArgumentNullException(nameof(certificate));
            }

            builder.AddSigningCredential(certificate);

            return builder;
        }

        public static IIdentityServerBuilder AddClientStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IClientStore, ClientStore>();

            return builder;
        }

        public static IIdentityServerBuilder AddDatabase(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IDatabaseInitialize, DatabaseInitialize>();
            builder.Services.AddSingleton<IRepository, Repository>();
            builder.Services.BuildServiceProvider().GetService<IDatabaseInitialize>().Initialize();

            return builder;
        }

        public static IIdentityServerBuilder AddResourceStore(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IResourceStore, ResourceStore>();

            return builder;
        }

    }
}
