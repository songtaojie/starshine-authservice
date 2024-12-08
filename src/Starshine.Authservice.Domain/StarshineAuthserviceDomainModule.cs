﻿using IdentityServer4.Configuration;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Starshine.Abp.Core;
using Starshine.Authservice.Domain.Devices;
using Starshine.Authservice.Domain.Shared;
using Starshine.Authservice.Domain.Tokens;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;
using Starshine.Authservice.Domain.Clients;
using Starshine.Authservice.Domain.IdentityResources;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Starshine.Authservice.Domain
{

    [DependsOn(
        typeof(StarshineAuthserviceDomainSharedModule),
        typeof(AbpIdentityDomainModule),
        typeof(AbpDddDomainModule),
        typeof(AbpCachingModule)
        )]
    public class StarshineAuthserviceDomainModule : StarshineAbpModule
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<StarshineAuthserviceDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AuthserviceAutoMapperProfile>(validate: true);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<ApiResource, ApiResourceEto>(typeof(StarshineAuthserviceDomainModule));
                options.EtoMappings.Add<Client, ClientEto>(typeof(StarshineAuthserviceDomainModule));
                options.EtoMappings.Add<DeviceFlowCodes, DeviceFlowCodesEto>(typeof(StarshineAuthserviceDomainModule));
                options.EtoMappings.Add<IdentityResource, IdentityResourceEto>(typeof(StarshineAuthserviceDomainModule));
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
                    typeof(Client)
                );

                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityServerModuleExtensionConsts.ModuleName,
                    IdentityServerModuleExtensionConsts.EntityNames.IdentityResource,
                    typeof(IdentityResource)
                );

                ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToEntity(
                    IdentityServerModuleExtensionConsts.ModuleName,
                    IdentityServerModuleExtensionConsts.EntityNames.ApiResource,
                    typeof(ApiResource)
                );
            });
        }

        public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
        {
            var options = context.ServiceProvider.GetRequiredService<IOptions<TokenCleanupOptions>>().Value;
            if (options.IsCleanupEnabled)
            {
                await context.ServiceProvider
                    .GetRequiredService<IBackgroundWorkerManager>()
                    .AddAsync(
                        context.ServiceProvider
                            .GetRequiredService<TokenCleanupBackgroundWorker>()
                    );
            }
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(() => OnApplicationInitializationAsync(context));
        }
    }
}
