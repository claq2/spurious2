using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core.Reading.Domain;
using Spurious2.SqlOrmLite.Reading.Repositories;
using Spurious2.Web.ServiceInterface;

[assembly: HostingStartup(typeof(Spurious2.Web.AppHost))]

namespace Spurious2.Web;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
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
        });

    public AppHost() : base("Spurious2.Web", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        // enable server-side rendering, see: https://sharpscript.net/docs/sharp-pages
        Plugins.Add(new SharpPagesFeature {
            EnableSpaFallback = true
        });

        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });

        container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
           AppSettings.GetString("SpuriousSqlDb"),
           SqlServerDialect.Provider
           ));
        container.RegisterAs<StoreRepository, IStoreRepository>();
        container.RegisterAs<SubdivisionRepository, ISubdivisionRepository>();
    }
}
