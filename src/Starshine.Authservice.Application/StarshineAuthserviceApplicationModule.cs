using Starshine.Authservice.Application.Contracts;
using Starshine.Authservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace Starshine.Authservice.Application
{

    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(StarshineAuthserviceApplicationContractsModule),
        typeof(StarshineAuthserviceDomainModule),
        typeof(AbpTenantManagementApplicationModule)
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
