using Starshine.IdentityServer.Configuration;
using Starshine.IdentityServer.Services;
using Starshine.IdentityServer.Stores;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Abp.Core;
using Starshine.Authservice.Domain.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Starshine.Abp.Identity;
using Starshine.Abp.IdentityServer.ApiResources;
using Starshine.Abp.IdentityServer.Clients;
using Starshine.Abp.IdentityServer.Devices;
using Starshine.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.IdentityResources;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Starshine.Authservice.Domain.ApiResources;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.MultiTenancy;
using Starshine.Authservice.Domain.Shared.Consts;
using Starshine.Abp.TenantManagement;

namespace Starshine.Authservice.Domain
{

    [DependsOn(
        typeof(StarshineAuthserviceDomainSharedModule),
        typeof(StarshineIdentityDomainModule),
        typeof(StarshinePermissionManagementDomainIdentityModule),
        typeof(AbpCachingModule),
        typeof(StarshineTenantManagementDomainModule)
    )]
    public class StarshineAuthserviceDomainModule : StarshineAbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
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
            context.Services.AddAutoMapperObjectMapper<StarshineAuthserviceDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AuthserviceAutoMapperProfile>(validate: true);
            });
            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = ConmmonConst.IsEnabledMultiTenancy;
            });
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<ApiResources.ApiResource, Starshine.Abp.IdentityServer.ApiResources.ApiResourceEto>(typeof(StarshineAuthserviceDomainModule));
                options.EtoMappings.Add<Clients.Client, ClientEto>(typeof(StarshineAuthserviceDomainModule));
                options.EtoMappings.Add<Devices.DeviceFlowCodes, DeviceFlowCodesEto>(typeof(StarshineAuthserviceDomainModule));
                options.EtoMappings.Add<IdentityResources.IdentityResource, IdentityResourceEto>(typeof(StarshineAuthserviceDomainModule));
            });

            Configure<AbpClaimsServiceOptions>(options =>
            {
                options.RequestedClaims.AddRange(new[]
                {
                    AbpClaimTypes.TenantId,
                    AbpClaimTypes.EditionId
                });
            });

            AddIdentityServer(context.Services);
        }

        private static void AddIdentityServer(IServiceCollection services)
        {
            var configuration = services.GetConfiguration();
            var builderOptions = services.ExecutePreConfiguredActions<AbpIdentityServerBuilderOptions>();

            var identityServerBuilder = AddIdentityServer(services, builderOptions);

            if (builderOptions.AddDeveloperSigningCredential)
            {
                identityServerBuilder = identityServerBuilder.AddDeveloperSigningCredential();
            }

            identityServerBuilder.AddAbpIdentityServer(builderOptions);

            services.ExecutePreConfiguredActions(identityServerBuilder);

            if (!services.IsAdded<IPersistedGrantService>())
            {
                services.TryAddSingleton<IPersistedGrantStore, InMemoryPersistedGrantStore>();
            }

            if (!services.IsAdded<IDeviceFlowStore>())
            {
                services.TryAddSingleton<IDeviceFlowStore, InMemoryDeviceFlowStore>();
            }

            if (!services.IsAdded<IClientStore>())
            {
                identityServerBuilder.AddInMemoryClients(configuration.GetSection("IdentityServer:Clients"));
            }

            if (!services.IsAdded<IResourceStore>())
            {
                identityServerBuilder.AddInMemoryApiResources(configuration.GetSection("IdentityServer:ApiResources"));
                identityServerBuilder.AddInMemoryIdentityResources(configuration.GetSection("IdentityServer:IdentityResources"));
            }
        }

        private static IIdentityServerBuilder AddIdentityServer(IServiceCollection services, AbpIdentityServerBuilderOptions abpIdentityServerBuilderOptions)
        {
            services.Configure<IdentityServerOptions>(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            });

            var identityServerBuilder = services.AddIdentityServerBuilder()
                .AddRequiredPlatformServices()
                .AddCoreServices()
                .AddDefaultEndpoints()
                .AddPluggableServices()
                .AddValidators()
                .AddResponseGenerators()
                .AddDefaultSecretParsers()
                .AddDefaultSecretValidators();

            if (abpIdentityServerBuilderOptions.AddIdentityServerCookieAuthentication)
            {
                identityServerBuilder.AddCookieAuthentication();
            }

            // provide default in-memory implementation, not suitable for most production scenarios
            identityServerBuilder.AddInMemoryPersistedGrants();

            return identityServerBuilder;
        }

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityServerModuleExtensionConsts.ModuleName,
                    IdentityServerModuleExtensionConsts.EntityNames.Client,
                    typeof(Clients.Client)
                );

                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityServerModuleExtensionConsts.ModuleName,
                    IdentityServerModuleExtensionConsts.EntityNames.IdentityResource,
                    typeof(IdentityResources.IdentityResource)
                );

                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityServerModuleExtensionConsts.ModuleName,
                    IdentityServerModuleExtensionConsts.EntityNames.ApiResource,
                    typeof(ApiResources.ApiResource)
                );
            });
        }

        //public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        //{
        //    var options = context.ServiceProvider.GetRequiredService<IOptions<TokenCleanupOptions>>().Value;
        //    if (options.IsCleanupEnabled)
        //    {
        //        IBackgroundWorker backgroundWorker = context.ServiceProvider.GetRequiredService<TokenCleanupBackgroundWorker>();
        //        IBackgroundWorkerManager backgroundWorkerManager = context.ServiceProvider
        //            .GetRequiredService<IBackgroundWorkerManager>();
        //       await backgroundWorkerManager.AddAsync(backgroundWorker);
        //    }
        //}

        //public override void OnApplicationInitialization(ApplicationInitializationContext context)
        //{
        //    AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
        //}
    }
}
