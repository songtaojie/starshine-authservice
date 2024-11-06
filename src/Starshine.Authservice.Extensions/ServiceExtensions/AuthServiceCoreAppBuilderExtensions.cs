using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class AuthServiceCoreAppBuilderExtensions
    {
        /// <summary>
        /// 添加应用中间件
        /// </summary>
        /// <param name="app">应用构建器</param>
        /// <param name="env"></param>
        /// <returns>应用构建器</returns>
        public static IApplicationBuilder UseAuthServiceCoreApp(this IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                //app.UseForwardedHeaders();
                app.UseHsts();
            }
            //// 启用HTTPS
            //app.UseHttpsRedirection();

            // 特定文件类型（文件后缀）处理
            // contentTypeProvider.Mappings[".文件后缀"] = "MIME 类型";
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    ContentTypeProvider = FileContentTypeUtil.GetFileExtensionContentTypeProvider()
            //});
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseCors("default");
            //app.UseConsulService(lifetime);
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Account", action = "Index" });
                //endpoints.MapDefaultControllerRoute();
            });
            return app;
        }
    }
}
