using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Role
{
    /// <summary>
    /// 角色管理model
    /// </summary>
    public class RolePageModel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称，即角色的代码
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 是否启用，Y：启用，N：禁用
        /// </summary>
        public string Enabled { get; set; }
    }
}
