using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Starshine.Authservice.Application.Contracts.Dtos.Account;
using Starshine.Authservice.Application.Contracts.Services;
using Starshine.Authservice.Entity;
using Starshine.Authservice.EntityFrameworkCore.DbContexts;
using Starshine.Common;
using Starshine.Extensions;
using System.Security.Claims;

namespace Starshine.Authservice.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly RoleManager<AspNetRoles> _roleManager;
        private readonly ApplicationDbContext _applicationDb;

        public AccountService(UserManager<AspNetUsers> userManager,
            RoleManager<AspNetRoles> roleManager, ApplicationDbContext applicationDb)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDb = applicationDb;
        }


        public async Task<bool> CreateOrUpdate(AccountCreateParamDto param)
        {
            if (string.IsNullOrEmpty(param.Id))
            {
                var user = new AspNetUsers
                {
                    Email = param.Email,
                    UserName = param.UserName,
                    RealName = param.RealName,
                    Sex = param.Sex,
                    Address = ""
                };
                if (param.Birthday.HasValue)
                {
                    user.Age = param.Birthday.Value.Year - DateTime.Now.Year;
                    user.Birthday = param.Birthday.Value;
                }
                IdentityResult result = await _userManager.CreateAsync(user, param.Password);
                if (!result.Succeeded)
                    throw new Exception(result.Errors.FirstOrDefault()?.Description);
                //var role = await _roleManager.FindByNameAsync(ConstKey.Client);
                //// 为账号分配角色
                //if (role != null)
                //{
                //    await _userManager.AddToRoleAsync(user, role.Name);
                //}
                await _userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.GivenName, param.RealName),
                    new Claim(JwtClaimTypes.Email, param.Email),
                    new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                });
                return true;
            }
            else// 编辑
            {
                var user = await _userManager.FindByIdAsync(param.Id);
                if (user == null)
                    throw new Exception("用户信息不存在");
                user.RealName = param.RealName;
                if (param.Birthday.HasValue)
                {
                    user.Birthday = param.Birthday.Value;
                    user.Sex = param.Sex;
                }
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded) throw new Exception(result.Errors.FirstOrDefault()?.Description);
                //重新添加GivenName claim
                var claims = await _userManager.GetClaimsAsync(user);
                var giveNameClaim = claims.FirstOrDefault(c => c.Type == JwtClaimTypes.GivenName);
                if (giveNameClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, giveNameClaim);
                    await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.GivenName, param.RealName));
                }
                return true;
            }
        }

        public async Task<PagedListResult<AccountPageResultDto>> GetPage(AccountPageParamDto param)
        {
            IQueryable<AspNetUsers> query = _userManager.Users
                .Where(u => !u.IsDeleted);
            var result = await query.OrderByDescending(u => u.CreateTime)
                .Select(u => new AccountPageResultDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    RealName = u.RealName,
                    CreateTime = u.CreateTime,
                    AccessFailedCount = u.AccessFailedCount
                })
                .ToOrderAndPageListAsync(param);
            var userIds = result.Items.Select(u => u.Id).ToArray();
            //获取查询结果的所有角色
            var roles = await (from r in _applicationDb.Roles.AsNoTracking()
                               join ur in _applicationDb.UserRoles.AsNoTracking() on r.Id equals ur.RoleId
                               where !r.Deleted && userIds.Contains(ur.UserId)
                               select new
                               {
                                   ur.UserId,
                                   ur.RoleId,
                                   r.Description
                               }).ToListAsync();
            result.Items.ForEach(u =>
            {
                u.RoleName = string.Join(",", roles.Where(r => r.UserId == u.Id).Select(r => r.Description));
            });
            return result;
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            return await _userManager.GetRolesAsync(new AspNetUsers()
            {
                Id = userId
            });
        }

        public async Task<bool> AssignRole(AssignRoleParamDto param)
        {
            if(param.RoleNames == null || param.RoleNames.Count == 0 || param.RoleIds == null
                || param.RoleIds.Count != param.RoleNames.Count) throw new Exception("请选择角色");
            var user = await _userManager.FindByIdAsync(param.UserId);
            if (user == null) throw new Exception("未找到用户信息");
            IDbContextTransaction? dbContextTransaction = null;
            try
            {
                dbContextTransaction = await _applicationDb.Database.BeginTransactionAsync();

                //添加角色
                var roleList = await _userManager.GetRolesAsync(user);
                if (roleList.Count > 0) await _userManager.RemoveFromRolesAsync(user, roleList);
                var identityResult = await _userManager.AddToRolesAsync(user, param.RoleNames);
                if (!identityResult.Succeeded)
                    throw new Exception(identityResult.Errors.FirstOrDefault()?.Description);
                //添加Claim
                var allClaims = await _userManager.GetClaimsAsync(user);
                var roleClaims = allClaims.Where(c => c.Type == ClaimTypes.Role).ToList();
                if (roleClaims.Count > 0) await _userManager.RemoveClaimsAsync(user, roleClaims);
                var newRoleClaims = new List<Claim>();
                param.RoleIds.ForEach(roleId =>
                {
                    newRoleClaims.Add(new Claim(ClaimTypes.Role, roleId));
                });
                identityResult = await _userManager.AddClaimsAsync(user, newRoleClaims);
                if (!identityResult.Succeeded) 
                    throw new Exception(identityResult.Errors.FirstOrDefault()?.Description);
                await dbContextTransaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                if (dbContextTransaction != null)
                    await dbContextTransaction.RollbackAsync();
                throw;
            }
            finally
            {
                if(dbContextTransaction != null)
                    await dbContextTransaction.DisposeAsync();
            }
        }

        public async Task<AccountDetailResultDto> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new Exception("未找到用户信息");
            return new AccountDetailResultDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RealName = user.RealName,
                Sex = user.Sex,
                Birthday = user.Birthday
            };
        }

        public async Task<bool> DeleteById(string id)
        {
            var userItem = await _userManager.FindByIdAsync(id);
            if (userItem == null)
                throw new Exception("找不到当前用户");
            userItem.IsDeleted = true;
            IdentityResult identityResult = await _userManager.UpdateAsync(userItem);
            if (!identityResult.Succeeded && identityResult.Errors.Count() > 0)
            {
                var error = identityResult.Errors.First();
                throw new Exception(error.Description);
                //ajaxResult.Code = error.Code;
                //ajaxResult.Message = error.Description;
            }
            return true;
        }

        public async Task<bool> VerifyUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new Exception("用户名不能为空");
            var user = await _userManager.FindByNameAsync(userName.Trim());
            if (user != null)
            {
                throw new Exception(string.Format("用户名{0}已存在!", userName));
            }
            return true;
        }

        public async Task<bool> VerifyEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("邮箱不能为空");
           
            var user = await _userManager.FindByNameAsync(email.Trim());
            if (user != null)
                throw new Exception(string.Format("邮箱{0}已存在!", email));
            return true;
        }
    }
}
