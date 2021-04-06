using Hx.IdentityServer.Common;
using Hx.IdentityServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Hx.IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConsoleHelper.WriteSuccessLine("*****��ʼConfigureServicesע�����******");
            var connString = Configuration.GetConnectionString("DefaultConnection");
            ConsoleHelper.WriteInfoLine("���ݿ������ַ�����"+ connString);
            services.AddSameSiteCookiePolicy();
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("default",
            //             policy =>
            //             {
            //                 policy.AllowAnyOrigin()
            //                 .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
            //                 .AllowAnyHeader();
            //             });

            //});
            var mvcBuilder = services.AddControllersWithViews()
                .AddJsonOptions(json => {
                    json.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    json.JsonSerializerOptions.Converters.Add(new DateTimeNullConverter());
                    //json.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                });
            if (Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
            
            services.AddIdentityServerSetup(Configuration);
            ConsoleHelper.WriteSuccessLine("*****ConfigureServicesע��������******");
            Console.WriteLine();
        }

        public void Configure(IApplicationBuilder app)
        {
            ConsoleHelper.WriteSuccessLine("*****��ʼApplicationBuilder����******");
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("default");
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults:new { controller = "Account", action = "Index" });
                //endpoints.MapDefaultControllerRoute();
            });
            ConsoleHelper.WriteSuccessLine("*****ApplicationBuilder�������******");
            Console.WriteLine();
        }
    }
}