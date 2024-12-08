using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Entities.ApiScopes;
using Starshine.Authservice.Domain.IdentityResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [DependsOn(
    typeof(StarshineAuthserviceDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
    )]
    public class StarshineAuthserviceEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<IIdentityServerBuilder>(
                builder =>
                {
                    builder.AddAbpStores();
                }
            );
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AuthserviceDbContext>(options =>
            {
                options.AddDefaultRepositories<IAuthserviceDbContext>();
                options.AddRepository<Client, ClientRepository>();
                options.AddRepository<ApiResource, ApiResourceRepository>();
                options.AddRepository<ApiScope, ApiScopeRepository>();
                options.AddRepository<IdentityResource, IdentityResourceRepository>();
                options.AddRepository<PersistedGrant, PersistentGrantRepository>();
                options.AddRepository<DeviceFlowCodes, DeviceFlowCodesRepository>();
            });
        }
    }

}
