using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using Volo.Abp.EntityFrameworkCore;
using Starshine.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Starshine.Abp.TenantManagement.EntityFrameworkCore;
using Starshine.Abp.PermissionManagement.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [DependsOn(
        typeof(AuthserviceDomainModule),
        typeof(StarshineIdentityEntityFrameworkCoreModule),
        typeof(StarshinePermissionManagementEntityFrameworkCoreModule),
        typeof(StarshineTenantManagementEntityFrameworkCoreModule)
    )]
    public class AuthserviceEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AuthserviceDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();
            });
        }
    }

}
