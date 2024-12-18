using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return new AuthserviceDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Starshine.Authservice.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
