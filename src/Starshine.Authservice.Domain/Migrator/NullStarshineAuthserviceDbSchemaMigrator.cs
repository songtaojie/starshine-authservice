using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Starshine.Authservice.Domain.Migrator
{
    /// <summary>
    /// 默认实现
    /// </summary>
    public class NullStarshineAuthserviceDbSchemaMigrator : IStarshineAuthserviceDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
