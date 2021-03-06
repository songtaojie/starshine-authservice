// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Hx.IdentityServer.Common;
using Hx.IdentityServer.Controllers;
using Hx.IdentityServer.Entity;
using Hx.IdentityServer.Model;
using Hx.IdentityServer.Model.Account;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        #region 登录
        /// <summary>
        /// 登录授权页面
        /// </summary>
        [HttpGet]
        [Route("oauth2/authorize")]
        public async Task<IActionResult> Login(string returnUrl,string test)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("oauth2/authorize", Name = "authorize")]
        public async Task<IActionResult> Login(LoginCreateModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            // 用户点击了"cancel"按钮
            if (button != "login")
            {
                if (context != null)
                {
                    // 如果用户取消，则将结果发送回IdentityServer，就像他们拒绝同意（即使此客户不需要同意）。 
                    // 这将向客户端发送拒绝访问的OIDC错误响应
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);
                    if (context.IsNativeClient())
                    {
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // 由于我们没有有效的上下文，因此我们只需返回首页
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                    if (context != null)
                    {
                        if (context.IsNativeClient())
                        {
                            // 客户端是本地客户端，因此此更改方式
                            //返回response是为了使最终用户获得更好的用户体验。
                            return this.LoadingPage("Redirect", model.ReturnUrl);
                        }
                        return Redirect(model.ReturnUrl);
                    }
                    // 请求本地页面
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // 用户可能单击了恶意链接-应该记录
                        throw new Exception("invalid return URL");
                    }
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }

        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginCreateModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }
        #endregion

        #region 注销
        /// <summary>
        /// 显示注销页面
        /// </summary>
        [HttpGet]
        [Route("oauth2/logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            //建立模型，以便注销页面知道要显示的内容
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// 处理注销页面回发
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("oauth2/logout", Name = "authlogout")]
        public async Task<IActionResult> Logout(LogoutreateModel model)
        {
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // 删除本地身份验证cookie
                await _signInManager.SignOutAsync();
                // 引发注销事件
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }
            // 检查我们是否需要在上游身份提供商处触发注销
            if (vm.TriggerExternalSignout)
            {
                // 建立返回URL，以便上游提供者在用户注销后将重定向回我们。 
                // 这样我们就可以完成单点退出处理。
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // 这会触发重定向到外部提供商以进行注销
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return RedirectToAction("login");
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // 自动注销很安全
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);
            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            //如果没有当前注销上下文，我们需要创建一个上下文，
                            //以便在我们注销并重定向到外部IdP进行注销之前从当前已登录的用户捕获必要的信息
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
        #endregion

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region 用户相关的操作
        [HttpGet]
        [Route("account/users")]
        [Authorize]
        public ActionResult Users(string returnUrl = null)
        {
            ViewData["IsSuperAdmin"] = this.IsSuperAdmin;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> QueryUserPage()
        {
            int draw = Convert.ToInt32(HttpContext.Request.Form["draw"]);
            int start = Convert.ToInt32(HttpContext.Request.Form["start"]);
            int length = Convert.ToInt32(HttpContext.Request.Form["length"]);
            string search = HttpContext.Request.Form["search[value]"];
            //没有过滤的记录数
            long recordsTotal = 0;
            IQueryable<ApplicationUser> query = _userManager.Users.Where(u=>!u.IsDelete);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.RealName.Contains(search) || u.UserName.Contains(search));
            }
            recordsTotal = query.LongCount();
            var data = await query.OrderByDescending(u => u.CreateTime)
                .Skip(start * length)
                .Take(length)
                .Select(u => new
                {
                    u.Id,
                    u.RealName,
                    u.UserName,
                    u.CreateTime,
                    u.AccessFailedCount
                }).ToListAsync();
          
            return Json(new
            {
                draw,
                recordsTotal,
                recordsFiltered = recordsTotal,
                data,
                IsSuperAdmin
            });
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
        [Route("account/register")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult> Register(RegisterCreateModel model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;
            IdentityResult result = new IdentityResult();
            if (ModelState.IsValid)
            {
                var userItem = _userManager.FindByNameAsync(model.UserName.Trim()).Result;
                if (userItem == null)
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
                    result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var role = await _roleManager.FindByNameAsync(ConstKey.System);
                        result = await _userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Name, model.RealName),
                            new Claim(JwtClaimTypes.Email, model.Email),
                            new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Role, role?.Id),
                            new Claim(MyJwtClaimTypes.RoleName, ConstKey.System),
                        });

                        if (result.Succeeded)
                        {
                            // 可以直接登录
                            return RedirectToLocal(model.ReturnUrl);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"用户{userItem?.UserName}已存在");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
            AjaxResult result = new AjaxResult();
            var userItem = await _userManager.FindByIdAsync(id);
            if (userItem != null)
            {
                userItem.IsDelete = true;
                IdentityResult  identityResult = await _userManager.UpdateAsync(userItem);
                if (!identityResult.Succeeded && identityResult.Errors.Count() > 0)
                {
                    var error = identityResult.Errors.FirstOrDefault();
                    result.Code = error.Code;
                    result.Message = error.Description;
                }
            }
            else
            {
                result.Message = "找不到当前用户";
            }
            return Json(result);
        }
        #endregion

        #region 验证输入信息
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost,HttpGet]
        public async Task<ContentResult> CheckUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return Content("用户名不能为空");
            var user = await _userManager.FindByNameAsync(userName.Trim());
            if(user != null) return Content(string.Format("用户名{0}已存在!", userName));
            return Content("true");
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost, HttpGet]
        public async Task<ContentResult> CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return Content("邮箱不能为空");
            var user =  await _userManager.FindByEmailAsync(email.Trim());
            if (user != null) return Content(string.Format("邮箱{0}已存在!", email));

            return Content("true");
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
                return RedirectToAction(nameof(HomeController.Index), "Home");
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