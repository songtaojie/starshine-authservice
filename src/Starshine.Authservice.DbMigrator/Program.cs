using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Starshine.Authservice.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                    .MinimumLevel.Override("Acme.BookStore", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Acme.BookStore", LogEventLevel.Information)
#endif
                    .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .AddAppSettingsSecretsJson()
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DbMigratorHostedService>();
                });
    }
}
