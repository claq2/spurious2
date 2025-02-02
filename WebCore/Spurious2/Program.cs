using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Serialization;
using Carter;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Spurious2;
using Spurious2.Core2;
using Spurious2.Core2.Densities;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;
using Spurious2.Infrastructure;

#if DEBUG
const string MyOrigins = "MyOrigins";
#endif
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
        NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
        new NetTopologySuite.Geometries.PrecisionModel(1000d),
        4326 /* ,
            // Note the following arguments are only valid for NTS v2.2
            // Geometry overlay operation function set to use (Legacy or NG)
            NetTopologySuite.Geometries.GeometryOverlay.NG,
            // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
            new NetTopologySuite.Geometries.CoordinateEqualityComparer() */
    );
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ISpuriousRepository, SpuriousRepository>();
    //builder.Services.AddDbContext<SpuriousContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SpuriousSqlDb"),
    //        b => b.UseNetTopologySuite()
    //            .EnableRetryOnFailure()
    //            .MigrationsAssembly("Spurious2"))
    ////.EnableSensitiveDataLogging()
    //);

    builder.Services.AddDbContextFactory<SpuriousContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SpuriousSqlDb"),
        b => b.UseNetTopologySuite()
            .EnableRetryOnFailure()
            .MigrationsAssembly("Spurious2"))
//.EnableSensitiveDataLogging()
);

    // Add services to the container.
    builder.Services.AddRazorPages();

    builder.Services.ConfigureHttpJsonOptions(options =>
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    builder.Services.AddCarter();

    builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetDensitiesRequest>());
    builder.Services.AddTransient<IStoreImportingService, StoreImportingService>();
    builder.Services.AddTransient<ISubdivisionImportingService, SubdivisionImportingService>();
#if DEBUG
    builder.Services.AddCors(options =>
        options.AddPolicy(name: MyOrigins, policy => policy.AllowAnyOrigin()));
#endif
    var app = builder.Build();

#if DEBUG
    var sw = new Stopwatch();
    sw.Start();
    await app.MigrateDatabase<SpuriousContext>().ConfigAwait();
    var importTasks = new List<Task>();
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<SpuriousContext>();
        var subdivsWithBoundary = await context.Subdivisions.CountAsync(sd => sd.Boundary != null).ConfigAwait();
        if (subdivsWithBoundary < 5161)
        {
            // add from boundary file
            var subDivImporter = scope.ServiceProvider.GetRequiredService<ISubdivisionImportingService>();
            importTasks.Add(subDivImporter.ImportBoundaryFromCsvFile("subdiv.csv"));
        }

        var subdivsWithPopulation = await context.Subdivisions.CountAsync(sd => sd.Population > 0).ConfigAwait();
        if (subdivsWithPopulation < 4830)
        {
            // add from population file
            var subDivImporter = scope.ServiceProvider.GetRequiredService<ISubdivisionImportingService>();
            importTasks.Add(subDivImporter.ImportPopulationFrom98File("population.csv"));
        }

        await Task.WhenAll(importTasks).ConfigAwait();
        Log.Information("Took {Elapsed} to import subdiv data", sw.Elapsed);

        var storeCount = await context.Stores.CountAsync().ConfigAwait();
        if (storeCount < 653)
        {
            var storeImporter = scope.ServiceProvider.GetRequiredService<IStoreImportingService>();
            await storeImporter.ImportStoresFromCsvFile("stores.csv").ConfigAwait();
        }
    }

    sw.Stop();
    Log.Information("Took {Elapsed} to set up DB", sw.Elapsed);
#endif
    app.UseSecurityHeaders(o => o.AddContentSecurityPolicy(b =>
    {
        b.AddDefaultSrc().Self();
        b.AddScriptSrc()
            .Self()
#if DEBUG
            .From("http://localhost:5000")
            .UnsafeInline()
#elif !DEBUG
                    .WithNonce()
                    .StrictDynamic()
#endif
        ;
        b.AddStyleSrc()
            .Self()
#if !DEBUG
                    .WithNonce()
                    //.WithHash256("NOqkZFG2ZdtACrMCaTo1EEwwqcEiIlzuYuXVVoLIb7o=")
                    //.WithHash256("+69+i2WixHVJ8zb6nUNKV8YNsYG6sjfJ3wmXgULgJJY=")
                    //.WithHash256("j7D01FWM6XmRMJqQn3kMs5KI69yIOCSCFsd3HyMWhxg=")
                    //.WithHash256("3h4buiXsKpWAN+mt/D+CqpDUe38t8xgvRR8xBrUlYlo=")
                    //.WithHash256("YUmm0H/7mTRbX1ONX5ovu/D21KzruIabKVGqKnnSSnY=")
                    //.WithHash256("2v7G6UZEWMyIosAgMnziCoeiu95yWo7Fi0IaIlRrcfA=")
                    //.WithHash256("Gigilx1DnI9IZyIHGiSVRO21Eb/w7wxCAXEAVYH3vo4=")
                    .WithHash256("47DEQpj8HBSa+/TImW+5JCeuQeRkm5NMpJWZG3hSuFU=")
                    .StrictDynamic()
#elif DEBUG
            .UnsafeHashes() // allow use of hashes on style elements, including the login error list
            .UnsafeInline()
#endif
            .WithHashTagHelper();
        b.AddWorkerSrc().From("blob:");
        b.AddConnectSrc().Self()
#if DEBUG
            .From("localhost:*")
            .From("127.0.0.1:*")
            .From("wss://localhost:*")
            .From("ws://localhost:*")
#endif
            .From("spurious2.azurewebsites.net")
            .From("canadacentral-1.in.applicationinsights.azure.com")
            .From("atlas.microsoft.com")
            .From("dc.services.visualstudio.com");
        b.AddFontSrc().Self()
            .Data()
#if DEBUG
            .From("http://localhost:5000")
#endif
            .From("atlas.microsoft.com");
        b.AddFrameSrc().From("https://challenges.cloudflare.com");
        b.AddImgSrc().Self()
#if DEBUG
            .From("localhost:5000")
#endif
            .Blob().Data();
    })
    .AddFrameOptionsDeny()
    .AddContentTypeOptionsNoSniff()
    .AddReferrerPolicyNoReferrer()
    .AddPermissionsPolicy(b =>
    {
        b.AddMidi().None();
        b.AddFullscreen().None();
        b.AddAccelerometer().None();
        b.AddAmbientLightSensor().None();
        b.AddAutoplay().None();
        b.AddCamera().None();
        b.AddEncryptedMedia().None();
        b.AddFederatedLearningOfCohortsCalculation().None();
        b.AddGeolocation().None();
        b.AddMagnetometer().None();
        b.AddMicrophone().None();
        b.AddPayment().None();
        b.AddPictureInPicture().Self().For("https://challenges.cloudflare.com");
        b.AddSpeaker().None();
        b.AddSyncXHR().None();
        b.AddUsb().None();
        b.AddVR().None();
    }));

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
    }

    app.UseStaticFiles();

    app.UseRouting();
#if DEBUG
    app.UseCors(MyOrigins);
#endif
    app.UseAuthorization();
    app.MapControllers();
    app.MapRazorPages();
    app.MapCarter();

    await app.RunAsync().ConfigAwait();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync().ConfigAwait();
}
