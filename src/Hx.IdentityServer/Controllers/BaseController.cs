using Hx.IdentityServer.Common;
using Hx.IdentityServer.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers
{

    [SecurityHeaders]
    public class BaseController: Controller
    {
        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return User.Identity?.IsAuthenticated == true;
            }
        }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSuperAdmin
        {
            get
            {
                if (User.Identity?.IsAuthenticated == true)
                {
                    return User.Claims.Any(c => c.Type == IdentityModel.JwtClaimTypes.Role && c.Value == ConstKey.SuperAdmin);
                }
                return false;
            }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId
        {
            get
            {
                if (IsAuthenticated)
                {
                    var claim = User.Claims.FirstOrDefault(c => c.Type == IdentityModel.JwtClaimTypes.Subject);
                    return claim?.Value;
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return User.Identity?.Name;
            }
        }

        protected IActionResult Success(object data)
        {
            return Json(new AjaxResult()
            {
                Data = data
            });
        }

        protected IActionResult Error(string message)
        {
            return Json(new AjaxResult
            {
                Success = false,
                Message = message
            });
        }

        protected IActionResult Error(string message,string code)
        {
            return Json(new AjaxResult
            {
                Code = code,
                Success = false,
                Message = message
            });
        }
    }
}
