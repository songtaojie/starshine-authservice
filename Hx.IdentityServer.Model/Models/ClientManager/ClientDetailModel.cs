using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Models.ClientManager
{
    public class ClientDetailModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 客户端秘钥
        /// </summary>
        public string ClientSecrets { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 指定允许的授予类型
        /// </summary>
        public string AllowedGrantTypes { get; set; }

        /// <summary>
        /// 指定允许客户端请求的api作用域。 如果为空，则客户端无法访问任何范围
        /// </summary>
        public string AllowedScopes { get; set; }

        /// <summary>
        /// 获取或设置JavaScript客户端允许的CORS源。
        /// </summary>
        public string AllowedCorsOrigins { get; set; }

        /// <summary>
        /// 回调地址,指定允许的URI，以将令牌或授权码返回给
        /// </summary>
        public string RedirectUris { get; set; }

        /// <summary>
        /// 退出回调地址,指定允许的URI在注销后重定向到
        /// </summary>
        public string PostLogoutRedirectUris { get; set; }
    }
}
