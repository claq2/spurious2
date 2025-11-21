using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder();
builder.UseEnvironment(EnvironmentName.Development);
builder.ConfigureWebJobs(b =>
{
    b.AddAzureStorageCoreServices();
    b.AddAzureStorageQueues();
});
builder.ConfigureLogging((context, b) =>
{
    b.SetMinimumLevel(LogLevel.Error);
    b.AddFilter("Function", LogLevel.Information);
    b.AddFilter("Host", LogLevel.Debug);
    b.AddConsole();
});

var host = builder.Build();
using (host)
{
    await host.RunAsync().ConfigureAwait(false);
}
