// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace IdentityServerHost.Quickstart.UI
{
    /// <summary>
    /// 账户登录/注销配置
    /// </summary>
    public class AccountOptions
    {
        /// <summary>
        /// 允许本地登录
        /// </summary>
        public static bool AllowLocalLogin = true;
        /// <summary>
        /// 允许记住登录
        /// </summary>
        public static bool AllowRememberLogin = true;
        /// <summary>
        /// 记住我登录过期时间
        /// </summary>
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        /// <summary>
        /// 显示注销提示
        /// </summary>
        public static bool ShowLogoutPrompt = false;

        /// <summary>
        /// 自动重新登录后退出
        /// </summary>
        public static bool AutomaticRedirectAfterSignOut = false;

        /// <summary>
        /// 无效的凭证错误消息
        /// </summary>
        public static string InvalidCredentialsErrorMessage = "用户名或密码无效";
    }
}
