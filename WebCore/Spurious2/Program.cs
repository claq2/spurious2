using System.Globalization;
using System.Text.Json.Serialization;
using Carter;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Spurious2.Core2;
using Spurious2.Core2.Densities;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.All;

namespace Spurious2;

#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
#pragma warning disable CA1506 // Avoid excessive class coupling
public class Program
#pragma warning restore CA1506 // Avoid excessive class coupling
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
{
#pragma warning disable CA1506 // Avoid excessive class coupling
    public static void Main(string[] args)
#pragma warning restore CA1506 // Avoid excessive class coupling
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
            .CreateBootstrapLogger();

#pragma warning disable CA1031 // Do not catch general exception types
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
            builder.Services.AddDbContext<SpuriousContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SpuriousSqlDb"),
                    b => b.UseNetTopologySuite()
                        .EnableRetryOnFailure()
                        .MigrationsAssembly("Spurious2")).EnableSensitiveDataLogging());

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.ConfigureHttpJsonOptions(options =>
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddCarter();

            builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetDensitiesRequest>());

            var app = builder.Build();
#if DEBUG
            app.MigrateDatabase<SpuriousContext>();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SpuriousContext>();
                var testSubdivision = context.Subdivisions.FirstOrDefault(s => s.Id == 3560057);
                if (testSubdivision == null || testSubdivision.Boundary == null)
                {
                    // add from boundary file
                }

                if (testSubdivision == null || testSubdivision.Population == 0)
                {
                    // add from population file
                }

                var testStore = context.Stores.FirstOrDefault();
                if (testStore == null)
                {
                    // add from stores file
                }

                context.SaveChanges();
            }
#endif

            app.UseSecurityHeaders(o => o.AddContentSecurityPolicy(b =>
            {
                b.AddDefaultSrc().Self();
                b.AddScriptSrc().WithNonce().StrictDynamic();
                b.AddStyleSrc()
                    .Self()
                    .WithNonce()
                    .WithHash256("ATeSIcqM6UxTblzmyyICgZWZUpvkDFfJxeK3WjeUN90=")
                    .WithHash256("6IXr1202gJ13H3beeZ6W5EIY+3gdXFsD2u9avWEe/90=")
                    .WithHash256("5ad8McBu1lCYD+EOXcLRS1FeuI8rw9mYscXkLiGKyRc=")
                    .WithHash256("4frXBI2FwF2LM/qKBg0E03wsgxAwjM9XkjmuHhsolJU=")
                    .WithHash256("Ev6x8tBHdhQy7B3Y75SlaTnHyEhorCajhRI7BtClp8w=")
                    .WithHash256("qYi3FSs1kDVtElTOOutxMXxwH9WdFVpZX7kmJrNrzCc=")
                    .UnsafeHashes() // allow use of hashes on style elements, including the login error list
                    .StrictDynamic()
                    .WithHashTagHelper();
                b.AddWorkerSrc().From("blob:");
                b.AddConnectSrc().Self()
                    .From("canadacentral-1.in.applicationinsights.azure.com")
                    .From("atlas.microsoft.com")
                    .From("dc.services.visualstudio.com");
                b.AddFontSrc().Self().From("atlas.microsoft.com");
                b.AddFrameSrc().From("https://challenges.cloudflare.com");
                b.AddImgSrc().Self().Blob().Data();
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

            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();
            app.MapCarter();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

}
