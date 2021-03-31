using Hx.IdentityServer.Common;
using Hx.IdentityServer.Model.Models.ApiResource;
using Hx.Sdk.Entity.Extensions;
using Hx.Sdk.Entity.Page;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers.ApiResources
{
    [Route("apiresource/[action]")]
    [ApiController]
    public class ApiResourcesController:BaseController
    {
        private readonly ConfigurationDbContext _configurationDb;

        public ApiResourcesController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDb = configurationDbContext;
        }

        /// <summary>
        /// 数据列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("~/apiresource")]
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
        public async Task<IActionResult> GetPage(ApiResourcePageParam param)
        {
            //没有过滤的记录数
            var query = _configurationDb.ApiResources
                .Include(r => r.UserClaims);
            var resource = await query.OrderByDescending(r => r.Created)
                .Select(r => r)
                .ToOrderAndPageListAsync(param);
            var resultList = new List<ApiResourcePageModel>();
            resource.Items.ForEach(r =>
            {
                var model = r.ToModel();
                resultList.Add(new ApiResourcePageModel
                {
                    Id = r.Id.ToString(),
                   Name = model.Name,
                   Enabled = model.Enabled,
                   DisplayName = model.DisplayName,
                   Description = model.Description,
                   UserClaims = string.Join(",",model.UserClaims)
                });
            });
            var result = new PageModel<ApiResourcePageModel>(resultList, resource.TotalCount, resource.PageIndex, resource.PageSize);
            return Success(result);
        }

        /// <summary>
        /// 获取用户名明细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            int intId = Convert.ToInt32(id);
            var resource = await _configurationDb.ApiResources
            .Include(r => r.UserClaims)
            .FirstOrDefaultAsync(r => r.Id == intId);
            if (resource == null) return Error("未找到当前资源信息");
            var resourceModel = resource.ToModel();
            ApiResourcePageModel detailModel = new ApiResourcePageModel
            {
                Id = resource.Id.ToString(),
                Name = resourceModel.Name,
                Enabled = resourceModel.Enabled,
                DisplayName = resourceModel.DisplayName,
                Description = resourceModel.Description,
                UserClaims = string.Join(",", resourceModel.UserClaims),
            };
            return Success(detailModel);
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> AddOrUpdate(ApiResourceCreateModel request)
        {
            if (request == null) return Error("请求参数不正确");
            if (string.IsNullOrEmpty(request.Id))
            {
                return await AddResource(request);
            }
            else
            {
                return await UpdateResource(request);
            }
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> Delete(string id)
        {
            int indId = Convert.ToInt32(id);
            var resource = await _configurationDb.ApiResources.FindAsync(indId);
            if (resource == null) return Error("未查询到数据");
            try
            {
                _configurationDb.ApiResources.Remove(resource);
                await _configurationDb.SaveChangesAsync();
                return Success("删除成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        #region 私有函数
        public async Task<IActionResult> AddResource(ApiResourceCreateModel request)
        {
            try
            {
                IdentityServer4.Models.ApiResource apiResource = new IdentityServer4.Models.ApiResource()
                {
                    Name = request.Name,
                    DisplayName = request.DisplayName,
                    Description = request.Description,
                    Enabled = request.Enabled,
                    UserClaims = request.UserClaims?.Split(","),
                };

                var result = await _configurationDb.ApiResources.AddAsync(apiResource.ToEntity());
                await _configurationDb.SaveChangesAsync();
                return Success("添加成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        public async Task<IActionResult> UpdateResource(ApiResourceCreateModel request)
        {
            try
            {
                int intId = Convert.ToInt32(request.Id);
                var resource = await _configurationDb.ApiResources
                       .Include(r => r.UserClaims)
                       .FirstOrDefaultAsync(r => r.Id == intId);
                if (resource == null) return Error("资源信息不存在");
                //resource.Name = request.Name;
                resource.DisplayName = request.DisplayName;
                resource.Description = request.Description;
                resource.Enabled = request.Enabled;
                if (!string.IsNullOrEmpty(request.UserClaims))
                {
                    var apiResourceClaim = new List<IdentityServer4.EntityFramework.Entities.ApiResourceClaim>();
                    request.UserClaims.Split(",").Where(u => !string.IsNullOrEmpty(u)).ToList().ForEach(u =>
                    {
                        apiResourceClaim.Add(new IdentityServer4.EntityFramework.Entities.ApiResourceClaim()
                        {
                            ApiResource = resource,
                            ApiResourceId = resource.Id,
                            Type = u
                        });
                    });
                    resource.UserClaims = apiResourceClaim;
                }

                var result = _configurationDb.ApiResources.Update(resource);
                await _configurationDb.SaveChangesAsync();
                return Success("修改成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            
        }
        #endregion
    }
}
