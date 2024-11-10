using System;
using System.Collections.Generic;
using System.Text;

namespace Starshine.Authservice.Application.Contracts.Dtos.Account
{
    /// <summary>
    /// 用户查询model
    /// </summary>
    public class AccountPageResultDto
    {
        /// <summary>
        /// 用户的id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 访问失败的次数
        /// </summary>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 角色，多个以逗号隔开
        /// </summary>
        public string RoleName { get; set; }
    }
}
