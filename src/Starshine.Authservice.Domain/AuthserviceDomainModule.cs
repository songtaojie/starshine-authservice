using Starshine.Abp.Core;
using Starshine.Authservice.Domain.Shared;
using Volo.Abp.Caching;
using Starshine.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.MultiTenancy;
using Starshine.Authservice.Domain.Shared.Consts;
using Starshine.Abp.TenantManagement;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Starshine.Abp.IdentityServer;
using Starshine.Abp.PermissionManagement;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Starshine.Authservice.Domain
{

    [DependsOn(
        //typeof(AbpBackgroundJobsDomainModule),
        typeof(AuthserviceDomainSharedModule),
        typeof(StarshineIdentityDomainModule),
         typeof(StarshineIdentityServerDomainModule),
        typeof(StarshinePermissionManagementDomainIdentityModule),
        typeof(StarshineTenantManagementDomainModule),
        typeof(AbpCachingModule)
    )]
    public class AuthserviceDomainModule : StarshineAbpModule
    {
        //public override void PreConfigureServices(ServiceConfigurationContext context)
        //{
        //    PreConfigure<IdentityBuilder>(builder =>
        //    {
        //        builder
        //            .AddDefaultTokenProviders()
        //            //.AddTokenProvider<LinkUserTokenProvider>(LinkUserTokenProviderConsts.LinkUserTokenProviderName)
        //            .AddSignInManager<AbpSignInManager>()
        //            .AddUserValidator<AbpIdentityUserValidator>();
        //    });
        //}
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpSettingOptions>(options =>
            {
                options.DefinitionProviders.Add<StarshineLocalizationSettingProvider>();
            });
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = AuthserviceConst.IsEnabledMultiTenancy;
            });
            Configure<PermissionManagementOptions>(options => options.SaveStaticPermissionsToDatabase = false);

        }
    }
}
