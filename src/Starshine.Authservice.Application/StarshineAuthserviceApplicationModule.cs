using Starshine.Authservice.Application.Contracts;
using Starshine.Authservice.Domain;
using Volo.Abp.AutoMapper;
using Starshine.Abp.Identity;
using Volo.Abp.Modularity;
using Starshine.Abp.TenantManagement;
using Starshine.Abp.PermissionManagement;

namespace Starshine.Authservice.Application
{
    /// <summary>
    /// 认证服务应用层模块
    /// </summary>
    [DependsOn(
        typeof(StarshineIdentityApplicationModule),
        typeof(StarshinePermissionManagementApplicationModule),
        typeof(StarshineAuthserviceApplicationContractsModule),
        typeof(StarshineAuthserviceDomainModule),
        typeof(StarshineTenantManagementApplicationModule)
    )]
    public class StarshineAuthserviceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<StarshineAuthserviceApplicationModule>();
            });
        }
    }
}
