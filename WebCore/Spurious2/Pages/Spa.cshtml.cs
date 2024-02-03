using Microsoft.AspNetCore.Mvc.RazorPages;
using NetTopologySuite.IO.Converters;
using Spurious2.Infrastructure.Models;
using System.Text;
using System.Text.Json;

namespace Spurious2.Pages
{
    public class SpaModel(SpuriousContext context) : PageModel
    {
        static readonly JsonSerializerOptions jsonOptions;
        static SpaModel()
        {
            jsonOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };
            jsonOptions.Converters.Add(new GeoJsonConverterFactory());

        }
        public void OnGet()
        {
            var subDivs = context.Subdivisions
                .Where(s => s.Id == 1001101)
                //.Select(s => s.GeographicCentre.AsText())
                .ToList();

            using (var memStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memStream))
                {
                    JsonSerializer.Serialize(writer, subDivs[0].GeographicCentre, jsonOptions);
                }

                var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
            }

            using (var memStream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(memStream))
                {
                    JsonSerializer.Serialize(writer, subDivs[0].Boundary, jsonOptions);
                }

                var boundryJson = Encoding.UTF8.GetString(memStream.ToArray());
            }

            var subDivs2 = context.Subdivisions
                .Where(s => s.Id == 1001101)
                .Select(s => s.Boundary.AsText())
                .ToList();

            var stores = context.Stores
                .Where(s => s.Id == 1)
                .Select(s => s.Location.AsText())
                .ToList();
        }
    }
}
