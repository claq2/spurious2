using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace Spurious2.Core2.Subdivisions;

public class SubdivisionInMemImportingService : ISubdivisionInMemImportingService
{
    public List<Subdivision> ImportWithData()
    {
        using var reader = new StreamReader("cachedsubdivs.csv", Encoding.UTF8);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<SubdivMap>();

        var subdivs = csv.GetRecords<Subdivision>().ToList();
        return subdivs;
    }

    public class SubdivMap : ClassMap<Subdivision>
    {
        public SubdivMap()
        {
            Map(m => m.Id);
            Map(m => m.Population);
            Map(m => m.SubdivisionName);
            Map(m => m.AlcoholDensity);
            Map(m => m.BeerDensity);
            Map(m => m.WineDensity);
            Map(m => m.SpiritsDensity);
            Map(m => m.Province);
            Map(m => m.GeographicCentreGeog).Name("CentreWkt").TypeConverter<GeographyConverter>();
            Map(m => m.Boundary).Name("BoundaryWkt").TypeConverter<GeographyConverter>();
            //Map(m => m.GeographicCentre).Index(15);
            //Map(m => m.Boundary).Index(16);
        }
    }
}
