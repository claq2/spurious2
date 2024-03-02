using System.Diagnostics.CodeAnalysis;
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
        builder.Property(e => e.City).HasColumnType("nvarchar(255)");
        builder.Property(e => e.StoreName).HasColumnType("nvarchar(255)");
        builder.Property(e => e.LocationGeog).HasColumnName("Location")
            .HasColumnType("geography");
        builder.Ignore(e => e.Location);
    }
}
