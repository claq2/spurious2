using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Spurious2.Function2;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureLogging((hostingContext, logging) =>
    {
        var filepath = string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WEBSITE_CONTENTSHARE")) ?
                        "log.txt" :
                        @"D:\home\LogFiles\Application\log.txt";

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Worker", LogEventLevel.Warning)
            .MinimumLevel.Override("Host", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .MinimumLevel.Override("Function", LogEventLevel.Debug)
            .MinimumLevel.Override("Azure.Storage", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Core", LogEventLevel.Error)
            .MinimumLevel.Override("Azure.Identity", LogEventLevel.Error)
            .Enrich.WithProperty("Application", "SHDev Blog Functions")
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Debug)
            .WriteTo.File(filepath, LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        logging.AddSerilog(Log.Logger, true);
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IGreeterService, GreeterService>();
    })
    .Build();

host.Run();
