using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Starshine.Authservice.Application.Contracts.Dtos.Role;
using Starshine.Authservice.Application.Contracts.Services;
using Starshine.Authservice.Entity;
using Starshine.Common;
using Starshine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AspNetRoles> _roleManager;

        public RoleService(RoleManager<AspNetRoles> roleManager)
        {
            _roleManager = roleManager;
        }
        
        public async Task<List<RolePageResultDto>> GetList()
        {
            var list = await _roleManager.Roles.Where(r => !r.Deleted)
                .OrderByDescending(r => r.CreateTime)
                .OrderBy(r => r.OrderSort)
                .Select(r => new RolePageResultDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Creater = r.Creater,
                    CreateTime = r.CreateTime,
                    Enabled = r.Enabled
                }).ToListAsync();
            return list;
        }

        public async Task<PagedListResult<RolePageResultDto>> GetPage(RolePageParamDto param)
        {
            var result = await _roleManager.Roles.Where(r => !r.Deleted)
               .OrderByDescending(r => r.CreateTime)
               .OrderBy(r => r.OrderSort)
               .Select(r => new RolePageResultDto
               {
                   Id = r.Id,
                   Name = r.Name,
                   Description = r.Description,
                   Creater = r.Creater,
                   CreateTime = r.CreateTime,
                   Enabled = r.Enabled
               }).ToOrderAndPageListAsync(param);
            return result;
        }

        public async Task<RoleDetailResultDto> GeById(string roleId)
        {
            var detail = await _roleManager.Roles.Where(r => r.Id == roleId)
                .Select(r => new RoleDetailResultDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Enabled = r.Enabled,
                    OrderSort = r.OrderSort
                }).FirstOrDefaultAsync();
            if (detail == null) throw new Exception("未找到当前角色信息");
            return detail;
        }

        public async Task<bool> Create(RoleCreateParamDto param)
        {
            var role = new AspNetRoles
            {
                Name = param.Name,
                Description = param.Description,
                Enabled = param.Enabled,
                OrderSort = param.OrderSort
            };
            //role.SetCreater(UserId, UserName);
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            return true;
        }

        public async Task<bool> Update(RoleCreateParamDto param)
        {
            var role = await _roleManager.FindByIdAsync(param.Id);
            if (role == null) throw new Exception("未找到角色信息");
            role.Name = param.Name;
            role.Description = param.Description;
            role.Enabled = param.Enabled;
            role.OrderSort = param.OrderSort;
            //role.SetModifier(UserId, UserName);
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            return true;
        }

        public async Task<bool> DeleteById(string id)
        {
            var role = new AspNetRoles()
            {
                Id = id
            };
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            return true;
        }
    }
}
