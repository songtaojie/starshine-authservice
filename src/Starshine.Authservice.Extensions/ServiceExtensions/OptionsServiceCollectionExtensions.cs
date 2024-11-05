using Microsoft.AspNetCore.Authentication.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Options服务拓展类
    /// </summary>
    public static class OptionsServiceCollectionExtensions
    {
        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddAdminOptions(this IServiceCollection services)
        {
            services.AddConfigureOptions<OAuthOptions>();

            return services;
        }
    }
}
