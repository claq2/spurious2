using Scraper;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<ScraperWorker>();
builder.Services.AddHostedService<ProductsWorker>();
builder.Services.AddHostedService<InventoriesWorker>();
builder.Services.AddHostedService<StoresWorker>();

var host = builder.Build();
host.Run();
