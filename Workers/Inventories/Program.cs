using Inventories;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<InventoriesWorker>();

var host = builder.Build();
host.Run();
