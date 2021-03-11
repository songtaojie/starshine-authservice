using System;
using System.ComponentModel.DataAnnotations;

namespace Hx.IdentityServer.Model.Account
{
    /// <summary>
    /// 账号添加或者修改的model
    /// </summary>
    public class AccountCreateModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }
       
        public string RealName { get; set; }
       
        public string UserName { get; set; }
       
        public string Email { get; set; }

        /// <summary>
        /// ^.*    指任意字符
        /// (?=.*[0-9]) 任意字符串后有一数字
        ///(?=.*[A-Z]) 任意字符串后有一大写字母
        ///(?=.*[a-z])  任意字符串后有一小写字母
        /// /(?=.*[!@#$%^&*?]) 任意字符串后有一括号里的特殊字符
        /// \w{ 6,}指长度要大于6位
        /// </summary>
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int Sex { get; set; } = 0;

        public DateTime? Birthday { get; set; } = DateTime.Now;
     
    }
}
