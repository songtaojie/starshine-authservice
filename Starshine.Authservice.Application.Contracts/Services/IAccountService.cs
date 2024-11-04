using Microsoft.AspNetCore.Mvc;
using Starshine.Authservice.Application.Contracts.Dtos.Account;
using Starshine.Common;

namespace Starshine.Authservice.Application.Contracts.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PagedListResult<AccountPageResultDto>> GetPage(AccountPageParamDto param);

        /// <summary>
        /// 创建或更新用户
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> CreateOrUpdate(AccountCreateParamDto param);

        /// <summary>
        /// 根据用户id获取角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<string>> GetRolesAsync(string userId);

        /// <summary>
        /// 分配角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> AssignRole(AssignRoleParamDto param);

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AccountDetailResultDto> GetById(string id);

        /// <summary>
        /// 根据主键删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteById(string id);

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> VerifyUserName(string userName);

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> VerifyEmail(string email);
    }
}
