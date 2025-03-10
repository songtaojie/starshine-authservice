using Starshine.Abp.PermissionManagement;
using Starshine.Authservice.Application.Contracts;
using Starshine.Authservice.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.DbMigrator
{
    [DependsOn(
    typeof(AuthserviceEntityFrameworkCoreModule),
    typeof(StarshineAuthserviceApplicationContractsModule)
    )]
    public class AuthserviceDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
            Configure<PermissionManagementOptions>(options => options.SaveStaticPermissionsToDatabase = false);
            
        }
    }
}
