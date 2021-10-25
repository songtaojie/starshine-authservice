using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.EntityFramework.Mappers;
using Hx.IdentityServer.Data;
using Hx.IdentityServer.Model;
using Hx.IdentityServer.Entity;
using Hx.IdentityServer.Common;

namespace Hx.IdentityServer.Data
{
    /// <summary>
    /// 种子
    /// </summary>
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            //迁移，在解决方案文件路径中执行迁移，指定启动项目，和迁移项目
            //dotnet ef -s src/Hx.IdentityServer -p src/Hx.IdentityServer.Model migrations add InitApplicationDb -c ApplicationDbContext
            //  dotnet ef -s src/Hx.IdentityServer -p src/Hx.IdentityServer.Model migrations remove -c ApplicationDbContext
            // dotnet ef -s src/Hx.IdentityServer -p src/Hx.IdentityServer.Model migrations add InitPersistedGrantDb -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
            // dotnet ef -s src/Hx.IdentityServer -p src/Hx.IdentityServer.Model migrations remove -c PersistedGrantDbContext
            // dotnet ef -s src/Hx.IdentityServer -p src/Hx.IdentityServer.Model migrations add InitConfigurationDb -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb
            // dotnet ef -s src/Hx.IdentityServer -p src/Hx.IdentityServer.Model migrations remove -c ConfigurationDbContext
            ConsoleHelper.WriteInfoLine("Seeding database...");
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
               
                {
                    scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                    var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                    context.Database.Migrate();
                    EnsureSeedData(context);
                }

                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();
                    EnsureSeedData(scope.ServiceProvider, context);
                }
            }
            ConsoleHelper.WriteInfoLine("Done seeding database.");
        }
        #region 客户端授权种子数据
        /// <summary>
        /// 客户端授权数据
        /// </summary>
        /// <param name="context"></param>
        public static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                ConsoleHelper.WriteSuccessLine("Clients being populated");
                foreach (var client in Config.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                ConsoleHelper.WriteSuccessLine("IdentityResources being populated");
                foreach (var resource in Config.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("IdentityResources already populated");
            }

            if (!context.ApiResources.Any())
            {
                ConsoleHelper.WriteSuccessLine("ApiResources being populated");
                foreach (var resource in Config.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("ApiResources already populated");
            }

            if (!context.ApiScopes.Any())
            {
                ConsoleHelper.WriteSuccessLine("ApiScopes being populated");
                foreach (var resource in Config.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                ConsoleHelper.WriteWarningLine("ApiScopes already populated");
            }
        }
        #endregion

        #region 用户种子数据
        public static void EnsureSeedData(IServiceProvider serviceProvider,ApplicationDbContext context)
        {
            var userMgr = serviceProvider.GetRequiredService<UserManager<AspNetUsers>>();
            var roleMgr = serviceProvider.GetRequiredService<RoleManager<AspNetRoles>>();
            
            var userList = GetApplicationUsers();
            var roleList = GetApplicationRoles();
            var userRoleList = GetApplicationUserRoles(userList, roleList);
            foreach (var role in roleList)
            {
                var roleItem = roleMgr.FindByNameAsync(role.Name).Result;
                if (roleItem == null)
                {
                    roleItem = role;
                    var result = roleMgr.CreateAsync(roleItem).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception("创建角色异常："+result.Errors.First().Description);
                    }
                }
                ConsoleHelper.WriteInfoLine($"{roleItem?.Name} created");//AspNetUserClaims 表
            }
            userList.ForEach(user =>
            {
                var userItem = userMgr.FindByNameAsync(user.UserName).Result;
                if (userItem == null)
                {
                    userItem = user;
                    var result = userMgr.CreateAsync(userItem, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var roleIdList = userRoleList.Where(r => r.UserId == userItem.Id).Select(r=>r.RoleId).Distinct();
                    var currentRoleList = roleList.Where(r => roleIdList.Contains(r.Id));

                    var claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Name, userItem.UserName),
                        new Claim(JwtClaimTypes.Email, userItem.Email),
                    };
                    claims.AddRange(roleIdList.Select(s => new Claim(JwtClaimTypes.Role, s.ToString())));
                    result = userMgr.AddClaimsAsync(userItem, claims).Result;//表AspNetUserClaims添加数据
                    if (!result.Succeeded)
                    {
                        throw new Exception("创建账户异常："+result.Errors.First().Description);
                    }
                    // 为账号分配角色
                    foreach (var role in currentRoleList)
                    {
                        var roleResult = userMgr.AddToRoleAsync(userItem, role.Name).Result;
                        if (!roleResult.Succeeded)
                        {
                            throw new Exception("分配角色异常：" + roleResult.Errors.First().Description);
                        }
                    }
                    ConsoleHelper.WriteErrorLine($"{userItem.UserName} created", ConsoleColor.Green);
                }
                else
                {
                    ConsoleHelper.WriteErrorLine($"{userItem.UserName} already exists");
                }

            });
           
        }
        #endregion
        #region 用户角色
        private static List<AspNetUsers> GetApplicationUsers()
        {
            return new List<AspNetUsers>
            {
                new AspNetUsers
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    Sex = "1",
                    Age = 30,
                    Birthday = DateTime.Parse("1991-10-10"),
                    Address="北京市",
                    RealName = "宋子轩"
                },
                new AspNetUsers
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    Sex = "2",
                    Age = 30,
                    Birthday = DateTime.Parse("1991-10-10"),
                    Address="北京市",
                    RealName = "宋佳"
                }
            };
        }

        private static List<AspNetRoles> GetApplicationRoles()
        {
            return new List<AspNetRoles>
            {
                new AspNetRoles
                { 
                    Name = ConstKey.Client,
                    Description="Api资源权限",
                    CreateTime=DateTime.Now,
                },
                new AspNetRoles
                {
                    Name = ConstKey.SuperAdmin,
                    Description = "超级管理员",
                    CreateTime=DateTime.Now,
                }
            };
        }

        private static List<AspNetUserRoles> GetApplicationUserRoles(List<AspNetUsers> users, List<AspNetRoles> roles)
        {
            List<AspNetUserRoles> userRoles = new List<AspNetUserRoles>();
            var random = new Random();
            users.ForEach(u =>
            {
                var index = random.Next(0, roles.Count);
                userRoles.Add(new AspNetUserRoles
                {
                    RoleId = roles[index].Id,
                    UserId = u.Id
                });
            });
            return userRoles;
        }
        #endregion
       
    }
}
