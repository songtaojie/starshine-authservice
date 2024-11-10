using Microsoft.AspNetCore.Mvc;
using Starshine.Authservice.Entity.Consts;
using Starshine.Common;
using System.Linq;

namespace Starshine.Authservice.Controllers
{

    [SecurityHeaders]
    public class BaseController : ControllerBase
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
    }
}
