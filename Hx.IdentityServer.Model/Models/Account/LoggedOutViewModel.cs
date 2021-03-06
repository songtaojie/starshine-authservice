// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


namespace Hx.IdentityServer.Model.Account
{
    public class LoggedOutViewModel
    {
        /// <summary>
        /// 发布注销重定向Uri
        /// </summary>
        public string PostLogoutRedirectUri { get; set; }
        public string ClientName { get; set; }

        /// <summary>
        /// 登出iframe网址
        /// </summary>
        public string SignOutIframeUrl { get; set; }

        /// <summary>
        /// 退出后自动重定向
        /// </summary>
        public bool AutomaticRedirectAfterSignOut { get; set; }

        /// <summary>
        /// 注销ID
        /// </summary>
        public string LogoutId { get; set; }

        /// <summary>
        /// 触发外部注销
        /// </summary>
        public bool TriggerExternalSignout => ExternalAuthenticationScheme != null;

        /// <summary>
        /// 外部认证方案
        /// </summary>
        public string ExternalAuthenticationScheme { get; set; }
    }
}