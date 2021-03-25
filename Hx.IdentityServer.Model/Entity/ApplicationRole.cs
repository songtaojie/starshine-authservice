using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Entity
{
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string IsDeleted { get; set; } = "N";
        /// <summary>
        /// 备注说明
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string IsDisabled { get; set; } = "N";
        /// <summary>
        /// 创建ID
        /// </summary>
        [MaxLength(36)]
        public string CreaterId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [MaxLength(36)]
        public string Creater { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改ID
        /// </summary>
        [MaxLength(36)]
        public string LastModifierId { get; set; }
        /// <summary>
        /// 修改者
        /// </summary>
        [MaxLength(36)]
        public string LastModifier { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; } = DateTime.Now;

    }
}
