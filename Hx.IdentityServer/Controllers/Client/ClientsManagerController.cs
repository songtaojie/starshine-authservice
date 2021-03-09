using Hx.IdentityServer.Model.Models.ClientManager;
using Hx.Sdk.Entity.Extensions;
using Hx.Sdk.Entity.Page;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers.Client
{
    public class ClientsManagerController:BaseController
    {
        private readonly ConfigurationDbContext _configurationDbContext;

        public ClientsManagerController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext;
        }

        /// <summary>
        /// 数据列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            ViewData["IsSuperAdmin"] = IsSuperAdmin;
            return View();
        }
        
        /// <summary>
        /// 数据列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> QueryPage([FromBody] ClientManagerPageParam param)
        {
            //没有过滤的记录数
            var query =  _configurationDbContext.Clients
              .Include(d => d.AllowedGrantTypes)
              .Include(d => d.AllowedScopes)
              .Include(d => d.AllowedCorsOrigins)
              .Include(d => d.RedirectUris)
              .Include(d => d.PostLogoutRedirectUris);
            var clientResult = await query.OrderByDescending(c=>c.Created)
                .Select(c=> c)
                .ToOrderAndPageListAsync(param);
            var clientModels = clientResult.Items.Select(c => c.ToModel()).ToList();
            var resultList = clientModels.Select(c => new ClientManagerPageModel
            {
                ClientId = c.ClientId,
                ClientName = c.ClientName,
                AllowedGrantTypes = string.Join("；", c.AllowedGrantTypes),
                AllowedScopes = string.Join("；", c.AllowedScopes),
                AllowedCorsOrigins = string.Join("<br/>", c.AllowedCorsOrigins),
                RedirectUris = string.Join("<br/>", c.RedirectUris),
                PostLogoutRedirectUris = string.Join("<br/>", c.PostLogoutRedirectUris)
            }).ToList();
            var result = new PageModel<ClientManagerPageModel>(resultList, clientResult.TotalCount, clientResult.PageIndex, clientResult.PageSize);
            return Json(result);
        }
        /// <summary>
        /// 数据修改页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "SuperAdmin")]
        public IActionResult AddOrUpdate(int clientId)
        {
            ViewBag.ClientId = clientId;
            return View();
        }


    }
}
