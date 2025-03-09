using Starshine.Abp.Core;
using Starshine.Abp.Identity;
using Starshine.Abp.IdentityServer;
using Starshine.Abp.PermissionManagement;
using Starshine.Abp.TenantManagement;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.Domain.Shared
{

    [DependsOn(typeof(AbpDddDomainSharedModule),
         typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(StarshinePermissionManagementDomainSharedModule),
        typeof(StarshineIdentityDomainSharedModule),
        typeof(StarshineTenantManagementDomainSharedModule),
        typeof(StarshineIdentityServerDomainSharedModule))]
    public class AuthserviceDomainSharedModule : StarshineAbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
