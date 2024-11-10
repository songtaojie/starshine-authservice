using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Starshine.JsonSerialization;

namespace Starshine.Authservice
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
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.Converters.Add(new SystemTextJsonDateTimeJsonConverter());
                    jsonOptions.JsonSerializerOptions.Converters.Add(new SystemTextJsonNullableDateTimeJsonConverter());
                    //json.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                });
            if (Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }

            services.AddIdentityServerSetup(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
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
        }
    }
}