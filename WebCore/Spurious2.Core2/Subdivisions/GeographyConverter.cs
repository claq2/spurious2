using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Spurious2.Core2.Subdivisions;

public class GeographyConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        var rdr = new NetTopologySuite.IO.WKTReader();
        var geom = rdr.Read(text);
        return geom;
    }
}
