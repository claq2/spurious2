using System.Globalization;
using System.Text;
using CsvHelper;
using Spurious2.Core.Populations;
using Spurious2.Core2;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Core.SubdivisionImporting.Services;

public class SubdivisionImportingService(ISpuriousRepository spuriousRepository) : ISubdivisionImportingService
{
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            spuriousRepository?.Dispose();
        }
    }

    //public int ImportBoundaryFromGmlFile(string filenameAndPath)
    //{
    //    // Massive mem use here somewhere - about 500 MB. Lots of strings.
    //    var gmlDoc = new XmlDocument();
    //    gmlDoc.Load(filenameAndPath);
    //    var ns = new XmlNamespaceManager(gmlDoc.NameTable);
    //    ns.AddNamespace("gml", "http://www.opengis.net/gml");
    //    ns.AddNamespace("fme", "http://www.safe.com/gml/fme");
    //    var nodes = gmlDoc.SelectNodes("/gml:FeatureCollection/gml:featureMember", ns);

    //    List<SubdivisionBoundary> boundaries = [];
    //    if (nodes != null)
    //    {
    //        foreach (XmlNode node in nodes)
    //        {
    //            var surfaceNode = node.SelectSingleNode("fme:lcsd000a16g_e/gml:surfaceProperty/gml:Surface", ns);
    //            var multiSurfaceNode = node.SelectSingleNode("fme:lcsd000a16g_e/gml:multiSurfaceProperty/gml:MultiSurface", ns);
    //            var province = node.SelectSingleNode("fme:lcsd000a16g_e/fme:PRNAME", ns)?.InnerText ?? string.Empty;
    //            var gmlNode = surfaceNode ?? multiSurfaceNode;
    //            var gmlText = gmlNode?.OuterXml ?? string.Empty;
    //            boundaries.Add(
    //                new SubdivisionBoundary(
    //                    Convert.ToInt32(node.SelectSingleNode("fme:lcsd000a16g_e/fme:CSDUID", ns)?.InnerText ?? "0",
    //                    CultureInfo.InvariantCulture),
    //                gmlText,
    //                node.SelectSingleNode("fme:lcsd000a16g_e/fme:CSDNAME", ns)?.InnerText ?? string.Empty,
    //                province));
    //        }
    //    }

    //    subdivisionRepository.Import(boundaries);

    //    return nodes?.Count ?? 0;
    //}

    public IEnumerable<BoundaryIncoming> ImportBoundaryFromCsvFile(string filenameAndPath)
    {
        using var reader = new StreamReader(filenameAndPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var boundaries = new List<BoundaryIncoming>();

        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            if (csv.TryGetField<int>("CSDUID", out var csduid)
                && csv.TryGetField<string>("WKT", out var wkt)
                && csv.TryGetField<string>("CSDNAME", out var csdname)
                && csv.TryGetField<int>("PRUID", out var prid)
                )
            {
                //if (prid == 35)
                //{
                //    yield return new BoundaryIncoming
                //    {
                //        Id = csduid,
                //        BoundaryWellKnownText = wkt,
                //        SubdivisionName = csdname,
                //        Province = "Ontario",
                //    };
                //}

                boundaries.Add(new BoundaryIncoming
                {
                    Id = csduid,
                    BoundaryWellKnownText = wkt,
                    SubdivisionName = csdname,
                    Province = string.Empty,
                });
            }

            spuriousRepository.ImportBoundaries(boundaries);
        }

        return boundaries;
    }

    //public int ImportPopulationFromFile(string filenameAndPath)
    //{
    //    using (var reader = new StreamReader(filenameAndPath, Encoding.GetEncoding(1252)))
    //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    //    {
    //        var records = new List<SubdivisionPopulation>();

    //        csv.Read();
    //        csv.ReadHeader();
    //        while (csv.Read())
    //        {
    //            if (csv.TryGetField<int>("Geographic code", out var id)
    //                && csv.TryGetField<string>("Geographic name, english", out var name)
    //                )
    //            {
    //                var populationString = csv.GetField("Population, 2016");
    //                var population = !string.IsNullOrEmpty(populationString) ? Convert.ToInt32(populationString, CultureInfo.InvariantCulture) : 0;
    //                records.Add(new SubdivisionPopulation
    //                {
    //                    Id = id,
    //                    Name = name,
    //                    Population = population,
    //                });
    //            }
    //        }

    //        subdivisionRepository.Import(records);
    //        return records.Count;
    //    }
    //}

    public IEnumerable<PopulationIncoming> ImportPopulationFrom98File(string filenameAndPath)
    {
        // First pass is to extract province/territory names and IDs
        var provincesDict = new Dictionary<int, string>();
        using var provinceReader = new StreamReader(filenameAndPath, Encoding.UTF8);
        using var provinceCsv = new CsvReader(provinceReader, CultureInfo.InvariantCulture);
        {

            _ = provinceCsv.Read();
            _ = provinceCsv.ReadHeader();
            while (provinceCsv.Read())
            {
                if (provinceCsv.TryGetField<int>("ALT_GEO_CODE", out var id)
                && provinceCsv.TryGetField<string>("GEO_NAME", out var name)
                && provinceCsv.TryGetField<string>("GEO_LEVEL", out var level)

                    )
                {
                    if (string.Equals(level, "Province", StringComparison.Ordinal)
                        || string.Equals(level, "Territory", StringComparison.Ordinal)
                    // && string.Compare(charName, "Population, 2021", StringComparison.Ordinal) == 0
                    )
                    {
                        provincesDict[id] = name;
                    }
                }
            }
        }

        // Second pass is to read populations
        using var reader = new StreamReader(filenameAndPath, Encoding.UTF8);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = new List<PopulationIncoming>();

        _ = csv.Read();
        _ = csv.ReadHeader();
        while (csv.Read())
        {
            if (csv.TryGetField<int>("ALT_GEO_CODE", out var id)
                && csv.TryGetField<string>("GEO_NAME", out var name)
                && csv.TryGetField<string>("GEO_LEVEL", out var level)
                && csv.TryGetField<int>("CHARACTERISTIC_ID", out var charId)
                && csv.TryGetField<string>("CHARACTERISTIC_NAME", out var charName)
                )
            {
                if (string.Equals(level, "Census subdivision", StringComparison.Ordinal)
                    // && string.Compare(charName, "Population, 2021", StringComparison.Ordinal) == 0
                    && charId == 1 // Population, 2021
                    )
                {
                    var provinceId = Convert.ToInt32(id.ToString(CultureInfo.InvariantCulture)[..2], CultureInfo.InvariantCulture);
                    var populationString = csv.GetField("C1_COUNT_TOTAL");
                    var population = !string.IsNullOrEmpty(populationString) ? Convert.ToInt32(populationString, CultureInfo.InvariantCulture) : 0;
                    //if (provincesDict[provinceId] == "Ontario")
                    //{
                    records.Add(new PopulationIncoming
                    {
                        Id = id,
                        // TODO: Don't send this subdiv name
                        SubdivisionName = name, // Name values in 98 file are bad because they have "Town", "City" e.g. Mount Carmel-Mitchells Brook-St. Catherine's, Town (T)
                        Population = population,
                        Province = provincesDict[provinceId]
                    });
                    //}
                }
            }
        }

        spuriousRepository.ImportPopulations(records);
        return records;
    }
}
