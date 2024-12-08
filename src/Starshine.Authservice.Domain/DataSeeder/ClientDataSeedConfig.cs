using IdentityServer4.Models;
using IdentityServer4;

namespace Starshine.Authservice.Domain.DataSeeder
{
    public static class ClientDataSeedConfig
    {

        public static IEnumerable<IdentityServer4.Models.Client> Clients =>
           new List<IdentityServer4.Models.Client>
           {
                // machine to machine client
                new IdentityServer4.Models.Client
                {
                    ClientId = "client",
                    ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials,
                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                
                // interactive ASP.NET Core MVC client
                new IdentityServer4.Models.Client
                {
                    ClientId = "mvc",
                    ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    
                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
                 // 1、blog.vue 前端vue项目
                new Client {
                    ClientId = "blogvuejs",
                    ClientName = "Blog.Vue JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           {
                        "http://vueblog.neters.club/callback",
                        "http://apk.neters.club/oauth2-redirect.html",

                        "http://localhost:8090/callback",
                        "http://localhost:8090/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = { "http://vueblog.neters.club","http://localhost:8090" },
                    AllowedCorsOrigins =     { "http://vueblog.neters.club","http://localhost:8090" },

                    AccessTokenLifetime=3600,

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "blog.core.api.BlogModule"
                    }
                },
                // 2、blog.admin 前端vue项目
                new Client {
                    ClientId = "blogadminjs",
                    ClientName = "Blog.Admin JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://vueadmin.neters.club/callback",
                        "http://apk.neters.club/oauth2-redirect.html",

                        "http://localhost:8091/callback",
                        "http://localhost:8091/oauth2-redirect.html",
                    },
                    PostLogoutRedirectUris = { "http://vueadmin.neters.club","http://localhost:8091" },
                    AllowedCorsOrigins =     { "http://vueadmin.neters.club","http://localhost:8091"  },

                    AccessTokenLifetime=3600,

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "blog.core.api"
                    }
                },
                // 3、nuxt.tbug 前端nuxt项目
                new Client {
                    ClientId = "tibugnuxtjs",
                    ClientName = "Nuxt.tBug JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "http://tibug.neters.club/callback" },
                    PostLogoutRedirectUris = { "http://tibug.neters.club" },
                    AllowedCorsOrigins =     { "http://tibug.neters.club" },

                    AccessTokenLifetime=3600,

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "blog.core.api"
                    }
                },
                // 4、DDD 后端MVC项目
                new Client
                {
                    ClientId = "chrisdddmvc",
                    ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken=true,//将用户所有的claims包含在IdToken内
                
                    // where to redirect to after login
                    RedirectUris = { "http://ddd.neters.club/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://ddd.neters.club/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "rolename",
                    }
                },
                // 5、控制台客户端
                new Client
                {
                    ClientId = "Console",
                    ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                    AllowedGrantTypes = new List<string>()
                    {
                        GrantTypes.ResourceOwnerPassword.First(),
                        GrantTypes.ClientCredentials.First(),
                        //GrantTypeCustom.ResourceWeixinOpen,
                    },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "blog.core.api"
                    }
                },
                // 6、mvp 后端blazor.server项目
                new Client
                {
                    ClientId = "blazorserver",
                    ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    AlwaysIncludeUserClaimsInIdToken=true,//将用户所有的claims包含在IdToken内
                    AllowAccessTokensViaBrowser = true,
                
                    // where to redirect to after login
                    RedirectUris = { "https://mvp.neters.club/signin-oidc" },

                    AllowedCorsOrigins =     { "https://mvp.neters.club" },
                   
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://mvp.neters.club/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "rolename",
                        "blog.core.api"
                    }
                },

                // 7、测试 hybrid 模式
                new Client
                {
                    ClientId = "hybridclent",
                    ClientName="Demo MVC Client",
                    ClientSecrets = { new IdentityServer4.Models.Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Hybrid,

                    RequirePkce = false,

                    RedirectUris = { "http://localhost:1003/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:1003/signout-callback-oidc" },

                    AllowOfflineAccess=true,
                    AlwaysIncludeUserClaimsInIdToken=true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "rolename",
                        "blog.core.api"
                    }
                }
           };
    }
}
