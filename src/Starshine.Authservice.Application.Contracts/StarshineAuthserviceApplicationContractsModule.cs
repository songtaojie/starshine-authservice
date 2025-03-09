using Starshine.Authservice.Domain.Shared;
using Starshine.Abp.Identity;
using Volo.Abp.Modularity;
using Starshine.Abp.TenantManagement;
using Starshine.Abp.PermissionManagement;

namespace Starshine.Authservice.Application.Contracts
{
    [DependsOn(
        typeof(AuthserviceDomainSharedModule),
        typeof(StarshineIdentityApplicationContractsModule),
        typeof(StarshinePermissionManagementApplicationContractsModule),
        typeof(StarshineTenantManagementApplicationContractsModule)
    )]
    public class StarshineAuthserviceApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }
    }

}
