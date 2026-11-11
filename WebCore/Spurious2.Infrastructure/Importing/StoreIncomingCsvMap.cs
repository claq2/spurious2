using CsvHelper;
using NetTopologySuite.Geometries;
using Spurious2.Core2.Stores;

namespace Spurious2.Infrastructure.Importing;

[CsvRecordMap(typeof(StoreIncoming))]
public partial class StoreIncomingCsvMap
{
    [CsvColumn]
    public int Id { get; set; }
    [CsvColumn]
    public string? City { get; set; }
    [CsvColumn]
    public string? StoreName { get; set; }
    [CsvIgnore]
    public decimal? Latitude { get; set; }
    [CsvIgnore]
    public decimal? Longitude { get; set; }
    [CsvIgnore]
    public bool StoreDone { get; set; }
    [CsvColumn("WKT")]
    public string? LocationWellKnownText { get; set; }
    [CsvColumn]
    public int? BeerVolume { get; set; }
    [CsvColumn]
    public int? WineVolume { get; set; }
    [CsvColumn]
    public int? SpiritsVolume { get; set; }
    [CsvIgnore]
    public Point? LocationGeog { get; set; } = Point.Empty;
}
