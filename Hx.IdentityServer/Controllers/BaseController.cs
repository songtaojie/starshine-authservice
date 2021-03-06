using Hx.IdentityServer.Common;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers
{
    //[SecurityHeaders]
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
                    return User.Claims.Any(c => c.Type == MyJwtClaimTypes.RoleName && c.Value == ConstKey.SuperAdmin);
                }
                return false;
            }
        }
    }
}
