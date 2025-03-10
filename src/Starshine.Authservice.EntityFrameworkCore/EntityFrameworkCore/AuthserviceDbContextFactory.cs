using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Starshine.Authservice.EntityFrameworkCore.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthserviceDbContextFactory : IDesignTimeDbContextFactory<AuthserviceDbContext>
    {
        public AuthserviceDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AuthserviceDbContext>()
                .UseSqlite(configuration.GetConnectionString("Default"))
                .UseSnakeCaseNamingConvention();

            return new AuthserviceDbContext(builder.Options, null!);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Starshine.Authservice.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: true);

            return builder.Build();
        }
    }
}
