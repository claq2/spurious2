using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Spurious2.Core2.Properties;

namespace Spurious2.Core2.Subdivisions;

public class SubdivisionInMemImportingService : ISubdivisionInMemImportingService
{
    public List<Subdivision> ImportWithData()
    {
        using var reader = new StringReader(Resources.cachedsubdivs);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<SubdivMap>();

        var subdivs = csv.GetRecords<Subdivision>().ToList();
        return subdivs;
    }

    public class SubdivMap : ClassMap<Subdivision>
    {
        public SubdivMap()
        {
            this.Map(m => m.Id);
            this.Map(m => m.Population);
            this.Map(m => m.SubdivisionName);
            this.Map(m => m.AlcoholDensity);
            this.Map(m => m.BeerDensity);
            this.Map(m => m.WineDensity);
            this.Map(m => m.SpiritsDensity);
            this.Map(m => m.Province);
            this.Map(m => m.GeographicCentreGeog).Name("CentreWkt").TypeConverter<GeographyConverter>();
            this.Map(m => m.Boundary).Name("BoundaryWkt").TypeConverter<GeographyConverter>();
        }
    }
}
