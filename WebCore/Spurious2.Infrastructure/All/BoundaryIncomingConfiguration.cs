using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure.All;

[SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public class BoundaryIncomingConfiguration : IEntityTypeConfiguration<BoundaryIncoming>
{
    public void Configure(EntityTypeBuilder<BoundaryIncoming> builder)
    {
        builder.HasKey(e => e.Id);
        builder.ToTable("BoundaryIncoming");

        builder.Property(e => e.Province).HasMaxLength(255);
        builder.Property(e => e.SubdivisionName).HasMaxLength(255);

        // builder.HasData(ReadBoundariesIncoming());
    }

    public static IEnumerable<BoundaryIncoming> ReadBoundariesIncoming()
    {
        using var context = new SpuriousContext();
        using var repo = new SpuriousRepository(context);
        using var importingService = new Core.SubdivisionImporting.Services.SubdivisionImportingService(repo);
        return importingService.ImportBoundaryFromCsvFile("subdiv.csv");
    }
}
