using System;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Starshine.Authservice.Controllers
{
    public static class Extensions
    {
        /// <summary>
        ///����ض���URI�Ƿ����ڱ����ͻ��ˡ�
        /// </summary>
        /// <returns></returns>
        public static bool IsNativeClient(this AuthorizationRequest context)
        {
            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
               && !context.RedirectUri.StartsWith("https", StringComparison.Ordinal);
        }

        //public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        //{
        //    controller.HttpContext.Response.StatusCode = 200;
        //    controller.HttpContext.Response.Headers["Location"] = "";

        //    return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
        //}
    }
}
