using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Starshine.Authservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AbpIdentityServerServiceCollectionExtensions
    {
        //public static void AddAbpStrictRedirectUriValidator(this IServiceCollection services)
        //{
        //    services.Replace(ServiceDescriptor.Transient<IRedirectUriValidator, AbpStrictRedirectUriValidator>());
        //}


        public static void AddAbpWildcardSubdomainCorsPolicyService(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<ICorsPolicyService, AbpWildcardSubdomainCorsPolicyService>());
        }
    }
}
