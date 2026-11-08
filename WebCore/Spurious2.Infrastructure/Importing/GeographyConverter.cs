using CsvHelper;
using NetTopologySuite.Geometries;

namespace Spurious2.Infrastructure.Importing;

public class GeographyConverter : ICsvConverter<Point>, ICsvConverter<Geometry>
{
    public Point Read(CsvReader reader, int index)
    {
        ArgumentNullException.ThrowIfNull(reader, nameof(reader));
        var rdr = new NetTopologySuite.IO.WKTReader();
        var geom = rdr.Read(reader.GetString(index).Trim());
        return (Point)geom;
    }

    public void Write(CsvWriter writer, Point value) => throw new NotImplementedException();
    public void Write(CsvWriter writer, Geometry value) => throw new NotImplementedException();
    Geometry ICsvConverter<Geometry>.Read(CsvReader reader, int index)
    {
        ArgumentNullException.ThrowIfNull(reader, nameof(reader));
        var rdr = new NetTopologySuite.IO.WKTReader();
        var geom = rdr.Read(reader.GetString(index).Trim());
        return geom;
    }
}
