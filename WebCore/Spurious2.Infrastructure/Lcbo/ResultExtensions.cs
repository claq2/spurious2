
using System.Globalization;
using Spurious2.Core2.Lcbo;
using Spurious2.Core2.Products;

namespace Lcbo;

public static class ResultExtensions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>")]
    public static List<ProductIncoming> GetProducts(this Result[] results, ProductType productType)
    {
        var result = results
            .Where(p => !string.IsNullOrEmpty(p.raw.lcbo_unit_volume))
            .Select(p => new ProductIncoming
            {
                ProductName = p.title,
                Id = Convert.ToInt32(p.raw.permanentid, CultureInfo.InvariantCulture),
                Size = p.raw.lcbo_unit_volume,
                ProductPageUrl = new Uri(p.uri),
                Category = productType.ToString()
            }
            ).ToList();

        return result ?? [];
    }

    public static string GetLiquorType(this string categoryFilter)
    {
        var result = categoryFilter switch
        {
            string a when a.Contains("Products|Wine", StringComparison.Ordinal) => ProductType.Wine.ToString(),
            string a when a.Contains("Products|Beer & Cider", StringComparison.Ordinal) => ProductType.Beer.ToString(),
            string a when a.Contains("Products|Spirits", StringComparison.Ordinal) => ProductType.Spirits.ToString(),
            _ => ProductType.Beer.ToString(),
        };

        return result;
    }
}

