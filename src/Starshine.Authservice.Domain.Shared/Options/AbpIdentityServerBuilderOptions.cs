using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain.Shared
{
    public class AbpIdentityServerBuilderOptions
    {
        /// <summary>
        /// 更新身份服务器声明兼容。
        /// Default: true.
        /// </summary>
        public bool UpdateJwtSecurityTokenHandlerDefaultInboundClaimTypeMap { get; set; } = true;

        /// <summary>
        /// 更新身份服务器声明兼容。
        /// Default: true.
        /// </summary>
        public bool UpdateAbpClaimTypes { get; set; } = true;

        /// <summary>
        ///集成到 AspNet Identity。
        /// Default: true.
        /// </summary>
        public bool IntegrateToAspNetIdentity { get; set; } = true;

        /// <summary>
        /// 设置为 false 以抑制对 IIdentityServerBuilder 的 AddDeveloperSigningCredential() 调用。
        /// </summary>
        public bool AddDeveloperSigningCredential { get; set; } = true;

        /// <summary>
        /// 添加默认 cookie 处理程序和相应的配置
        /// 默认值：true，设置为 false 以抑制对 IIdentityServerBuilder 的 AddCookieAuthentication() 调用。
        /// </summary>
        public bool AddIdentityServerCookieAuthentication { get; set; } = true;
    }
}
