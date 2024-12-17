using Starshine.Authservice.Application.Contracts;
using Starshine.Authservice.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Starshine.Authservice.DbMigrator
{
    [DependsOn(
    typeof(AbpAutofacModule),
    typeof(StarshineAuthserviceEntityFrameworkCoreModule),
    typeof(StarshineAuthserviceApplicationContractsModule)
    )]
    public class StarshineAuthserviceDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
