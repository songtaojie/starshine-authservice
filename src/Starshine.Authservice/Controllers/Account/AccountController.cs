// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Starshine.Authservice.Application.Contracts.Services;
using Starshine.Authservice.Application.Contracts.Dtos.Account;
using Starshine.Authservice.Entity.Consts;

namespace Starshine.Authservice.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
        public async Task<IActionResult> GetPage(AccountPageParamDto param)
        {
            var result = await _accountService.GetPage(param);
            return Success(result);
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
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> CreateOrUpdate([FromBody] AccountCreateParamDto param)
        {
            var result = await _accountService.CreateOrUpdate(param);
            return Success(result);
        }

        /// <summary>
        /// 获取用户的角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpGet("account/getrole/{userId}")]
        public async Task<IActionResult> GetRole(string userId)
        {
            var result = await _accountService.GetRolesAsync(userId);
            return Success(result);
        }

        /// <summary>
        ///添加角色
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> AssignRole(AssignRoleParamDto param)
        {
            var result = await _accountService.AssignRole(param);
            return Success(result);
        }


        /// <summary>
        /// 获取用户名明细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("account/get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _accountService.GetById(id);
            return Success(result);
        }


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id">要删除的用户的id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("account/delete/{id}")]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _accountService.DeleteById(id);
            return Success(result);
        }
        #endregion

        #region 验证输入信息
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public async Task<IActionResult> VerifyUserName(string userName)
        {
            var result = await _accountService.VerifyUserName(userName);
            return Success(result);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var result = await _accountService.VerifyEmail(email);
            return Success(result);
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