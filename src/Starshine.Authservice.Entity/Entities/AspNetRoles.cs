﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Starshine.Authservice.Entity
{
    public class AspNetRoles : IdentityRole
    {
        [MaxLength(36)]
        public override string Id { get => base.Id; set => base.Id = value; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; }
        /// <summary>
        /// 描述说明
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }
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

        #region 操作
        /// <summary>
        /// 设置创建者信息
        /// </summary>
        /// <param name="createrId">创建者Id</param>
        /// <param name="creater">创建者名称</param>
        public AspNetRoles SetCreater(string createrId, string creater)
        {
            this.CreaterId = createrId;
            this.Creater = creater;
            this.CreateTime = DateTime.Now;
            SetModifier(createrId, creater);
            return this;
        }

        /// <summary>
        /// 设置创建者信息
        /// </summary>
        /// <param name="modifierId">更新者Id</param>
        /// <param name="modifier">更新者名称</param>
        public AspNetRoles SetModifier(string modifierId, string modifier)
        {
            this.LastModifierId = modifierId;
            this.LastModifier = modifier;
            this.LastModifyTime = DateTime.Now;
            return this;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deleterId">设置删除者id</param>
        /// <param name="deleter">删除者名称</param>
        public AspNetRoles SetDelete(string deleterId, string deleter)
        {
            this.Deleted = true;
            SetModifier(deleterId, deleter);
            return this;
        }
        #endregion

    }
}
