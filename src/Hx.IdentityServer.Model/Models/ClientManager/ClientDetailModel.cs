using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Models.ClientManager
{
    /// <summary>
    /// 客户端管理详情数据
    /// </summary>
    public class ClientDetailModel
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
        public List<string> AllowedGrantTypes { get; set; }

        /// <summary>
        /// 身份令牌的生存时间（以秒为单位）（默认为300秒/ 5分钟）
        /// </summary>
        public int IdentityTokenLifetime { get; set; } = 300;

        /// <summary>
        /// 访问令牌的生存时间（以秒为单位）（默认为3600秒/ 1小时）
        /// </summary>
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// 是否需要同意页面
        /// </summary>
        public bool RequireConsent { get; set; }

        /// <summary>
        /// 是否存储同意决定
        /// </summary>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// 刷新令牌的最大生存期（以秒为单位）。 默认为2592000秒30天
        /// </summary>
        public int AbsoluteRefreshTokenLifetime { get; set; }

        /// <summary>
        /// 刷新令牌的滑动寿命（以秒为单位）。 默认为1296000秒15天
        /// </summary>
        public int SlidingRefreshTokenLifetime { get; set; }

        /// <summary>
        /// ReUse：刷新令牌句柄在刷新令牌时将保持不变
        /// OneTime：刷新令牌句柄在刷新令牌时将被更新
        /// </summary>
        public IdentityServer4.Models.TokenUsage RefreshTokenUsage { get; set; }

        /// <summary>
        /// Absolute：刷新令牌将在固定的时间点到期（由AbsoluteRefreshTokenLifetime指定）
        /// Sliding：刷新令牌时，刷新令牌的生存期将更新（按SlidingRefreshTokenLifetime中指定的数量）。 
        /// 生存期将不超过AbsoluteRefreshTokenLifetime。
        /// </summary>
        public IdentityServer4.Models.TokenExpiration RefreshTokenExpiration { get; set; }
    }


    /// <summary>
    /// 客户端作用域/跨域详情model
    /// </summary>
    public class ClientScDetailModel
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
        /// 指定允许客户端请求的api作用域。 如果为空，则客户端无法访问任何范围
        /// </summary>
        public string AllowedScopes { get; set; }

        /// <summary>
        /// 获取或设置JavaScript客户端允许的CORS源。
        /// </summary>
        public string AllowedCorsOrigins { get; set; }
    }


    /// <summary>
    /// 客户端回调/退出回调详情model
    /// </summary>
    public class ClientRuDetailModel
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
        /// 回调地址,指定允许的URI，以将令牌或授权码返回给
        /// </summary>
        public string RedirectUris { get; set; }

        /// <summary>
        /// 退出回调地址,指定允许的URI在注销后重定向到
        /// </summary>
        public string PostLogoutRedirectUris { get; set; }
    }
}
