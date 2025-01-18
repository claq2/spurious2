using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure;

public class BoundaryIncomingConfiguration : IEntityTypeConfiguration<BoundaryIncoming>
{
    public void Configure(EntityTypeBuilder<BoundaryIncoming> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.ToTable("BoundaryIncoming");

        builder.Property(e => e.Province).HasMaxLength(255);
        builder.Property(e => e.SubdivisionName).HasMaxLength(255);
    }
}
