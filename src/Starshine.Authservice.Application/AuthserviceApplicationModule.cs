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

namespace Starshine.Authservice.Application
{

    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AuthserviceApplicationContractsModule),
        typeof(AuthserviceDomainModule)
        )]
    public class AuthserviceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AuthserviceApplicationModule>();
            });
        }
    }
}
