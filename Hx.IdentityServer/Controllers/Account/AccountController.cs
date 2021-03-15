// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Hx.IdentityServer.Common;
using Hx.IdentityServer.Controllers;
using Hx.IdentityServer.Entity;
using Hx.IdentityServer.Model;
using Hx.IdentityServer.Model.Account;
using Hx.IdentityServer.Model.Models.Account;
using Hx.Sdk.Entity.Extensions;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region 用户相关的操作
        [HttpGet]
        [Authorize]
        public ActionResult Index(string returnUrl = null)
        {
            ViewData["IsSuperAdmin"] = this.IsSuperAdmin;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> QueryUserPage([FromBody]UserPageParam param)
        {
            //没有过滤的记录数
            AjaxResult ajaxResult = new AjaxResult();
            IQueryable<ApplicationUser> query = _userManager.Users.Where(u=>!u.IsDelete);
            //if (!string.IsNullOrEmpty(search))
            //{
            //    query = query.Where(u => u.RealName.Contains(search) || u.UserName.Contains(search));
            //}
            var data = await query.OrderByDescending(u => u.CreateTime)
                .Select(u => new UserPageModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    RealName = u.RealName,
                    CreateTime = u.CreateTime,
                    AccessFailedCount = u.AccessFailedCount
                })
                .ToOrderAndPageListAsync(param);
            ajaxResult.Data = data;
            return Json(ajaxResult);
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// 注册功能
        /// </summary>
        /// <param name="model">请求的model</param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> CreateOrUpdate([FromBody]AccountCreateModel model)
        {
            AjaxResult ajaxResult = new AjaxResult();
            if (model == null) return Error("请求参数不正确");
            if (string.IsNullOrEmpty(model.Id))
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    RealName = model.RealName,
                    Sex = model.Sex,
                    Age = model.Birthday.Value.Year - DateTime.Now.Year,
                    Birthday = model.Birthday.Value,
                    Address = ""
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded) return Error(result.Errors.FirstOrDefault()?.Description);
                var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Code == ConstKey.System);
                await _userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, model.RealName),
                            new Claim(JwtClaimTypes.Email, model.Email),
                            new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Role, role?.Id),
                            new Claim(MyJwtClaimTypes.RoleName, ConstKey.System),
                        });
                return Success("添加成功");
            }
            else// 编辑
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                user.RealName = model.RealName;
                user.Sex = model.Sex;
                if (model.Birthday.HasValue) user.Birthday = model.Birthday.Value;
                var result = await _userManager.UpdateAsync(user);
                if(!result.Succeeded) return Error(result.Errors.FirstOrDefault()?.Description);
                return Success("修改成功");
            }
        }

        /// <summary>
        /// 获取用户名明细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("account/getdetail/{id}")]
        public async Task<IActionResult> GetDetail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return Error("未找到用户信息");
            AccountDetailModel detailModel = new AccountDetailModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RealName = user.RealName,
                Sex = user.Sex,
                Birthday = user.Birthday
            };
            return Success(detailModel);
        }


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">要删除的用户的id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/delete/{id}")]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<JsonResult> Delete(string id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            var userItem = await _userManager.FindByIdAsync(id);
            if (userItem != null)
            {
                userItem.IsDelete = true;
                IdentityResult  identityResult = await _userManager.UpdateAsync(userItem);
                if (!identityResult.Succeeded && identityResult.Errors.Count() > 0)
                {
                    var error = identityResult.Errors.FirstOrDefault();
                    ajaxResult.Code = error.Code;
                    ajaxResult.Message = error.Description;
                }
            }
            else
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "找不到当前用户";
            }
            return Json(ajaxResult);
        }
        #endregion

        #region 验证输入信息
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost,HttpGet]
        public async Task<IActionResult> CheckUserName(string userName)
        {
            AjaxResult ajaxResult = new AjaxResult();
            if (string.IsNullOrEmpty(userName))
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "用户名不能为空";
                return Json(ajaxResult);
            }
            var user = await _userManager.FindByNameAsync(userName.Trim());
            if (user != null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = string.Format("用户名{0}已存在!", userName);
                return Json(ajaxResult);
            }
            ajaxResult.Data = true;
            return Json(ajaxResult);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public async Task<IActionResult> CheckEmail(string email)
        {
            AjaxResult ajaxResult = new AjaxResult();
            if (string.IsNullOrEmpty(email))
            {
                ajaxResult.Success = false;
                ajaxResult.Message = "邮箱不能为空";
                return Json(ajaxResult);
            }
            var user = await _userManager.FindByNameAsync(email.Trim());
            if (user != null)
            {
                ajaxResult.Success = false;
                ajaxResult.Message = string.Format("邮箱{0}已存在!", email);
                return Json(ajaxResult);
            }
            ajaxResult.Data = true;
            return Json(ajaxResult);
        }
        #endregion 

        #region 私有函数
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Index), "Account");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}