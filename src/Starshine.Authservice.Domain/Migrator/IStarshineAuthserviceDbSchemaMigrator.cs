using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starshine.Authservice.Domain
{
    /// <summary>
    /// 程序集迁移
    /// </summary>
    public interface IStarshineAuthserviceDbSchemaMigrator
    {
        /// <summary>
        /// 迁移
        /// </summary>
        /// <returns></returns>
        Task MigrateAsync();
    }
}
