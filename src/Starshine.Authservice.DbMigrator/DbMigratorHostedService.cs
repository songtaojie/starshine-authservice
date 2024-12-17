using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Starshine.Authservice.Domain.Migrator;

namespace Starshine.Authservice.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfiguration _configuration;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = await AbpApplicationFactory.CreateAsync<StarshineAuthserviceDbMigratorModule>(options =>
            {
                options.Services.ReplaceConfiguration(_configuration);
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                await application.InitializeAsync();

                await application
                    .ServiceProvider
                    .GetRequiredService<AuthserviceDbMigrationService>()
                    .MigrateAsync();

                await application.ShutdownAsync();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
