using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using Volo.Abp.EntityFrameworkCore;
using Starshine.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Starshine.Abp.TenantManagement.EntityFrameworkCore;
using Starshine.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Starshine.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Threading;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [DependsOn(
        typeof(AuthserviceDomainModule),
        typeof(StarshineIdentityEntityFrameworkCoreModule),
        typeof(StarshinePermissionManagementEntityFrameworkCoreModule),
        typeof(StarshineTenantManagementEntityFrameworkCoreModule),
        typeof(StarshineIdentityServerEntityFrameworkCoreModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule)
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

