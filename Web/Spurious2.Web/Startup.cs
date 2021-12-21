using Funq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Spurious2.Core.Reading.Domain;
using Spurious2.SqlOrmLite.Reading.Repositories;
using Spurious2.Web.ServiceInterface;
using System;

namespace Spurious2.Web
{
    public class Startup : ModularStartup
    {
        //public Startup(IConfiguration configuration)
        //    : base(configuration, typeof(MyServices).Assembly) { }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public new void ConfigureServices(IServiceCollection services)
        {
            NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
    NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
    new NetTopologySuite.Geometries.PrecisionModel(1000d),
    4326 /* ,
    // Note the following arguments are only valid for NTS v2.2
    // Geometry overlay operation function set to use (Legacy or NG)
    NetTopologySuite.Geometries.GeometryOverlay.NG,
    // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
    new NetTopologySuite.Geometries.CoordinateEqualityComparer() */);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration)
            });
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("Spurious2.Web", typeof(MyServices).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            SetConfig(new HostConfig
            {
                UseSameSiteCookies = true,
                AddRedirectParamsToQueryString = true,
                DebugMode = AppSettings.Get(nameof(HostConfig.DebugMode), HostingEnvironment.IsDevelopment()),
            });

            Plugins.Add(new SharpPagesFeature
            {
                EnableHotReload = Config.DebugMode
            }); // enable server-side rendering, see: https://sharpscript.net/docs/sharp-pages

            container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
                AppSettings.GetString("SpuriousSqlDb"),
                SqlServerDialect.Provider
                ));
            container.RegisterAs<StoreRepository, IStoreRepository>();
            container.RegisterAs<SubdivisionRepository, ISubdivisionRepository>();
        }
    }
}
