using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain.Data;
using Volo.Abp.DependencyInjection;

namespace Starshine.Authservice.EntityFrameworkCore.EntityFrameworkCore
{
    /// <summary>
    /// EfCore实现
    /// </summary>
    public class EfCoreAuthserviceDbSchemaMigrator
    : IAuthserviceDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EfCoreAuthserviceDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            await _serviceProvider
                .GetRequiredService<AuthserviceDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
