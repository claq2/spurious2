//using CsvHelper;
//using NetTopologySuite.Geometries;
//using Spurious2.Core2.Subdivisions;
//namespace ImportTester;

///*
// *  var altGeoCodeColumnIndex = provinceCsv.GetFieldIndex("ALT_GEO_CODE");
//                var geoNameColumnIndex = provinceCsv.GetFieldIndex("GEO_NAME");
//                var geoLevelColumnIndex = provinceCsv.GetFieldIndex("GEO_LEVEL");
// */
//[CsvRecordMap(typeof(PopulationIncoming))]
//public partial class PopulationIncomingCsvMap
//{
//    [CsvColumn("ALT_GEO_CODE")]
//    public int Id { get; set; }

//    [CsvColumn("C1_COUNT_TOTAL")]
//    [CsvConverter(typeof(EmptyToZeroIntConverter))]
//    public int Population { get; set; }

//    [CsvIgnore]
//    public string? SubdivisionName { get; set; }
//    [CsvColumn("GEO_NAME")]
//    public string? GeoName { get; set; }
//    [CsvIgnore]
//    public string Province { get; set; }
//    [CsvColumn("GEO_LEVEL")]
//    public string GeoLevel { get; set; }
//    [CsvColumn("CHARACTERISTIC_ID")]
//    public int CharacteristicId { get; set; }
//    [CsvColumn("CHARACTERISTIC_NAME")]
//    public string CharacteristicName { get; set; }
//}
