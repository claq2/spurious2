using CsvHelper;
using NetTopologySuite.Geometries;
using Spurious2.Core2.Subdivisions;
namespace ImportTester;

/*
 *  var csduidColumnIndex = csv.GetFieldIndex("CSDUID");
            var wktColumnIndex = csv.GetFieldIndex("WKT");
            var csdnameColumnIndex = csv.GetFieldIndex("CSDNAME");
            var pruidColumnIndex = csv.GetFieldIndex("PRUID");
 */
[CsvRecordMap(typeof(BoundaryIncoming))]
public partial class BoundaryIncomingCsvMap
{
    [CsvColumn("CSDUID")]
    public int Id { get; set; }

    [CsvColumn("WKT")]
    public string? BoundaryWellKnownText { get; set; }

    [CsvColumn("CSDNAME")]
    public string SubdivisionName { get; set; } = null!;

    [CsvIgnore]
    public string? Province { get; set; }

    [CsvIgnore]
    public Geometry? OriginalBoundary { get; set; }

    [CsvIgnore]
    public Geometry? ReorientedBoundary { get; set; }
}
