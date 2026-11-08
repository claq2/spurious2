//using CsvHelper;
//using NetTopologySuite.Geometries;
//using Spurious2.Core2.Stores;
//namespace ImportTester;

//[CsvRecordMap(typeof(StoreIncoming))]
//public partial class StoreIncomingCsvMap
//{
//    [CsvColumn]
//    public int Id { get; set; }
//    [CsvColumn]
//    public string? City { get; set; }
//    [CsvColumn]
//    public string? StoreName { get; set; }
//    [CsvIgnore]
//    public decimal? Latitude { get; set; }
//    [CsvIgnore]
//    public decimal? Longitude { get; set; }
//    [CsvIgnore]
//    public bool StoreDone { get; set; }
//    [CsvIgnore]
//    public string? LocationWellKnownText { get; set; }
//    [CsvColumn]
//    public int? BeerVolume { get; set; }
//    [CsvColumn]
//    public int? WineVolume { get; set; }
//    [CsvColumn]
//    public int? SpiritsVolume { get; set; }
//    [CsvColumn("WKT")]
//    [CsvConverter(typeof(GeographyConverter))]
//    public Point? LocationGeog { get; set; } = Point.Empty;
//}
