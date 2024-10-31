﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starshine.Authservice.Entity
{
    public class AspNetUsers : IdentityUser
    {
        [MaxLength(36)]
        public override string Id { get => base.Id; set => base.Id = value; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(36)]
        public string RealName { get; set; }

        /// <summary>
        /// 0，保密，1：男，2：女
        /// </summary>
        [MaxLength(1)]
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; } = DateTime.Now;

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column(TypeName = "char(1)")]
        public string Deleted { get; set; } = "N";

        /// <summary>
        /// 创建时间，即注册时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; } = DateTime.Now;
    }
}
