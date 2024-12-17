using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;

namespace Starshine.Authservice.Domain.Migrator
{
    /// <summary>
    /// 迁移服务
    /// </summary>
    public class AuthserviceDbMigrationService : ITransientDependency
    {
        private readonly ILogger _logger;

        private readonly IDataSeeder _dataSeeder;
        private readonly IEnumerable<IStarshineAuthserviceDbSchemaMigrator> _dbSchemaMigrators;
        private readonly ITenantRepository _tenantRepository;
        private readonly ICurrentTenant _currentTenant;

        public AuthserviceDbMigrationService(
            IDataSeeder dataSeeder,
            IEnumerable<IStarshineAuthserviceDbSchemaMigrator> dbSchemaMigrators,
            ITenantRepository tenantRepository,
            ICurrentTenant currentTenant,
            ILogger<AuthserviceDbMigrationService> logger)
        {
            _dataSeeder = dataSeeder;
            _dbSchemaMigrators = dbSchemaMigrators;
            _tenantRepository = tenantRepository;
            _currentTenant = currentTenant;
            _logger = logger;
        }

        public async Task MigrateAsync()
        {
            var migrationsFolderExists = MigrationsFolderExists();

            if (!migrationsFolderExists)
            {
                _logger.LogInformation("请先生成迁移文件");
                return;
            }

            _logger.LogInformation("已启动的数据库迁移...");

            await MigrateDatabaseSchemaAsync();
            await SeedDataAsync();

            _logger.LogInformation($"成功完成主机数据库迁移.");

            var tenants = await _tenantRepository.GetListAsync(includeDetails: true);

            var migratedDatabaseSchemas = new HashSet<string>();
            foreach (var tenant in tenants)
            {
                using (_currentTenant.Change(tenant.Id))
                {
                    if (tenant.ConnectionStrings.Any())
                    {
                        var tenantConnectionStrings = tenant.ConnectionStrings
                            .Select(x => x.Value)
                            .ToList();

                        if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
                        {
                            await MigrateDatabaseSchemaAsync(tenant);

                            migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
                        }
                    }

                    await SeedDataAsync(tenant);
                }

                _logger.LogInformation($"已成功完成租户 {tenant.Name} 数据库迁移.");
            }

            _logger.LogInformation("已成功完成所有数据库迁移.");
            _logger.LogInformation("您可以安全地结束此过程...");
        }

        private async Task MigrateDatabaseSchemaAsync(Tenant? tenant = null)
        {
            _logger.LogInformation(
                $"迁移{(tenant == null ? "默认" : " 租户：" + tenant.Name)} 数据库...");

            foreach (var migrator in _dbSchemaMigrators)
            {
                await migrator.MigrateAsync();
            }
        }

        private async Task SeedDataAsync(Tenant? tenant = null)
        {
            _logger.LogInformation($"执行 {(tenant == null ? "默认" : "租户 " + tenant.Name)} 数据库种子...");

            await _dataSeeder.SeedAsync(new DataSeedContext(tenant?.Id)
                .WithProperty(IdentityDataSeedContributor.AdminEmailPropertyName, IdentityDataSeedContributor.AdminEmailDefaultValue)
                .WithProperty(IdentityDataSeedContributor.AdminPasswordPropertyName, IdentityDataSeedContributor.AdminPasswordDefaultValue)
            );
        }

        /// <summary>
        /// 迁移目录是否存在
        /// </summary>
        /// <returns></returns>
        private bool MigrationsFolderExists()
        {
            var dbMigrationsProjectFolder = GetEntityFrameworkCoreProjectFolderPath();
            if (string.IsNullOrEmpty(dbMigrationsProjectFolder)) return false;
            return Directory.Exists(Path.Combine(dbMigrationsProjectFolder, "Migrations"));
        }

        private string? GetEntityFrameworkCoreProjectFolderPath()
        {
            var slnDirectoryPath = GetSolutionDirectoryPath();

            if (slnDirectoryPath == null)
            {
                throw new Exception("Solution folder not found!");
            }

            var srcDirectoryPath = Path.Combine(slnDirectoryPath, "src");

            return Directory.GetDirectories(srcDirectoryPath)
                .FirstOrDefault(d => d.EndsWith(".EntityFrameworkCore"));
        }

        private string? GetSolutionDirectoryPath()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (Directory.GetParent(currentDirectory.FullName) != null)
            {
                currentDirectory = Directory.GetParent(currentDirectory.FullName);
                if (currentDirectory == null) return null;

                if (Directory.GetFiles(currentDirectory.FullName).FirstOrDefault(f => f.EndsWith(".sln")) != null)
                {
                    return currentDirectory.FullName;
                }
            }

            return null;
        }
    }
}
