using Starshine.Authservice.Authorization;
using Starshine.Authservice.Common;
using Starshine.Authservice.Data;
using Starshine.Authservice.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerServiceExtensions
    {
        /// <summary>
        /// 添加IdentityServer4
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddIdentityServerSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDb(configuration);

            services.AddIdentity<AspNetUsers, AspNetRoles>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                //options.Events.OnRedirectToAccessDenied = (c) =>
                //{
                //    return Task.CompletedTask;
                //};
                options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/oauth2/accessdenied");
                options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/oauth2/authorize");
            });

            var builder = services.AddIdentityServer(options =>
            {
                //options.Events.RaiseErrorEvents = true;
                //options.Events.RaiseInformationEvents = true;
                //options.Events.RaiseFailureEvents = true;
                //options.Events.RaiseSuccessEvents = true;
                //// see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                //options.EmitStaticAudienceClaim = true;
                options.UserInteraction.LoginUrl = "/oauth2/authorize";
                options.UserInteraction.LogoutUrl = "/oauth2/logout";
                options.UserInteraction.ErrorUrl = "/error";
            }).AddAspNetIdentity<AspNetUsers>();

            builder.AddConfigAndOperateStore(configuration);
            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            //        // register your IdentityServer with Google at https://console.developers.google.com
            //        // enable the Google+ API
            //        // set the redirect URI to https://localhost:5001/signin-google
            //        options.ClientId = "copy client ID from Google here";
            //        options.ClientSecret = "copy client secret from Google here";
            //    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstKey.SuperAdmin, policy => policy.RequireClaim(MyJwtClaimTypes.RoleName, ConstKey.SuperAdmin));
            });
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();
            return builder;
        }
    }
}
