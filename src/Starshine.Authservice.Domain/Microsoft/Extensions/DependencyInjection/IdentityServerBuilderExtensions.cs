using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Grants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice
{

    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddAbpStores(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            builder.Services.AddTransient<IDeviceFlowStore, DeviceFlowStore>();

            return builder
            .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddCorsPolicyService<AbpCorsPolicyService>();
        }
    }
}
