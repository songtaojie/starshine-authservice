using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Starshine.Authservice.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Starshine.Authservice.EntityFrameworkCore.EntityFrameworkCore
{
    /// <summary>
    /// EfCore实现
    /// </summary>
    public class EfCoreStarshineAuthserviceDbSchemaMigrator
    : IStarshineAuthserviceDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EfCoreStarshineAuthserviceDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            await _serviceProvider
                .GetRequiredService<IAuthserviceDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
