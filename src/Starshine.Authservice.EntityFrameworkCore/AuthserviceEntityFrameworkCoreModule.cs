using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using Volo.Abp.EntityFrameworkCore;
using Starshine.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Starshine.Abp.TenantManagement.EntityFrameworkCore;
using Starshine.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.MySQL;
using Starshine.Abp.IdentityServer.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore
{
    [DependsOn(
        typeof(AuthserviceDomainModule),
        typeof(StarshineIdentityEntityFrameworkCoreModule),
        typeof(StarshinePermissionManagementEntityFrameworkCoreModule),
        typeof(StarshineTenantManagementEntityFrameworkCoreModule),
        typeof(StarshineIdentityServerEntityFrameworkCoreModule),
        //typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreMySQLModule)
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
                options.AddDefaultRepositories(includeAllEntities: true);
            });
            var configuration = context.Services.GetConfiguration();
            Configure<AbpDbContextOptions>(options => options.UseDynamicSql(configuration));
        }
    }
}

