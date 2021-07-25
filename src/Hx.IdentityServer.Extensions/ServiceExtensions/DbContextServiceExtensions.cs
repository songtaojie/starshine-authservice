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
            var daType = configuration.GetConnectionString("DbType");
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(daType)) daType = "sqlserver";
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("缺少数据库连接字符串配置");
            switch (daType.ToLower().Trim())
            {
                case "mysql":
                    services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString,sqlOptions=>
                        sqlOptions.MigrationsAssembly(migrationsAssembly)));
                    break;
                default:
                    services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, 
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));
                    break;
            }
            return services;
        }

        /// <summary>
        /// 添加客户端和资源配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static IIdentityServerBuilder AddConfigAndOperateStore(this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            var dbType = configuration.GetConnectionString("DbType");
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(dbType)) dbType = "sqlserver";
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("缺少数据库连接字符串配置");
            builder.AddConfigurationStore(options =>
             {
                 options.ConfigureDbContext = dbType.ToLower() switch
                 {
                     "mysql" => b => b.UseMySql(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)),
                     _ => b => b.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)),
                 };
             })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = dbType.ToLower() switch
                {
                    "mysql" => b => b.UseMySql(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)),
                    _ => b => b.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)),
                };
                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
            });

            return builder;
        }
    }


}
