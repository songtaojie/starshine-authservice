// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace Starshine.Authservice.Model.Account
{
    /// <summary>
    /// 登录输入信息
    /// </summary>
    public class LoginCreateModel
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberLogin { get; set; }

        /// <summary>
        /// 登录后跳转url
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}