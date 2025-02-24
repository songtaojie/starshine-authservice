using Starshine.Abp.Core;
using Starshine.Abp.Identity;
using Starshine.Abp.PermissionManagement;
using Starshine.Abp.TenantManagement;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.Domain.Shared
{

    [DependsOn(typeof(AbpDddDomainSharedModule),
        typeof(StarshinePermissionManagementDomainSharedModule),
        typeof(StarshineIdentityDomainSharedModule),
        typeof(StarshineTenantManagementDomainSharedModule))]
    public class StarshineAuthserviceDomainSharedModule : StarshineAbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
