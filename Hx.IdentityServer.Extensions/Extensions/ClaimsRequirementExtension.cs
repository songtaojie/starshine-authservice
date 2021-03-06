using Hx.IdentityServer.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Extensions
{
    /// <summary>
    /// ClaimsRequirement扩展
    /// </summary>
    public static class ClaimsRequirementExtension
    {
        /// <summary>
        /// 添加ClaimsRequirement
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddClaimsRequirement(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            return services;
        }
    }
}
