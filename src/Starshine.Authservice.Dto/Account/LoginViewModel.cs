// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;

namespace Starshine.Authservice.Model.Account
{
    public class LoginViewModel : LoginCreateModel
    {
        /// <summary>
        /// 允许记住登录
        /// </summary>
        public bool AllowRememberLogin { get; set; } = true;
        /// <summary>
        /// 允许localhost登录
        /// </summary>
        public bool EnableLocalLogin { get; set; } = true;

        /// <summary>
        /// 外部供应商
        /// </summary>
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

        /// <summary>
        /// 可见的外部供应商
        /// </summary>
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        /// <summary>
        /// 是否仅外部登录
        /// </summary>
        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;

        /// <summary>
        /// 外部登录方案
        /// </summary>
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}