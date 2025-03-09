using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Threading;

namespace Starshine.Authservice.Domain
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IBackgroundJobWorker))]
    public class AuthserviceBackgroundJobWorker : BackgroundJobWorker
    {
        public AuthserviceBackgroundJobWorker(AbpAsyncTimer timer, 
            IOptions<AbpBackgroundJobOptions> jobOptions, 
            IOptions<AbpBackgroundJobWorkerOptions> workerOptions, 
            IServiceScopeFactory serviceScopeFactory, 
            IAbpDistributedLock distributedLock,
            IAbpLazyServiceProvider abpLazyServiceProvider) : base(timer, jobOptions, workerOptions, serviceScopeFactory, distributedLock)
        {
            LazyServiceProvider = abpLazyServiceProvider;
        }
    }
}
