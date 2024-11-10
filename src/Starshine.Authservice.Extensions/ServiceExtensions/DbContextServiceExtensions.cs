using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Starshine.Authservice.EntityFrameworkCore.DbContexts;
using System;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DbContextServiceExtensions
    {
        private static readonly string migrationsAssembly = "Starshine.Authservice.EntityFrameworkCore";
        /// <summary>
        /// 添加ApplicationDbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static IServiceCollection AddApplicationDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext, ApplicationDbContext>(options => ConfigDbContextOptions(options,configuration));
            return services;
        }

        /// <summary>
        /// 添加客户端和资源配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static IIdentityServerBuilder AddConfigAndOperateStore(this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            builder.AddConfigurationStore(options =>
             {
                 options.ConfigureDbContext = b => ConfigDbContextOptions(b,configuration);
             })
            // this adds the operational data from DB (codes, tokens, consents)
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => ConfigDbContextOptions(b, configuration);
                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
                // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
            });

            return builder;
        }

        private static void ConfigDbContextOptions(DbContextOptionsBuilder optionsBuilder,IConfiguration configuration)
        {
            var dbType = configuration.GetConnectionString("DbType");
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("缺少数据库连接字符串配置");
            switch (dbType.ToLower())
            {
                case "mysql":
                    optionsBuilder.UseMySql(ServerVersion.AutoDetect(connectionString),
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
                    break;
                case "sqlserver":
                    optionsBuilder.UseSqlServer(connectionString, 
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
                    break;
                default:
                    optionsBuilder.UseSqlite(connectionString,
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
                    break;
            }
        }
    }


}
