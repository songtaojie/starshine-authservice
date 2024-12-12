//using Microsoft.AspNetCore.Mvc;
//using Starshine.Authservice.Application.Contracts.Dtos.ApiResource;
//using Starshine.Abp.Application.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Starshine.Authservice.Application.Contracts.Services
//{
//    public interface IApiResourcesService
//    {
//        /// <summary>
//        /// 添加或编辑api资源
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        Task<bool> AddOrUpdate(ApiResourceCreateParamDto param);

//        /// <summary>
//        /// 根据id删除主键
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        Task<bool> DeleteById(string id);

//        /// <summary>
//        /// 根据主键获取api资源
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        Task<ApiResourcePageResultDto> GetById(string id);

//        /// <summary>
//        /// 获取分页数据
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        Task<PagedListResult<ApiResourcePageResultDto>> GetPage(ApiResourcePageParamDto param);
//    }
//}
