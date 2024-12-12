//// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
//// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Starshine.Authservice.Web.Application.Contracts.Services;
//using Starshine.Authservice.Web.Application.Contracts.Dtos.Account;
//using Starshine.Abp.Application.Dtos;
//using System.Collections.Generic;

//namespace Starshine.Authservice.Web.Controllers
//{
//    /// <summary>
//    /// 账户控制器
//    /// </summary>
//    public class AccountController : BaseController
//    {
//        private readonly IAccountService _accountService;
//        public AccountController(IAccountService accountService)
//        {
//            _accountService = accountService;
//        }

//        #region 用户相关的操作
//        //[HttpGet]
//        //[Authorize]
//        //public ActionResult Index(string returnUrl = null)
//        //{
//        //    ViewData["IsSuperAdmin"] = this.IsSuperAdmin;
//        //    ViewData["ReturnUrl"] = returnUrl;
//        //    return View();
//        //}

//        [HttpGet]
//        //[Authorize]
//        public async Task<PagedListResult<AccountPageResultDto>> GetPage([FromQuery] AccountPageParamDto param)
//        {
//            var result = await _accountService.GetPage(param);
//            return result;
//        }

//        ///// <summary>
//        ///// 注册页面
//        ///// </summary>
//        ///// <param name="returnUrl"></param>
//        ///// <returns></returns>
//        //[HttpGet]
//        //public IActionResult Register(string returnUrl = null)
//        //{
//        //    ViewData["ReturnUrl"] = returnUrl;
//        //    return View();
//        //}

//        /// <summary>
//        /// 注册功能
//        /// </summary>
//        /// <param name="model">请求的model</param>
//        /// <param name="returnUrl"></param>
//        /// <returns></returns>
//        [HttpPost]
//        //[Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<bool> CreateOrUpdate([FromBody] AccountCreateParamDto param)
//        {
//            var result = await _accountService.CreateOrUpdate(param);
//            return result;
//        }

//        /// <summary>
//        /// 获取用户的角色
//        /// </summary>
//        /// <param name="userId">用户id</param>
//        /// <returns></returns>
//        [HttpGet("{userId}")]
//        public async Task<IList<string>> GetRole(string userId)
//        {
//            var result = await _accountService.GetRolesAsync(userId);
//            return result;
//        }

//        /// <summary>
//        ///添加角色
//        /// </summary>
//        /// <param name="id">用户id</param>
//        /// <returns></returns>
//        [HttpPost]
//        //[Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<bool> AssignRole(AssignRoleParamDto param)
//        {
//            var result = await _accountService.AssignRole(param);
//            return result;
//        }


//        /// <summary>
//        /// 获取用户名明细信息
//        /// </summary>
//        /// <param name="id">用户id</param>
//        /// <returns></returns>
//        [HttpGet("{id}")]
//        public async Task<AccountDetailResultDto> Get(string id)
//        {
//            var result = await _accountService.GetById(id);
//            return result;
//        }


//        /// <summary>
//        /// 删除操作
//        /// </summary>
//        /// <param name="id">要删除的用户的id</param>
//        /// <returns></returns>
//        [HttpDelete]
//        //[Route("account/delete/{id}")]
//        //[Authorize(Policy = ConstKey.SuperAdmin)]
//        public async Task<bool> Delete(string id)
//        {
//            var result = await _accountService.DeleteById(id);
//            return result;
//        }
//        #endregion

//        #region 验证输入信息
//        /// <summary>
//        /// 检查用户名是否存在
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<bool> VerifyUserName(string userName)
//        {
//            var result = await _accountService.VerifyUserName(userName);
//            return result;
//        }

//        /// <summary>
//        /// 检查用户名是否存在
//        /// </summary>
//        /// <param name="userName"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public async Task<bool> VerifyEmail(string email)
//        {
//            var result = await _accountService.VerifyEmail(email);
//            return result;
//        }
//        #endregion 
//    }
//}