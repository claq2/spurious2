using Ardalis.Specification;
using Carter;
using Microsoft.EntityFrameworkCore;
using Spurious2.Core;
using Spurious2.Core2.Densities;
using Spurious2.Infrastructure;
using Spurious2.Infrastructure.Models;

namespace Spurious2
{
    public class Program
    {
        public static void Main(string[] args)
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
            builder.Services.AddScoped(typeof(SpuriousRepository<>));
            builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(SpuriousRepository<>));
            builder.Services.AddScoped(typeof(IReadRepositoryBase<>), typeof(SpuriousRepository<>));
            builder.Services.AddScoped<ISpuriousRepository, SpuriousRepository2>();
            builder.Services.AddDbContext<SpuriousContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("SpuriousSqlDb"),
                    x => x.UseNetTopologySuite().EnableRetryOnFailure());
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddCarter();

            builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetDensitiesRequest>());

            var app = builder.Build();

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
}
