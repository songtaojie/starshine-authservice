﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.IdentityServer.Model.Role
{
    /// <summary>
    /// 角色detailmodel
    /// </summary>
    public class RoleDetailModel
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
        ///排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string Enabled { get; set; }
    }
}
