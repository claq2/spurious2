﻿using CsvHelper;
using Spurious2.Core.SubdivisionImporting.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Spurious2.Core.SubdivisionImporting.Services
{
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

            var boundaries = new List<SubdivisionBoundary>();
            foreach (XmlNode node in nodes)
            {
                var surfaceNode = node.SelectSingleNode("fme:lcsd000a16g_e/gml:surfaceProperty/gml:Surface", ns);
                var multiSurfaceNode = node.SelectSingleNode("fme:lcsd000a16g_e/gml:multiSurfaceProperty/gml:MultiSurface", ns);
                var province = node.SelectSingleNode("fme:lcsd000a16g_e/fme:PRNAME", ns).InnerText;
                var gmlNode = surfaceNode ?? multiSurfaceNode;
                var gmlText = gmlNode.OuterXml;
                boundaries.Add(new SubdivisionBoundary(Convert.ToInt32(node.SelectSingleNode("fme:lcsd000a16g_e/fme:CSDUID", ns).InnerText, CultureInfo.InvariantCulture),
                    gmlText,
                    node.SelectSingleNode("fme:lcsd000a16g_e/fme:CSDNAME", ns).InnerText,
                    province));
            }

            this.subdivisionRepository.Import(boundaries);

            return nodes.Count;
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
                    if (csv.Context.Record.Length >= 1)
                    {
                        boundaries.Add(new SubdivisionBoundary
                        (
                            csv.GetField<int>("CSDUID"),
                            csv.GetField("WKT"),
                            csv.GetField("CSDNAME"),
                            csv.GetField("PRNAME")
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
                    if (csv.Context.Record.Length >= 10)
                    {
                        var populationString = csv.GetField("Population, 2016");
                        var population = !string.IsNullOrEmpty(populationString) ? Convert.ToInt32(populationString, CultureInfo.InvariantCulture) : 0;
                        records.Add(new SubdivisionPopulation
                        {
                            Id = csv.GetField<int>("Geographic code"),
                            Name = csv.GetField("Geographic name, english"),
                            Population = population,
                        });
                    }
                }

                this.subdivisionRepository.Import(records);
                return records.Count;
            }
        }
    }
}
