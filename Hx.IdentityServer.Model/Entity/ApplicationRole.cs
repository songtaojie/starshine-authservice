using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Entity
{
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// 角色COde
        /// </summary>
        [MaxLength(36)]
        public string Code { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1000)]
        public string Remark { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 创建ID
        /// </summary>
        [MaxLength(36)]
        public string CreateId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [MaxLength(36)]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改ID
        /// </summary>
        [MaxLength(36)]
        public string ModifyId { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        [MaxLength(36)]
        public string ModifyBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; } = DateTime.Now;

    }
}
