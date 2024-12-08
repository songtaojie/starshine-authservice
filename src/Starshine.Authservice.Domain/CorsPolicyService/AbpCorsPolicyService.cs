using IdentityServer4.Configuration;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Starshine.Authservice.Domain.Repositories;

namespace Starshine.Authservice.Domain
{
    public class AbpCorsPolicyService : ICorsPolicyService
    {
        public ILogger<AbpCorsPolicyService> Logger { get; set; }
        protected IServiceScopeFactory HybridServiceScopeFactory { get; }
        protected IDistributedCache<AllowedCorsOriginsCacheItem> Cache { get; }
        protected IdentityServerOptions Options { get; }

        public AbpCorsPolicyService(
            IDistributedCache<AllowedCorsOriginsCacheItem> cache,
            IServiceScopeFactory hybridServiceScopeFactory,
            IOptions<IdentityServerOptions> options)
        {
            Cache = cache;
            HybridServiceScopeFactory = hybridServiceScopeFactory;
            Options = options.Value;
            Logger = NullLogger<AbpCorsPolicyService>.Instance;
        }

        public virtual async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var cacheItem = await Cache.GetOrAddAsync(AllowedCorsOriginsCacheItem.AllOrigins, CreateCacheItemAsync,
                () => new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = Options.Caching.CorsExpiration
                });

            var isAllowed = await IsOriginAllowedAsync(cacheItem.AllowedOrigins, origin);

            if (!isAllowed)
            {
                Logger.LogWarning($"Origin is not allowed: {origin}");
            }

            return isAllowed;
        }

        protected virtual async Task<AllowedCorsOriginsCacheItem> CreateCacheItemAsync()
        {
            // doing this here and not in the ctor because: https://github.com/aspnet/AspNetCore/issues/2377
            using (var scope = HybridServiceScopeFactory.CreateScope())
            {
                var clientRepository = scope.ServiceProvider.GetRequiredService<IClientRepository>();

                return new AllowedCorsOriginsCacheItem
                {
                    AllowedOrigins = (await clientRepository.GetAllDistinctAllowedCorsOriginsAsync()).ToArray()
                };
            }
        }

        protected virtual Task<bool> IsOriginAllowedAsync(string[] allowedOrigins, string origin)
        {
            return Task.FromResult(allowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase));
        }
    }
}
