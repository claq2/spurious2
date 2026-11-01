using CsvHelper;
using NetTopologySuite.Geometries;
using Spurious2.Core2.Properties;
//using Spurious2.Core2.Stores;

namespace Spurious2.Core2.Subdivisions;

public class SubdivisionInMemImportingService : ISubdivisionInMemImportingService
{
    public List<Subdivision> ImportWithData()
    {
        using var reader = new StringReader(Resources.cachedsubdivs);
        using var csv = CsvReader.Create(reader);
        //csv.Context.RegisterClassMap<SubdivMap>();

        var subdivs = csv.GetRecords<Subdivision>().ToList();
        return subdivs;
    }

    //public class SubdivMap : ClassMap<Subdivision>
    //{
    //    public SubdivMap()
    //    {
    //        this.Map(m => m.Id);
    //        this.Map(m => m.Population);
    //        this.Map(m => m.SubdivisionName);
    //        this.Map(m => m.AlcoholDensity);
    //        this.Map(m => m.BeerDensity);
    //        this.Map(m => m.WineDensity);
    //        this.Map(m => m.SpiritsDensity);
    //        this.Map(m => m.Province);
    //        this.Map(m => m.GeographicCentreGeog).Name("CentreWkt").TypeConverter<GeographyConverter>();
    //        this.Map(m => m.Boundary).Name("BoundaryWkt").TypeConverter<GeographyConverter>();
    //    }
    //}
}

[CsvRecordMap(typeof(Subdivision))]
public sealed partial class SubdivisionMap34
{
    [CsvColumn]
    public int Id { get; set; }
    [CsvColumn]
    public int Population { get; set; }
    [CsvColumn]
    public string? SubdivisionName { get; set; } = string.Empty;
    [CsvColumn]
    public decimal? AlcoholDensity { get; set; }
    [CsvColumn]
    public decimal? BeerDensity { get; set; }
    [CsvColumn]
    public decimal? WineDensity { get; set; }
    [CsvColumn]
    public decimal? SpiritsDensity { get; set; }
    [CsvColumn]
    public string? Province { get; set; } = string.Empty;
    [CsvColumn("CentreWkt")]
    [CsvConverter(typeof(GeographyConverter))]
    public Point GeographicCentreGeog { get; set; }
    [CsvColumn("BoundaryWkt")]
    [CsvConverter(typeof(GeographyConverter))]
    public Geometry Boundary { get; set; }
    [CsvIgnore]
    public GeoJSON.Text.Geometry.Point GeographicCentre { get; set; }
}
