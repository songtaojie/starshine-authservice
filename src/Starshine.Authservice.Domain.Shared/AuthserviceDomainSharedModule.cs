using Starshine.Abp.Core;
using Volo.Abp.Domain;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.Domain.Shared
{

    [DependsOn(typeof(AbpDddDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule))]
    public class AuthserviceDomainSharedModule : StarshineAbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}
