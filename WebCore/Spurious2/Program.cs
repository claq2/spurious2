using System.Text.Json.Serialization;
using Carter;
using Microsoft.EntityFrameworkCore;
using Spurious2.Core2;
using Spurious2.Core2.Densities;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.Models;

namespace Spurious2;

#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
public class Program
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
#pragma warning disable CA1506 // Avoid excessive class coupling
    public static void Main(string[] args)
#pragma warning restore CA1506 // Avoid excessive class coupling
    {
        var builder = WebApplication.CreateBuilder(args);

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
        builder.Services.AddDbContext<SpuriousContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("SpuriousSqlDb"),
                x => x.UseNetTopologySuite().EnableRetryOnFailure()));

        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddCarter();

        builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetDensitiesRequest>());

        var app = builder.Build();

        app.UseSecurityHeaders(o => o.AddContentSecurityPolicy(b =>
        {
            b.AddDefaultSrc().Self();
            b.AddScriptSrc().WithNonce().StrictDynamic();
            b.AddStyleSrc()
                .Self()
                .WithNonce()
                //.WithHash256("aqNNdDLnnrDOnTNdkJpYlAxKVJtLt9CtFLklmInuUAE=") // display:none
                .WithHash256("ATeSIcqM6UxTblzmyyICgZWZUpvkDFfJxeK3WjeUN90=")
                .WithHash256("6IXr1202gJ13H3beeZ6W5EIY+3gdXFsD2u9avWEe/90=")
                .WithHash256("5ad8McBu1lCYD+EOXcLRS1FeuI8rw9mYscXkLiGKyRc=")
                .WithHash256("4frXBI2FwF2LM/qKBg0E03wsgxAwjM9XkjmuHhsolJU=")
                //.WithHash256("KOn1ayjT/YFGzQrAGWjKJYhgg5mJTDMbsUv9P5jyiBI=")
                //.WithHash256("e3eOpOzUImZzwHNXItARyghMSoToPE9xvAaAnp9I4jY=")
                //.WithHash256("Cxx6bOC0Zxb3YVgRhsPywgmydIMfs1Ev+hfQe+W0yww=")
                //.WithHash256("mwfCSNT2gG4EguqqR9xx/OsUjpqqxZKhCvVZvIZxvZM=")
                //.WithHash256("6TlvdS/xVe6xq0dnsTUGBPfB583S+afLe59gwkcGO38=")
                //.WithHash256("m8CW1jJIAJykofZ3oTkCM8UbceyIjLdGYTHwN8uUaD8=")
                //.WithHash256("XUA1wQVvMfqOjYpetEE3e/aFfHYzb7GM3b5ruPIuaQQ=")
                //.WithHash256("t1qXqTDrhjkLrSuTK6cfQXxZgFvv6YshzZyqbOEqUSQ=")
                //.WithHash256("MOC36pv13im130jzJVJepGgY1LHrSvcKFSspd+WdfM0=")
                //.WithHash256("1YWm9UYb0n5iwyXUot7Wd42C9HJbKoZL4U8V0/n/uYo=")
                //.WithHash256("sYIZGfQC/euJyM8mmGfPuF6eb9eo/HeULI/gzbBfFoQ=")
                //.WithHash256("Sqd2AvWzMkQ0Y3jTs3zazfKy/NYFiCX2yFrS2vEdOD4=")
                .WithHash256("Ev6x8tBHdhQy7B3Y75SlaTnHyEhorCajhRI7BtClp8w=")
                //.WithHash256("4VwUCoMkb7ArbBGLMdVijUl1S6eLrBEXcTKEwPYiY9I=")
                //.WithHash256("LmDQlLw06KJf1PMXYZEHOXFHH5ZV2Vr59qG0oz6B444=")
                //.WithHash256("Ncn4kC3pHMfsvdfwSz1RrfJ2Ow2T9Ao1mmpFTuHEAh4=")
                //.WithHash256("rYyT5f4Pj/P23pPIA6YGWr/Oqz0Zt3DFihIX1DbOJ38=")
                //.WithHash256("oBFTyWUR8pji6AIv/Z4Dxq5rvbCAf88+YvElwUlpT2o=")
                //.WithHash256("sHOkVENLgOoxswROF9gq3L5SYgBiJwLsHfoCT1MllyA=")
                //.WithHash256("cSCi0/BoKuxU5ey+XafI0ARvUYgp4joz2hD4uanrT78=")
                //.WithHash256("x7qDFMGocoH4J5ikH3jAxpp+poqrmiD/n/EUzIziPnI=")
                //.WithHash256("oB8CznOA36AjMxKbkJ2eahjLajSRpPRK1mgzv6/MCDo=")
                //.WithHash256("lhbN2ffkCwt8GvAcigdJnZnV/LNMmLkBLnKczLRQWDI=")
                //.WithHash256("iinr+Ixdnc78yCAXH0e7obpDu2RObuh4ZKW0jL8ziHM=")
                //.WithHash256("Jf/QEz4Hou+P/x7xBRfgJ3cDcenvUb69eHpD7hgpDqQ=")
                //.WithHash256("coh3armyBiterIe2Mh+pk8rXurGmMFp5Lcd/ptz54F0=")
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
}
