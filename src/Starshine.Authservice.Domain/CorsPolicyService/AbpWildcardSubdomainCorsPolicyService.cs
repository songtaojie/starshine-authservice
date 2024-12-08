using IdentityServer4.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.Text.Formatting;

namespace Starshine.Authservice.Domain
{
    public class AbpWildcardSubdomainCorsPolicyService : AbpCorsPolicyService
    {
        public AbpWildcardSubdomainCorsPolicyService(
            IDistributedCache<AllowedCorsOriginsCacheItem> cache,
            IServiceScopeFactory hybridServiceScopeFactory,
            IOptions<IdentityServerOptions> options)
                : base(cache, hybridServiceScopeFactory, options)
        {

        }

        protected override async Task<bool> IsOriginAllowedAsync(string[] allowedOrigins, string origin)
        {
            var isAllowed = await base.IsOriginAllowedAsync(allowedOrigins, origin);
            if (isAllowed)
            {
                return true;
            }

            foreach (var url in allowedOrigins)
            {
                var extractResult = FormattedStringValueExtracter.Extract(origin, url, ignoreCase: true);
                if (extractResult.IsMatch)
                {
                    return extractResult.Matches.Aggregate(url, (current, nameValue) => current.Replace($"{{{nameValue.Name}}}", nameValue.Value))
                        .Contains(origin, StringComparison.OrdinalIgnoreCase);
                }

                if (url.Replace("{0}.", "").Contains(origin, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
