using Starshine.Abp.Core;
using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Starshine.Authservice.Domain.Shared
{

    [DependsOn(typeof(AbpDddDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule))]
    public class StarshineAuthserviceDomainSharedModule : StarshineAbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}
