using System;
using System.Collections.Generic;
using System.Text;

namespace Starshine.Authservice.Model.Models.ApiResource
{
    public class ApiResourcePageModel
    {
        /// <summary>
        /// Api资源主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Api资源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 声明
        /// </summary>
        public string UserClaims { get; set; }
    }
}
