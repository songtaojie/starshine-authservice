using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Starshine.Authservice.EntityFrameworkCore
{
    /// <summary>
    /// DbContextOptionsBuilder扩展
    /// </summary>
    public static class DbContextOptionsBuilderExtension
    {
        /// <summary>
        /// 动态选择数据库
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder<TContext> UseDynamicSql<TContext>(this DbContextOptionsBuilder<TContext> optionsBuilder, IConfiguration configuration)
        where TContext : DbContext
        => (DbContextOptionsBuilder<TContext>)UseDynamicSql((DbContextOptionsBuilder)optionsBuilder, configuration);

        /// <summary>
        /// 动态选择数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"></exception>
        public static DbContextOptionsBuilder UseDynamicSql(this DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            var dbType = configuration.GetConnectionString("DbType");
            var connectionString = configuration.GetConnectionString(ConnectionStrings.DefaultConnectionStringName);
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("缺少数据库连接字符串配置");
            switch (dbType?.ToLower())
            {
                case "mysql":
                    optionsBuilder.UseMySql(ServerVersion.AutoDetect(connectionString));
                    break;
                case "postgresql":
                    optionsBuilder.UseNpgsql(connectionString);
                    break;
                default:
                    optionsBuilder.UseSqlite(connectionString);
                    break;
            }
            return optionsBuilder;
        }

        /// <summary>
        /// 动态选择数据库
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentException"></exception>
        public static AbpDbContextOptions UseDynamicSql(this AbpDbContextOptions options, IConfiguration configuration)
        {
            var dbType = configuration.GetConnectionString("DbType");
            switch (dbType?.ToLower())
            {
                case "mysql":
                    options.UseMySQL();
                    break;
                case "postgresql":
                    options.UseNpgsql();
                    break;
                default:
                    options.UseSqlite();
                    break;
            }
            return options;
        }
    }
}
