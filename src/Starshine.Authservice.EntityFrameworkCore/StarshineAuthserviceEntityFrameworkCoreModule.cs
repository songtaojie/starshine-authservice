using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using Starshine.Authservice.Domain.ApiResources;
using Starshine.Authservice.Domain.ApiScopes;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Grants;
using Starshine.Authservice.Domain.IdentityResources;
using Starshine.Authservice.EntityFrameworkCore.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [DependsOn(
        typeof(StarshineAuthserviceDomainModule),
        typeof(AbpIdentityEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpTenantManagementEntityFrameworkCoreModule)
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
                options.AddDefaultRepositories<IAuthserviceDbContext>(includeAllEntities: true);
                options.AddRepository<Client, ClientRepository>();
                options.AddRepository<ApiResource, ApiResourceRepository>();
                options.AddRepository<ApiScope, ApiScopeRepository>();
                options.AddRepository<IdentityResource, IdentityResourceRepository>();
                options.AddRepository<PersistedGrant, PersistentGrantRepository>();
                options.AddRepository<DeviceFlowCodes, DeviceFlowCodesRepository>();
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                //options.UseMySQL();
                options.Configure<AuthserviceDbContext>(op =>
                {
                    op.DbContextOptions.UseSnakeCaseNamingConvention();
                });
                options.UseSqlite();
            });
        }
    }

}
