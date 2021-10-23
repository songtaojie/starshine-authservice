using Hx.IdentityServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DbContextServiceExtensions
    {
        private static readonly string migrationsAssembly = "Hx.IdentityServer.Model";
        /// <summary>
        /// 添加ApplicationDbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddApplicationDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySqlConnectionString");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("缺少数据库连接字符串配置");
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));
            return services;
        }

        /// <summary>
        /// 添加客户端和资源配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static IIdentityServerBuilder AddConfigAndOperateStore(this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySqlConnectionString");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("缺少数据库连接字符串配置");
            builder.AddConfigurationStore(options =>
             {
                 options.ConfigureDbContext = b => b.UseMySQL(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
             })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseMySQL(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
            });

            return builder;
        }
    }


}
