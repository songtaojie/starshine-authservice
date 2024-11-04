using System;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Starshine.Authservice.EntityFrameworkCore.DbContexts;
using Starshine.Authservice.EntityFrameworkCore.SeedDatas;
using Starshine.Common;

namespace Starshine.Authservice
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                .WriteTo.File($"log/{DateTime.Now:yyyy-MM-dd}.log",
                              fileSizeLimitBytes: 1_000_000,
                              rollOnFileSizeLimit: true,
                              shared: true,
                              flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            try
            {
                var seed = args.Contains("/seed");
                if (seed)
                {
                    args = args.Except(new[] { "/seed" }).ToArray();
                }
                var host = CreateHostBuilder(args).Build();
                host.MigrateDbContext<PersistedGrantDbContext>()
                    .MigrateDbContext<ConfigurationDbContext>((db, _) =>
                    {
                        //if (seed) SeedData.EnsureSeedData(db);
                        SeedData.EnsureSeedData(db);
                    }).MigrateDbContext<ApplicationDbContext>((db, s) =>
                    {
                        //if (seed) SeedData.EnsureSeedData(s,db);
                        SeedData.EnsureSeedData(s, db);
                    });
                host.Run();
                ConsoleHelper.WriteSuccessLine("program success");
                return 1;
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteErrorLine(string.Format("�쳣��Ϣ��{0}", ex.Message));
                ConsoleHelper.WriteErrorLine(string.Format("��ջStackTrace��{0}", ex.StackTrace));
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://*:5002");
                });
    }
}
