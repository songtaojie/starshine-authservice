﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Starshine.JsonSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthserviceCoreServiceCollectionExtensions
    {
        public static void AddAuthserviceCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAdminOptions();
            services.AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    //options.Conventions.Add(new WebApiApplicationModelConvention());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new SystemTextJsonDateTimeJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new SystemTextJsonNullableDateTimeJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new SystemTextJsonLongToStringJsonConverter());
                });
            // 雪花Id

            // 配置Nginx转发获取客户端真实IP
            // 注1：如果负载均衡不是在本机通过 Loopback 地址转发请求的，一定要加上options.KnownNetworks.Clear()和options.KnownProxies.Clear()
            // 注2：如果设置环境变量 ASPNETCORE_FORWARDEDHEADERS_ENABLED 为 True，则不需要下面的配置代码
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });
            // 即时通讯
            services.AddSignalR();
            //// logo显示
            //services.AddLogoDisplay();
            //// 验证码
            //services.AddLazyCaptcha(configuration);
        }

    }
}