using CsvHelper;
using Spurious2.Core.SubdivisionImporting.Domain;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Spurious2.Core.SubdivisionImporting.Services;

public class ImportingService : IImportingService
{
    private readonly ISubdivisionRepository subdivisionRepository;

    public ImportingService(ISubdivisionRepository subdivisionRepository)
    {
        this.subdivisionRepository = subdivisionRepository;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.subdivisionRepository?.Dispose();
        }
    }

    public int ImportBoundaryFromGmlFile(string filenameAndPath)
    {
        // Massive mem use here somewhere - about 500 MB. Lots of strings.
        var gmlDoc = new XmlDocument();
        gmlDoc.Load(filenameAndPath);
        var ns = new XmlNamespaceManager(gmlDoc.NameTable);
        ns.AddNamespace("gml", "http://www.opengis.net/gml");
        ns.AddNamespace("fme", "http://www.safe.com/gml/fme");
        var nodes = gmlDoc.SelectNodes("/gml:FeatureCollection/gml:featureMember", ns);

        List<SubdivisionBoundary> boundaries = new();
        if (nodes != null)
        {
            foreach (XmlNode node in nodes)
            {
                var surfaceNode = node.SelectSingleNode("fme:lcsd000a16g_e/gml:surfaceProperty/gml:Surface", ns);
                var multiSurfaceNode = node.SelectSingleNode("fme:lcsd000a16g_e/gml:multiSurfaceProperty/gml:MultiSurface", ns);
                var province = node.SelectSingleNode("fme:lcsd000a16g_e/fme:PRNAME", ns)?.InnerText ?? string.Empty;
                var gmlNode = surfaceNode ?? multiSurfaceNode;
                var gmlText = gmlNode?.OuterXml ?? string.Empty;
                boundaries.Add(
                    new SubdivisionBoundary(
                        Convert.ToInt32(node.SelectSingleNode("fme:lcsd000a16g_e/fme:CSDUID", ns)?.InnerText ?? "0",
                        CultureInfo.InvariantCulture),
                    gmlText,
                    node.SelectSingleNode("fme:lcsd000a16g_e/fme:CSDNAME", ns)?.InnerText ?? string.Empty,
                    province));
            }
        }

        this.subdivisionRepository.Import(boundaries);

        return nodes?.Count ?? 0;
    }

    public int ImportBoundaryFromCsvFile(string filenameAndPath)
    {
        using (var reader = new StreamReader(filenameAndPath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var boundaries = new List<SubdivisionBoundary>();

            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                if (csv.TryGetField<int>("CSDUID", out var csduid)
                    && csv.TryGetField<string>("WKT", out var wkt)
                    && csv.TryGetField<string>("CSDNAME", out var csdname)
                    && csv.TryGetField<string>("PRNAME", out var prnname))
                {
                    boundaries.Add(new SubdivisionBoundary
                    (
                        csduid,
                        wkt ?? string.Empty,
                        csdname ?? string.Empty,
                        prnname ?? string.Empty
                    ));
                }
            }

            this.subdivisionRepository.Import(boundaries);
            return boundaries.Count;
        }
    }

    public int ImportPopulationFromFile(string filenameAndPath)
    {
        using (var reader = new StreamReader(filenameAndPath, Encoding.GetEncoding(1252)))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = new List<SubdivisionPopulation>();

            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                if (csv.TryGetField<int>("Geographic code", out var id)
                    && csv.TryGetField<string>("Geographic name, english", out var name))
                {
                    var populationString = csv.GetField("Population, 2016");
                    var population = !string.IsNullOrEmpty(populationString) ? Convert.ToInt32(populationString, CultureInfo.InvariantCulture) : 0;
                    records.Add(new SubdivisionPopulation
                    {
                        Id = id,
                        Name = name ?? string.Empty,
                        Population = population,
                    });
                }
            }

            this.subdivisionRepository.Import(records);
            return records.Count;
        }
    }
}
