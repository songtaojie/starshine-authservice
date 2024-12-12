//using IdentityServer4.EntityFramework.DbContexts;
//using IdentityServer4.EntityFramework.Mappers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Starshine.Authservice.Application.Contracts.Dtos.ApiResource;
//using Starshine.Authservice.Application.Contracts.Services;
//using Starshine.Abp.Application.Dtos;
//using Starshine.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Starshine.Authservice.Application.Services
//{
//    public class ApiResourcesService : IApiResourcesService
//    {
//        private readonly ConfigurationDbContext _configurationDb;
//        public ApiResourcesService(ConfigurationDbContext configurationDbContext) 
//        {
//            _configurationDb = configurationDbContext;
//        }

//        public async Task<bool> AddOrUpdate(ApiResourceCreateParamDto param)
//        {
//            if (string.IsNullOrEmpty(param.Id))
//            {
//                return await AddApiResource(param);
//            }
//            else
//            {
//                return await UpdateResource(param);
//            }
//        }

//        public async Task<bool> AddApiResource(ApiResourceCreateParamDto param)
//        {
//            IdentityServer4.Models.ApiResource apiResource = new IdentityServer4.Models.ApiResource()
//            {
//                Name = param.Name,
//                DisplayName = param.DisplayName,
//                Description = param.Description,
//                Enabled = param.Enabled,
//                UserClaims = param.UserClaims?.Split(","),
//            };

//            var result = await _configurationDb.ApiResources.AddAsync(apiResource.ToEntity());
//            await _configurationDb.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> UpdateResource(ApiResourceCreateParamDto param)
//        {
//            int intId = Convert.ToInt32(param.Id);
//            var resource = await _configurationDb.ApiResources
//                   .Include(r => r.UserClaims)
//                   .FirstOrDefaultAsync(r => r.Id == intId);
//            if (resource == null) throw new Exception("资源信息不存在");
//            //resource.Name = request.Name;
//            resource.DisplayName = param.DisplayName;
//            resource.Description = param.Description;
//            resource.Enabled = param.Enabled;
//            if (!string.IsNullOrEmpty(param.UserClaims))
//            {
//                var apiResourceClaim = new List<IdentityServer4.EntityFramework.Entities.ApiResourceClaim>();
//                param.UserClaims.Split(",").Where(u => !string.IsNullOrEmpty(u)).ToList().ForEach(u =>
//                {
//                    apiResourceClaim.Add(new IdentityServer4.EntityFramework.Entities.ApiResourceClaim()
//                    {
//                        ApiResource = resource,
//                        ApiResourceId = resource.Id,
//                        Type = u
//                    });
//                });
//                resource.UserClaims = apiResourceClaim;
//            }

//            var result = _configurationDb.ApiResources.Update(resource);
//            await _configurationDb.SaveChangesAsync();
//            return true;
//        }

//        public async Task<bool> DeleteById(string id)
//        {
//            int indId = Convert.ToInt32(id);
//            var resource = await _configurationDb.ApiResources.FindAsync(indId);
//            if (resource == null) throw new Exception("未查询到数据");
//            _configurationDb.ApiResources.Remove(resource);
//            await _configurationDb.SaveChangesAsync();
//            return true;
//        }

//        public async Task<ApiResourcePageResultDto> GetById(string id)
//        {
//            int intId = Convert.ToInt32(id);
//            var resource = await _configurationDb.ApiResources
//            .Include(r => r.UserClaims)
//            .FirstOrDefaultAsync(r => r.Id == intId);
//            if (resource == null) throw new Exception("未找到当前资源信息");
//            var resourceModel = resource.ToModel();
//            return new ApiResourcePageResultDto
//            {
//                Id = resource.Id.ToString(),
//                Name = resourceModel.Name,
//                Enabled = resourceModel.Enabled,
//                DisplayName = resourceModel.DisplayName,
//                Description = resourceModel.Description,
//                UserClaims = string.Join(",", resourceModel.UserClaims),
//            };
//        }

//        public async Task<PagedListResult<ApiResourcePageResultDto>> GetPage(ApiResourcePageParamDto param)
//        {
//            var query = _configurationDb.ApiResources
//                .Include(r => r.UserClaims);
//            var result = await query.OrderByDescending(r => r.Created)
//                .Select(r => r)
//                .ToOrderAndPageListAsync(param);
//            var resultList = new List<ApiResourcePageResultDto>();
//            result.Items.ForEach(r =>
//            {
//                var model = r.ToModel();
//                resultList.Add(new ApiResourcePageResultDto
//                {
//                    Id = r.Id.ToString(),
//                    Name = model.Name,
//                    Enabled = model.Enabled,
//                    DisplayName = model.DisplayName,
//                    Description = model.Description,
//                    UserClaims = GetStrong(model.UserClaims)
//                });
//            });
//            return new PagedListResult<ApiResourcePageResultDto>(resultList, result.Total, result.Page, result.PageSize);
//        }

//        #region 私有函数


//        private string GetStrong(IEnumerable<string> list)
//        {
//            var strongList = list.Select((s, i) =>
//            {
//                if (i % 2 == 0) return s;
//                return string.Format("<strong>{0}</strong>", s);
//            });
//            return string.Join(", ", strongList);
//        }
//        #endregion
//    }
//}
