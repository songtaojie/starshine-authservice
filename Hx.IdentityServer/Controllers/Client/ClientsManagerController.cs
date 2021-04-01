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
    [Route("client/[action]")]
    [ApiController]
    [Authorize]
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
        [Route("~/client")]
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
        public async Task<IActionResult> GetPage([FromBody] ClientPageParam param)
        {
            //没有过滤的记录数
            var query = _configurationDb.Clients
              .Include(d => d.AllowedGrantTypes);
            var clientResult = await query.OrderByDescending(c => c.Created)
                .Select(c => c)
                .ToOrderAndPageListAsync(param);
            var resultList = new List<ClientPageModel>();
            clientResult.Items.ForEach(c =>
            {
                var model = c.ToModel();
                resultList.Add(new ClientPageModel
                {
                    Id = c.Id.ToString(),
                    Enabled = model.Enabled,
                    ClientId = model.ClientId,
                    ClientName = model.ClientName,
                    Description = model.Description,
                    AllowedGrantTypes = GetStrong(model.AllowedGrantTypes),
                    IdentityTokenLifetime = model.IdentityTokenLifetime,
                    AccessTokenLifetime = model.AccessTokenLifetime
                });
            });
            var result = new PageModel<ClientPageModel>(resultList, clientResult.TotalCount, clientResult.PageIndex, clientResult.PageSize);
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
            .FirstOrDefaultAsync(c => c.Id == intId);
            if (client == null) return Error("未找到当前客户端信息");
            var model = client.ToModel();
            ClientDetailModel detailModel = new ClientDetailModel
            {
                Id = client.Id.ToString(),
                Enabled = model.Enabled,
                ClientId = model.ClientId,
                ClientName = model.ClientName,
                ClientSecrets = string.Join(",", model.ClientSecrets.Select(s => s.Value)),
                Description = model.Description,
                AllowedGrantTypes = model.AllowedGrantTypes.ToList(),
                IdentityTokenLifetime = model.IdentityTokenLifetime,
                AccessTokenLifetime = model.AccessTokenLifetime,
                RequireConsent = model.RequireConsent,
                AllowRememberConsent = model.AllowRememberConsent,
                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
                RefreshTokenExpiration = model.RefreshTokenExpiration,
                RefreshTokenUsage = model.RefreshTokenUsage
            };
            return Success(detailModel);
        }


        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.SuperAdmin)]
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
        [Authorize(Policy = ConstKey.SuperAdmin)]
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
                    AllowedGrantTypes = request.AllowedGrantTypes,
                    IdentityTokenLifetime = request.IdentityTokenLifetime,
                    AccessTokenLifetime = request.AccessTokenLifetime,
                    RequireConsent = request.RequireConsent,
                    AllowRememberConsent = request.AllowRememberConsent,
                    RefreshTokenUsage = request.RefreshTokenUsage,
                    RefreshTokenExpiration = request.RefreshTokenExpiration,
                    AbsoluteRefreshTokenLifetime = request.AbsoluteRefreshTokenLifetime,
                    SlidingRefreshTokenLifetime = request.SlidingRefreshTokenLifetime
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
                    .Include(c=>c.AllowedGrantTypes)
                    .FirstOrDefaultAsync(c => c.ClientId == request.ClientId);
                if (client == null)return Error($"未找到客户端Id[{request.ClientId}]信息");
                client.ClientName = request.ClientName;
                client.Description = request.Description;
                client.Enabled = request.Enabled;
                client.IdentityTokenLifetime = request.IdentityTokenLifetime;
                client.AccessTokenLifetime = request.AccessTokenLifetime;
                client.RequireConsent = request.RequireConsent;
                client.AllowRememberConsent = request.AllowRememberConsent;
                client.RefreshTokenUsage = request.RefreshTokenUsage.GetHashCode();
                client.RefreshTokenExpiration = request.RefreshTokenExpiration.GetHashCode();
                client.AbsoluteRefreshTokenLifetime = request.AbsoluteRefreshTokenLifetime;
                client.SlidingRefreshTokenLifetime = request.SlidingRefreshTokenLifetime;
                // 先删除原来的
                _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientGrantType>()
                    .RemoveRange(client.AllowedGrantTypes);
                if (request.AllowedGrantTypes != null && request.AllowedGrantTypes.Count > 0)
                {
                    var AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>();
                    request.AllowedGrantTypes.Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
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


        #region 作用域和允许跨域
        /// <summary>
        /// 数据列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("~/client/sc")]
        public IActionResult ScIndex()
        {
            ViewData["IsSuperAdmin"] = IsSuperAdmin;
            return View();
        }

        /// <summary>
        /// 作用域和允许跨域的数据列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetScPage([FromBody] ClientPageParam param)
        {
            //没有过滤的记录数
            var query = _configurationDb.Clients
              .Include(d => d.AllowedScopes)
              .Include(d => d.AllowedCorsOrigins);
            var clientResult = await query.Where(c=>c.Enabled)
                .OrderByDescending(c => c.Created)
                .Select(c => c)
                .ToOrderAndPageListAsync(param);
            var resultList = new List<ClientScPageModel>();
            clientResult.Items.ForEach(c =>
            {
                var model = c.ToModel();
                resultList.Add(new ClientScPageModel
                {
                    ClientId = model.ClientId,
                    ClientName = model.ClientName,
                    AllowedScopes = GetStrong(model.AllowedScopes),
                    AllowedCorsOrigins = GetStrong(model.AllowedCorsOrigins)
                });
            });
            var result = new PageModel<ClientScPageModel>(resultList, clientResult.TotalCount, clientResult.PageIndex, clientResult.PageSize);
            return Success(result);
        }

        /// <summary>
        /// 获取作用域/允许跨域详情数据
        /// </summary>
        /// <param name="id">客户端id</param>
        /// <returns></returns>
        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetSc(string clientId)
        {
            var client = await _configurationDb.Clients
            .Include(d => d.AllowedScopes)
            .Include(d => d.AllowedCorsOrigins)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
            if (client == null) return Error("未找到当前信息");
            var clientModel = client.ToModel();
            ClientScDetailModel detailModel = new ClientScDetailModel
            {
                ClientId = clientModel.ClientId,
                ClientName = clientModel.ClientName,
                AllowedCorsOrigins = string.Join(",", clientModel.AllowedCorsOrigins),
                AllowedScopes = string.Join(",", clientModel.AllowedScopes),
            };
            return Success(detailModel);
        }

        /// <summary>
        /// 数据修改编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> UpdateSc(ClientScCreateModel request)
        {
            if (request == null) return Error("请求参数不正确");
            try
            {
                var client = await _configurationDb.Clients
                    .Include(c=>c.AllowedScopes)
                    .Include(c=>c.AllowedCorsOrigins)
                    .FirstOrDefaultAsync(d => d.ClientId == request.ClientId);
                if (client == null) return Error($"未找到客户端Id[{request.ClientId}]信息");
                var tempModel = new IdentityServer4.Models.Client()
                {
                    AllowedScopes = request.AllowedScopes.Split(",")
                };
                //作用域信息
                _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientScope>()
                    .RemoveRange(client.AllowedScopes);
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
                //允许跨域信息
                _configurationDb.ClientCorsOrigins.RemoveRange(client.AllowedCorsOrigins);
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

                var result = _configurationDb.Clients.Update(client);
                await _configurationDb.SaveChangesAsync();
                return Success("保存成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }
        #endregion


        #region 回调地址/退出回调

        /// <summary>
        /// 数据列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("~/client/ru")]
        public IActionResult RuIndex()
        {
            ViewData["IsSuperAdmin"] = IsSuperAdmin;
            return View();
        }

        /// <summary>
        /// 作用域和允许跨域的数据列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetRuPage([FromBody] ClientPageParam param)
        {
            //没有过滤的记录数
            var query = _configurationDb.Clients
              .Include(d => d.RedirectUris)
              .Include(d => d.PostLogoutRedirectUris);
            var clientResult = await query.Where(c => c.Enabled)
                .OrderByDescending(c => c.Created)
                .Select(c => c)
                .ToOrderAndPageListAsync(param);
            var resultList = new List<ClientRuPageModel>();
            clientResult.Items.ForEach(c =>
            {
                var model = c.ToModel();
                resultList.Add(new ClientRuPageModel
                {
                    ClientId = model.ClientId,
                    ClientName = model.ClientName,
                    RedirectUris = GetStrong(model.RedirectUris),
                    PostLogoutRedirectUris = GetStrong(model.PostLogoutRedirectUris)
                });
            });
            var result = new PageModel<ClientRuPageModel>(resultList, clientResult.TotalCount, clientResult.PageIndex, clientResult.PageSize);
            return Success(result);
        }

        /// <summary>
        /// 获取作用域/允许跨域详情数据
        /// </summary>
        /// <param name="id">客户端id</param>
        /// <returns></returns>
        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetRu(string clientId)
        {
            var client = await _configurationDb.Clients
            .Include(d => d.RedirectUris)
            .Include(d => d.PostLogoutRedirectUris)
            .FirstOrDefaultAsync(c => c.ClientId == clientId);
            if (client == null) return Error("未找到当前信息");
            var clientModel = client.ToModel();
            ClientRuDetailModel detailModel = new ClientRuDetailModel
            {
                ClientId = clientModel.ClientId,
                ClientName = clientModel.ClientName,
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
        [Authorize(Policy = ConstKey.SuperAdmin)]
        public async Task<IActionResult> UpdateRu(ClientRuCreateModel request)
        {
            if (request == null) return Error("请求参数不正确");
            try
            {
                var client = await _configurationDb.Clients
                     .Include(d => d.RedirectUris)
                    .Include(d => d.PostLogoutRedirectUris)
                    .FirstOrDefaultAsync(d => d.ClientId == request.ClientId);
                if (client == null) return Error($"未找到客户端Id[{request.ClientId}]信息");
                //退出回调
                _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>()
                    .RemoveRange(client.PostLogoutRedirectUris);
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

                //回调
                _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientRedirectUri>()
                    .RemoveRange(client.RedirectUris);
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
                return Success("保存成功");
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        #endregion

        private string GetStrong(IEnumerable<string> list)
        {
            var strongList = list.Select((s, i) =>
            {
                if (i % 2 == 0) return s;
                return string.Format("<strong>{0}</strong>", s);
            });
            return string.Join(", ", strongList);
        }
    }
}
