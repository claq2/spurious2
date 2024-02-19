using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spurious2.Core2.Stores;

namespace Spurious2.Infrastructure.All;

[SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.HasKey(e => e.Id);

        builder.ToTable("Store");

        builder.Property(e => e.Id).ValueGeneratedNever();
        builder.Property(e => e.City).HasColumnType("text");
        builder.Property(e => e.StoreName).HasColumnType("text");
        builder.Property(e => e.LocationGeog).HasColumnName("Location")
            .HasColumnType("geography");
        builder.Ignore(e => e.Location);

        builder.HasData(ReadStores());
    }

    public static IEnumerable<Store> ReadStores()
    {
        var lines = File.ReadLines("stores.csv");
        foreach (var line in lines)
        {
            var elements = line.Split(',');
            // POINT (-79.531037 43.712679)
            var pointValues = elements[6].Split(' ');
            var pointX = Convert.ToDouble(pointValues[1][1..], CultureInfo.InvariantCulture);
            var pointY = Convert.ToDouble(pointValues[2][..^1], CultureInfo.InvariantCulture);
            yield return new Store
            {
                Id = Convert.ToInt32(elements[0], CultureInfo.InvariantCulture),
                StoreName = elements[1],
                City = elements[2],
                BeerVolume = elements[3] != "NULL" ? Convert.ToInt32(elements[3], CultureInfo.InvariantCulture) : 0,
                WineVolume = elements[4] != "NULL" ? Convert.ToInt32(elements[4], CultureInfo.InvariantCulture) : 0,
                SpiritsVolume = elements[5] != "NULL" ? Convert.ToInt32(elements[5], CultureInfo.InvariantCulture) : 0,
                LocationGeog = new NetTopologySuite.Geometries.Point(pointX, pointY)
            };
        }
    }
}
