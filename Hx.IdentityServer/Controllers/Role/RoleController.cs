using Hx.IdentityServer.Common;
using Hx.IdentityServer.Entity;
using Hx.IdentityServer.Model.Role;
using Hx.Sdk.Entity.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers.Role
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("role/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RoleController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// 数据列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("~/role")]
        public IActionResult Index()
        {
            ViewData["IsSuperAdmin"] = IsSuperAdmin;
            return View();
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetPage([FromBody] RolePageParam param)
        {
            //没有过滤的记录数
            var query = await _roleManager.Roles.Where(r => r.Deleted == ConstKey.No)
                .OrderByDescending(r => r.CreateTime)
                .OrderBy(r => r.OrderSort)
                .Select(r => new RolePageModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Creater = r.Creater,
                    CreateTime = r.CreateTime,
                    Enabled = r.Enabled 
                }).ToOrderAndPageListAsync(param);
            return Success(query);
        }
        /// <summary>
        /// 获取用户名明细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("{roleId}")]
        public async Task<IActionResult> Get(string roleId)
        {
            var detail = await _roleManager.Roles.Where(r => r.Id == roleId)
                .Select(r => new RoleDetailModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Enabled = r.Enabled,
                    OrderSort = r.OrderSort
                }).FirstOrDefaultAsync();
            if (detail == null) return Error("未找到当前角色信息");
            return Success(detail);
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> Create(RoleCreateModel request)
        {
            if (request == null) return Error("请求参数不正确");
            var role = new ApplicationRole
            {
                Name = request.Name,
                Description = request.Description,
                Enabled = request.Enabled,
                OrderSort = request.OrderSort
            };
            role.SetCreater(UserId, UserName);


            var result = await _roleManager.CreateAsync(role);
            if(!result.Succeeded) return Error(result.Errors.FirstOrDefault().Description);
            return Success("添加成功");
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> Update(RoleCreateModel request)
        {
            if (request == null) return Error("请求参数不正确");
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null) return Error("未找到角色信息");
            role.Name = request.Name;
            role.Description = request.Description;
            role.Enabled = request.Enabled;
            role.OrderSort = request.OrderSort;
            role.SetModifier(UserId, UserName);
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded) return Error(result.Errors.FirstOrDefault().Description);
            return Success("更新成功");
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = new ApplicationRole()
                {
                    Id = id
                };
                var result = await _roleManager.UpdateAsync(role);
                if (!result.Succeeded) return Error(result.Errors.FirstOrDefault().Description);
                return Success("删除成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

    }
}
