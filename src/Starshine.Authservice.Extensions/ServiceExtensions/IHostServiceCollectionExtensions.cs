using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Starshine.Authservice.EntityFrameworkCore.DbContexts;
using Starshine.Authservice.EntityFrameworkCore.SeedDatas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Hosting
{
    public static class IHostServiceCollectionExtensions
    {

        public static IApplicationBuilder MigrateDbContext(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                MigrateDbContext<PersistedGrantDbContext>(scope.ServiceProvider);
                MigrateDbContext<ConfigurationDbContext>(scope.ServiceProvider, (db, _) =>
                {
                    SeedData.EnsureSeedData(db);
                });
                MigrateDbContext<ApplicationDbContext>(scope.ServiceProvider, (db, s) =>
                {
                    SeedData.EnsureSeedData(s, db);
                });
            }
            return app;
        }

        /// <summary>
        /// 数据库迁移文件执行
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="webHost"></param>
        /// <param name="seeder"></param>
        /// <returns></returns>
        public static void MigrateDbContext<TContext>(IServiceProvider serviceProvider, Action<TContext, IServiceProvider> seeder = null) where TContext : DbContext
        {
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
           
            try
            {
                var context = serviceProvider.GetRequiredService<TContext>();
                logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
                var retry = Policy.Handle<Exception>()
                    .WaitAndRetry(new TimeSpan[]
                    {
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(10),
                            TimeSpan.FromSeconds(15),
                    });

                retry.Execute(() =>
                {
                    context.Database.Migrate();
                    seeder?.Invoke(context, serviceProvider);
                });

                logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");
            }
        }

    }
}
