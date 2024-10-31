using System;
using System.Collections.Generic;
using System.Text;

namespace Starshine.Authservice.Model.Models.ClientManager
{
    /// <summary>
    /// 客户端管理model
    /// </summary>
    public class ClientPageModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 描述说明
        /// </summary>
        public string Description { get; set; }

        public string AllowedGrantTypes { get; set; }

        /// <summary>
        /// 身份令牌的生存时间（以秒为单位）（默认为300秒/ 5分钟）
        /// </summary>
        public int IdentityTokenLifetime { get; set; } = 300;

        /// <summary>
        /// 访问令牌的生存时间（以秒为单位）（默认为3600秒/ 1小时）
        /// </summary>
        public int AccessTokenLifetime { get; set; }
    }

    /// <summary>
    /// 客户端作用域/跨域，odel
    /// </summary>
    public class ClientScPageModel
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 允许的作用域
        /// </summary>
        public string AllowedScopes { get; set; }

        /// <summary>
        /// 允许跨域的地址
        /// </summary>
        public string AllowedCorsOrigins { get; set; }
    }

    /// <summary>
    /// 客户端回调地址model
    /// </summary>
    public class ClientRuPageModel
    {
        /// <summary>
        /// 客户端id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string RedirectUris { get; set; }

        /// <summary>
        /// 退出回调的地址
        /// </summary>
        public string PostLogoutRedirectUris { get; set; }
    }
}
