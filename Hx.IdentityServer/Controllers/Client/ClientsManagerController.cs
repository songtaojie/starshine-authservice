using Hx.IdentityServer.Common;
using Hx.IdentityServer.Model.ClientManager;
using Hx.IdentityServer.Model.Models.ClientManager;
using Hx.Sdk.Entity.Extensions;
using Hx.Sdk.Entity.Page;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hx.IdentityServer.Controllers.Client
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ClientsManagerController : BaseController
    {
        private readonly ConfigurationDbContext _configurationDb;

        public ClientsManagerController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDb = configurationDbContext;
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
            var query = _configurationDb.Clients
              .Include(d => d.AllowedGrantTypes)
              .Include(d => d.AllowedScopes)
              .Include(d => d.AllowedCorsOrigins)
              .Include(d => d.RedirectUris)
              .Include(d => d.PostLogoutRedirectUris);
            var clientResult = await query.OrderByDescending(c => c.Created)
                .Select(c => c)
                .ToOrderAndPageListAsync(param);
            var resultList = new List<ClientManagerPageModel>();
            clientResult.Items.ForEach(c =>
            {
                var model = c.ToModel();
                resultList.Add(new ClientManagerPageModel
                {
                    Id = c.Id.ToString(),
                    Enabled = model.Enabled,
                    ClientId = model.ClientId,
                    ClientName = model.ClientName,
                    AllowedGrantTypes = string.Join(", ", model.AllowedGrantTypes),
                    AllowedScopes = string.Join(", ", model.AllowedScopes),
                    AllowedCorsOrigins = string.Join("<br/>", model.AllowedCorsOrigins),
                    RedirectUris = string.Join("<br/>", model.RedirectUris),
                    PostLogoutRedirectUris = string.Join("<br/>", model.PostLogoutRedirectUris)
                });
            });
            var result = new PageModel<ClientManagerPageModel>(resultList, clientResult.TotalCount, clientResult.PageIndex, clientResult.PageSize);
            return Success(result);
        }

        /// <summary>
        /// 获取用户名明细信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            int intId = Convert.ToInt32(id);
            var client = await _configurationDb.Clients
            .Include(d => d.AllowedGrantTypes)
            .Include(d => d.AllowedScopes)
            .Include(d => d.AllowedCorsOrigins)
            .Include(d => d.RedirectUris)
            .Include(d => d.PostLogoutRedirectUris)
            .FirstOrDefaultAsync(c => c.Id == intId);
            if (client == null) return Error("未找到当前客户端信息");
            var clientModel = client.ToModel();
            ClientDetailModel detailModel = new ClientDetailModel
            {
                Id = client.Id.ToString(),
                Enabled = clientModel.Enabled,
                ClientId = clientModel.ClientId,
                ClientName = clientModel.ClientName,
                ClientSecrets = string.Join(",", clientModel.ClientSecrets.Select(s => s.Value)),
                Description = clientModel.Description,
                AllowedGrantTypes = string.Join(",", clientModel.AllowedGrantTypes),
                AllowedCorsOrigins = string.Join(",", clientModel.AllowedCorsOrigins),
                AllowedScopes = string.Join(",", clientModel.AllowedScopes),
                RedirectUris = string.Join(",", clientModel.RedirectUris),
                PostLogoutRedirectUris = string.Join(",", clientModel.PostLogoutRedirectUris),

            };
            return Success(detailModel);
        }


        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.Admin)]
        public async Task<IActionResult> AddOrUpdate(ClientCreateModel request)
        {
            if (request == null) return Error("请求参数不正确");
            if (string.IsNullOrEmpty(request.Id))
            {
                return await AddClients(request);
            }
            else
            {
                return await UpdateClients(request);
            }
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Authorize(Policy = ConstKey.Admin)]
        public async Task<IActionResult> Delete(string id)
        {
            int indId = Convert.ToInt32(id);
            var client = await _configurationDb.Clients.FindAsync(indId);
            if (client == null) return Error("未查询到数据");
            try
            {
                _configurationDb.Clients.Remove(client);
                await _configurationDb.SaveChangesAsync();
                return Success("删除成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        #region 私有函数
        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<IActionResult> AddClients(ClientCreateModel request)
        {
            try
            {
                var isExist = await _configurationDb.Clients.AnyAsync(c => c.ClientId == request.ClientId);
                if (isExist) return Error($"已存在客户端Id[{request.ClientId}]");
                IdentityServer4.Models.Client client = new IdentityServer4.Models.Client()
                {
                    Enabled = request.Enabled,
                    ClientId = request.ClientId,
                    ClientName = request.ClientName,
                    Description = request.Description,
                    AllowedCorsOrigins = request.AllowedCorsOrigins?.Split(","),
                    AllowedGrantTypes = request.AllowedGrantTypes?.Split(","),
                    AllowedScopes = request.AllowedScopes?.Split(","),
                    PostLogoutRedirectUris = request.PostLogoutRedirectUris?.Split(","),
                    RedirectUris = request.RedirectUris?.Split(","),
                };

                if (!string.IsNullOrEmpty(request.ClientSecrets))
                {
                    client.ClientSecrets = new List<Secret>() { new Secret(request.ClientSecrets.Sha256()) };
                }
                var result = await _configurationDb.Clients.AddAsync(client.ToEntity());
                await _configurationDb.SaveChangesAsync();
                return Success("添加成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        /// <summary>
        /// 编辑客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<IActionResult> UpdateClients(ClientCreateModel request)
        {
            try
            {
                int intId = Convert.ToInt32(request.Id);
                var client = await _configurationDb.Clients
                    .Include(d => d.AllowedGrantTypes)
                    .Include(d => d.AllowedScopes)
                    .Include(d => d.AllowedCorsOrigins)
                    .Include(d => d.RedirectUris)
                    .Include(d => d.PostLogoutRedirectUris)
                    .FirstOrDefaultAsync(d => d.Id == intId);
                client.ClientName = request.ClientName;
                client.Description = request.Description;
                client.Enabled = request.Enabled;

                if (!string.IsNullOrEmpty(request.AllowedCorsOrigins))
                {
                    var AllowedCorsOrigins = new List<IdentityServer4.EntityFramework.Entities.ClientCorsOrigin>();
                    request.AllowedCorsOrigins.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
                    {
                        AllowedCorsOrigins.Add(new IdentityServer4.EntityFramework.Entities.ClientCorsOrigin()
                        {
                            Client = client,
                            ClientId = client.Id,
                            Origin = s
                        });
                    });
                    client.AllowedCorsOrigins = AllowedCorsOrigins;
                }

                if (!string.IsNullOrEmpty(request.AllowedGrantTypes))
                {
                    var AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>();
                    request?.AllowedGrantTypes.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
                    {
                        AllowedGrantTypes.Add(new IdentityServer4.EntityFramework.Entities.ClientGrantType()
                        {
                            Client = client,
                            ClientId = client.Id,
                            GrantType = s
                        });
                    });
                    client.AllowedGrantTypes = AllowedGrantTypes;
                }

                if (!string.IsNullOrEmpty(request.AllowedScopes))
                {
                    var AllowedScopes = new List<IdentityServer4.EntityFramework.Entities.ClientScope>();
                    request.AllowedScopes.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
                    {
                        AllowedScopes.Add(new IdentityServer4.EntityFramework.Entities.ClientScope()
                        {
                            Client = client,
                            ClientId = client.Id,
                            Scope = s
                        });
                    });
                    client.AllowedScopes = AllowedScopes;
                }

                if (!string.IsNullOrEmpty(request.PostLogoutRedirectUris))
                {
                    var PostLogoutRedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>();
                    request.PostLogoutRedirectUris.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
                    {
                        PostLogoutRedirectUris.Add(new IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri()
                        {
                            Client = client,
                            ClientId = client.Id,
                            PostLogoutRedirectUri = s
                        });
                    });
                    client.PostLogoutRedirectUris = PostLogoutRedirectUris;
                }

                if (!string.IsNullOrEmpty(request.RedirectUris))
                {
                    var RedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientRedirectUri>();
                    request.RedirectUris.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
                    {
                        RedirectUris.Add(new IdentityServer4.EntityFramework.Entities.ClientRedirectUri()
                        {
                            Client = client,
                            ClientId = client.Id,
                            RedirectUri = s
                        });
                    });
                    client.RedirectUris = RedirectUris;
                }
                var result = _configurationDb.Clients.Update(client);
                await _configurationDb.SaveChangesAsync();
                return Success("编辑成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }
        #endregion

    }
}
