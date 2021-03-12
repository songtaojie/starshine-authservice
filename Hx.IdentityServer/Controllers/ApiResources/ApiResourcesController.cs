using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers.ApiResources
{
    public class ApiResourcesController:BaseController
    {
        private readonly ConfigurationDbContext _configurationDb;

        public ApiResourcesController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDb = configurationDbContext;
        }

        /// <summary>
        /// 数据列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _configurationDb.ApiResources
                .Include(d => d.UserClaims)
                .ToListAsync());
        }
    }
}
