//using IdentityServer4.EntityFramework.DbContexts;
//using IdentityServer4.EntityFramework.Entities;
//using IdentityServer4.EntityFramework.Mappers;
//using IdentityServer4.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Starshine.Authservice.Application.Contracts.Dtos.ClientManager;
//using Starshine.Authservice.Application.Contracts.Services;
//using Starshine.Abp.Application.Dtos;
//using Starshine.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Starshine.Authservice.Application.Services
//{
//    public class ClientsManagerService : IClientsManagerService
//    {
//        private readonly ConfigurationDbContext _configurationDb;

//        public ClientsManagerService(ConfigurationDbContext configurationDbContext)
//        {
//            _configurationDb = configurationDbContext;
//        }

        

//        public async Task<ClientDetailResultDto> GetById(string id)
//        {
//            int intId = Convert.ToInt32(id);
//            var client = await _configurationDb.Clients
//            .Include(d => d.AllowedGrantTypes)
//            .FirstOrDefaultAsync(c => c.Id == intId);
//            if (client == null) throw new Exception("未找到当前客户端信息");
//            var model = client.ToModel();
//            return new ClientDetailResultDto
//            {
//                Id = client.Id.ToString(),
//                Enabled = model.Enabled,
//                ClientId = model.ClientId,
//                ClientName = model.ClientName,
//                ClientSecrets = string.Join(",", model.ClientSecrets.Select(s => s.Value)),
//                Description = model.Description,
//                AllowedGrantTypes = model.AllowedGrantTypes.ToList(),
//                IdentityTokenLifetime = model.IdentityTokenLifetime,
//                AccessTokenLifetime = model.AccessTokenLifetime,
//                RequireConsent = model.RequireConsent,
//                AllowRememberConsent = model.AllowRememberConsent,
//                AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime,
//                SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime,
//                RefreshTokenExpiration = model.RefreshTokenExpiration,
//                RefreshTokenUsage = model.RefreshTokenUsage
//            };
//        }

//        public async Task<PagedListResult<ClientPageResultDto>> GetPage(ClientPageParamDto param)
//        {
//            //没有过滤的记录数
//            var query = _configurationDb.Clients
//              .Include(d => d.AllowedGrantTypes);
//            var result = await query.OrderByDescending(c => c.Created)
//                .Select(c => new ClientPageResultDto
//                {
//                    Id = c.Id.ToString(),
//                    Enabled = c.Enabled,
//                    ClientId = c.ClientId,
//                    ClientName = c.ClientName,
//                    Description = c.Description,
//                    IdentityTokenLifetime = c.IdentityTokenLifetime,
//                    AccessTokenLifetime = c.AccessTokenLifetime
//                })
//                .ToOrderAndPageListAsync(param);
//            return result;
//            //var resultList = new List<ClientPageModel>();
//            //clientResult.Items.ForEach(c =>
//            //{
//            //    c.AllowedGrantTypes = GetStrong(c.AllowedGrantTypes);
//            //    resultList.Add(new ClientPageModel
//            //    {
//            //        Id = c.Id.ToString(),
//            //        Enabled = model.Enabled,
//            //        ClientId = model.ClientId,
//            //        ClientName = model.ClientName,
//            //        Description = model.Description,
//            //        AllowedGrantTypes = GetStrong(model.AllowedGrantTypes),
//            //        IdentityTokenLifetime = model.IdentityTokenLifetime,
//            //        AccessTokenLifetime = model.AccessTokenLifetime
//            //    });
//            //});
//            //return new PagedListResult<ClientPageModel>(resultList, clientResult.Total, clientResult.Page, clientResult.PageSize);
//        }

//        private string GetStrong(IEnumerable<string> list)
//        {
//            var strongList = list.Select((s, i) =>
//            {
//                if (i % 2 == 0) return s;
//                return string.Format("<strong>{0}</strong>", s);
//            });
//            return string.Join(", ", strongList);
//        }

//        public async Task<bool> AddOrUpdate(ClientCreateParamDto param)
//        {
//            if (string.IsNullOrEmpty(param.Id))
//            {
//                return await AddClients(param);
//            }
//            else
//            {
//                return await UpdateClients(param);
//            }
//        }

//        #region 私有函数
//        /// <summary>
//        /// 添加客户端
//        /// </summary>
//        /// <param name="param"></param>
//        /// <returns></returns>
//        private async Task<bool> AddClients(ClientCreateParamDto param)
//        {
//            var isExist = await _configurationDb.Clients.AnyAsync(c => c.ClientId == param.ClientId);
//            if (isExist) throw new Exception($"已存在客户端Id[{param.ClientId}]");
//            IdentityServer4.Models.Client client = new IdentityServer4.Models.Client()
//            {
//                Enabled = param.Enabled,
//                ClientId = param.ClientId,
//                ClientName = param.ClientName,
//                Description = param.Description,
//                AllowedGrantTypes = param.AllowedGrantTypes,
//                IdentityTokenLifetime = param.IdentityTokenLifetime,
//                AccessTokenLifetime = param.AccessTokenLifetime,
//                RequireConsent = param.RequireConsent,
//                AllowRememberConsent = param.AllowRememberConsent,
//                RefreshTokenUsage = param.RefreshTokenUsage,
//                RefreshTokenExpiration = param.RefreshTokenExpiration,
//                AbsoluteRefreshTokenLifetime = param.AbsoluteRefreshTokenLifetime,
//                SlidingRefreshTokenLifetime = param.SlidingRefreshTokenLifetime
//            };

//            if (!string.IsNullOrEmpty(param.ClientSecrets))
//            {
//                client.ClientSecrets = new List<IdentityServer4.Models.Secret>() { new IdentityServer4.Models.Secret(param.ClientSecrets.Sha256()) };
//            }
//            var result = await _configurationDb.Clients.AddAsync(client.ToEntity());
//            await _configurationDb.SaveChangesAsync();
//            return true;
//        }

//        /// <summary>
//        /// 编辑客户端
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        private async Task<bool> UpdateClients(ClientCreateParamDto request)
//        {
//            int intId = Convert.ToInt32(request.Id);
//            var client = await _configurationDb.Clients
//                .Include(c => c.AllowedGrantTypes)
//                .FirstOrDefaultAsync(c => c.ClientId == request.ClientId);
//            if (client == null) throw new Exception($"未找到客户端Id[{request.ClientId}]信息");
//            client.ClientName = request.ClientName;
//            client.Description = request.Description;
//            client.Enabled = request.Enabled;
//            client.IdentityTokenLifetime = request.IdentityTokenLifetime;
//            client.AccessTokenLifetime = request.AccessTokenLifetime;
//            client.RequireConsent = request.RequireConsent;
//            client.AllowRememberConsent = request.AllowRememberConsent;
//            client.RefreshTokenUsage = request.RefreshTokenUsage.GetHashCode();
//            client.RefreshTokenExpiration = request.RefreshTokenExpiration.GetHashCode();
//            client.AbsoluteRefreshTokenLifetime = request.AbsoluteRefreshTokenLifetime;
//            client.SlidingRefreshTokenLifetime = request.SlidingRefreshTokenLifetime;
//            // 先删除原来的
//            _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientGrantType>()
//                .RemoveRange(client.AllowedGrantTypes);
//            if (request.AllowedGrantTypes != null && request.AllowedGrantTypes.Count > 0)
//            {
//                var AllowedGrantTypes = new List<IdentityServer4.EntityFramework.Entities.ClientGrantType>();
//                request.AllowedGrantTypes.Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
//                {
//                    AllowedGrantTypes.Add(new IdentityServer4.EntityFramework.Entities.ClientGrantType()
//                    {
//                        Client = client,
//                        ClientId = client.Id,
//                        GrantType = s
//                    });
//                });
//                client.AllowedGrantTypes = AllowedGrantTypes;
//            }
//            var result = _configurationDb.Clients.Update(client);
//            await _configurationDb.SaveChangesAsync();
//            return true;
//        }
//        #endregion
//        public async Task<bool> DeleteById(string id)
//        {
//            int indId = Convert.ToInt32(id);
//            var client = await _configurationDb.Clients.FindAsync(indId);
//            if (client == null) throw new Exception("未查询到数据");
//            _configurationDb.Clients.Remove(client);
//            return await _configurationDb.SaveChangesAsync() > 0;
//        }

//        public async Task<PagedListResult<ClientScPageResultDto>> GetScPage(ClientPageParamDto param)
//        {
//            //没有过滤的记录数
//            var query = _configurationDb.Clients
//              .Include(d => d.AllowedScopes)
//              .Include(d => d.AllowedCorsOrigins);
//            var clientResult = await query.Where(c => c.Enabled)
//                .OrderByDescending(c => c.Created)
//                .Select(c => c)
//                .ToOrderAndPageListAsync(param);
//            var resultList = new List<ClientScPageResultDto>();
//            clientResult.Items.ForEach(c =>
//            {
//                var model = c.ToModel();
//                resultList.Add(new ClientScPageResultDto
//                {
//                    ClientId = model.ClientId,
//                    ClientName = model.ClientName,
//                    AllowedScopes = GetStrong(model.AllowedScopes),
//                    AllowedCorsOrigins = GetStrong(model.AllowedCorsOrigins)
//                });
//            });
//            return new PagedListResult<ClientScPageResultDto>(resultList, clientResult.Total, clientResult.Page, clientResult.PageSize);
//        }

//        public async Task<ClientScDetailResultDto> GetScById(string id)
//        {
//            var client = await _configurationDb.Clients
//                .Include(d => d.AllowedScopes)
//                .Include(d => d.AllowedCorsOrigins)
//                .FirstOrDefaultAsync(c => c.ClientId == id);
//            if (client == null) throw new Exception("未找到当前信息");
//            var clientModel = client.ToModel();
//            return new ClientScDetailResultDto
//            {
//                ClientId = clientModel.ClientId,
//                ClientName = clientModel.ClientName,
//                AllowedCorsOrigins = string.Join(",", clientModel.AllowedCorsOrigins),
//                AllowedScopes = string.Join(",", clientModel.AllowedScopes),
//            };
//        }

//        public async Task<bool> UpdateSc(ClientScCreateParamDto param)
//        {
//            var client = await _configurationDb.Clients
//                    .Include(c => c.AllowedScopes)
//                    .Include(c => c.AllowedCorsOrigins)
//                    .FirstOrDefaultAsync(d => d.ClientId == param.ClientId);
//            if (client == null) throw new Exception($"未找到客户端Id[{param.ClientId}]信息");
//            var tempModel = new IdentityServer4.Models.Client()
//            {
//                AllowedScopes = param.AllowedScopes.Split(",")
//            };
//            //作用域信息
//            _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientScope>()
//                .RemoveRange(client.AllowedScopes);
//            if (!string.IsNullOrEmpty(param.AllowedScopes))
//            {
//                var AllowedScopes = new List<IdentityServer4.EntityFramework.Entities.ClientScope>();
//                param.AllowedScopes.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
//                {
//                    AllowedScopes.Add(new IdentityServer4.EntityFramework.Entities.ClientScope()
//                    {
//                        Client = client,
//                        ClientId = client.Id,
//                        Scope = s
//                    });
//                });
//                client.AllowedScopes = AllowedScopes;
//            }
//            //允许跨域信息
//            _configurationDb.ClientCorsOrigins.RemoveRange(client.AllowedCorsOrigins);
//            if (!string.IsNullOrEmpty(param.AllowedCorsOrigins))
//            {
//                var AllowedCorsOrigins = new List<IdentityServer4.EntityFramework.Entities.ClientCorsOrigin>();
//                param.AllowedCorsOrigins.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
//                {
//                    AllowedCorsOrigins.Add(new IdentityServer4.EntityFramework.Entities.ClientCorsOrigin()
//                    {
//                        Client = client,
//                        ClientId = client.Id,
//                        Origin = s
//                    });
//                });
//                client.AllowedCorsOrigins = AllowedCorsOrigins;
//            }

//            var result = _configurationDb.Clients.Update(client);
//            return await _configurationDb.SaveChangesAsync() > 0;
//        }

//        public async Task<PagedListResult<ClientRuPageResultDto>> GetRuPage(ClientPageParamDto param)
//        {
//            //没有过滤的记录数
//            var query = _configurationDb.Clients
//              .Include(d => d.RedirectUris)
//              .Include(d => d.PostLogoutRedirectUris);
//            var clientResult = await query.Where(c => c.Enabled)
//                .OrderByDescending(c => c.Created)
//                .Select(c => c)
//                .ToOrderAndPageListAsync(param);
//            var resultList = new List<ClientRuPageResultDto>();
//            clientResult.Items.ForEach(c =>
//            {
//                var model = c.ToModel();
//                resultList.Add(new ClientRuPageResultDto
//                {
//                    ClientId = model.ClientId,
//                    ClientName = model.ClientName,
//                    RedirectUris = GetStrong(model.RedirectUris),
//                    PostLogoutRedirectUris = GetStrong(model.PostLogoutRedirectUris)
//                });
//            });
//            return new PagedListResult<ClientRuPageResultDto>(resultList, clientResult.Total, clientResult.Page, clientResult.PageSize);
//        }

//        public async Task<ClientRuDetailResultDto> GetRuById(string id)
//        {
//            var client = await _configurationDb.Clients
//            .Include(d => d.RedirectUris)
//            .Include(d => d.PostLogoutRedirectUris)
//            .FirstOrDefaultAsync(c => c.ClientId == id);
//            if (client == null) throw new Exception("未找到当前信息");
//            var clientModel = client.ToModel();
//            return new ClientRuDetailResultDto
//            {
//                ClientId = clientModel.ClientId,
//                ClientName = clientModel.ClientName,
//                RedirectUris = string.Join(",", clientModel.RedirectUris),
//                PostLogoutRedirectUris = string.Join(",", clientModel.PostLogoutRedirectUris),
//            };
//        }

//        public async Task<bool> UpdateRu(ClientRuCreateParamDto param)
//        {
//            var client = await _configurationDb.Clients
//                     .Include(d => d.RedirectUris)
//                    .Include(d => d.PostLogoutRedirectUris)
//                    .FirstOrDefaultAsync(d => d.ClientId == param.ClientId);
//            if (client == null) throw new Exception($"未找到客户端Id[{param.ClientId}]信息");
//            //退出回调
//            _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>()
//                .RemoveRange(client.PostLogoutRedirectUris);
//            if (!string.IsNullOrEmpty(param.PostLogoutRedirectUris))
//            {
//                var PostLogoutRedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri>();
//                param.PostLogoutRedirectUris.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
//                {
//                    PostLogoutRedirectUris.Add(new IdentityServer4.EntityFramework.Entities.ClientPostLogoutRedirectUri()
//                    {
//                        Client = client,
//                        ClientId = client.Id,
//                        PostLogoutRedirectUri = s
//                    });
//                });
//                client.PostLogoutRedirectUris = PostLogoutRedirectUris;
//            }

//            //回调
//            _configurationDb.Set<IdentityServer4.EntityFramework.Entities.ClientRedirectUri>()
//                .RemoveRange(client.RedirectUris);
//            if (!string.IsNullOrEmpty(param.RedirectUris))
//            {
//                var RedirectUris = new List<IdentityServer4.EntityFramework.Entities.ClientRedirectUri>();
//                param.RedirectUris.Split(",").Where(s => !string.IsNullOrEmpty(s)).ToList().ForEach(s =>
//                {
//                    RedirectUris.Add(new IdentityServer4.EntityFramework.Entities.ClientRedirectUri()
//                    {
//                        Client = client,
//                        ClientId = client.Id,
//                        RedirectUri = s
//                    });
//                });
//                client.RedirectUris = RedirectUris;
//            }

//            var result = _configurationDb.Clients.Update(client);
//            return await _configurationDb.SaveChangesAsync() > 0;
//        }
//    }
//}
