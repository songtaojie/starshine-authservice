using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Models.ClientManager
{
    /// <summary>
    /// 客户端管理model
    /// </summary>
    public class ClientManagerPageModel
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

        public string AllowedGrantTypes { get; set; }

        public string AllowedScopes { get; set; }

        public string AllowedCorsOrigins { get; set; }

        public string RedirectUris { get; set; }

        public string PostLogoutRedirectUris { get; set; }
    }
}
