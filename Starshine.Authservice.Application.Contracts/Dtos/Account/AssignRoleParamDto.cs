using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Application.Contracts.Dtos.Account
{
    /// <summary>
    /// 为用户分配角色
    /// </summary>
    public class AssignRoleParamDto
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
