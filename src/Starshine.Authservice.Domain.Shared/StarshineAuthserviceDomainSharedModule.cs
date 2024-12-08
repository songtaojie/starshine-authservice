using Starshine.Abp.Core;
using Volo.Abp.Domain;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.Domain.Shared
{

    [DependsOn(typeof(AbpDddDomainSharedModule),
    typeof(AbpIdentityServerDomainSharedModule))]
    public class StarshineAuthserviceDomainSharedModule : StarshineAbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}
