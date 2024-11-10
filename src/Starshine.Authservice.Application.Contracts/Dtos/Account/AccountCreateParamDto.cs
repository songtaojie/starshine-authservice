using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Starshine.Authservice.Application.Contracts.Dtos.Account
{
    /// <summary>
    /// 账号添加或者修改的model
    /// </summary>
    public class AccountCreateParamDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
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

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 0，保密，1：男，2：女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; } = DateTime.Now;

    }

    /// <summary>
    /// 添加角色model
    /// </summary>
    public class AccountAddRoleModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色name的集合
        /// </summary>
        public List<string> RoleNames { get; set; }

        /// <summary>
        /// 角色id的集合
        /// </summary>
        public List<string> RoleIds { get; set; }
    }
}
