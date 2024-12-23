using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
        .Enrich.FromLogContext()
        // uncomment to write to Azure diagnostics stream
        .WriteTo.File($"logs/Log{DateTime.Now:yyyy-MM-dd}.log",
                        fileSizeLimitBytes: 1_000_000,
                        rollOnFileSizeLimit: true,
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(1))
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
        .CreateLogger();

try
{

    Log.Information("Starting web application");
    var builder = WebApplication.CreateBuilder();
    builder.ConfigureStarshineWebApp();
    builder.Services.AddAuthserviceCoreService(builder.Configuration,builder.Environment);
    builder.Host.UseSerilogSetup();
    var app = builder.Build();
    
    app.UseAuthServiceCoreApp(builder.Environment);
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "程序因异常而停止");
}
finally
{
    // 确保在应用程序退出之前刷新和停止内部计时器/线程(避免Linux上的分段错误)
    Log.CloseAndFlush();
}

