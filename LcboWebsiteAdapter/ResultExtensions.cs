using Spurious2.Core.LcboImporting.Domain;

namespace LcboWebsiteAdapter;

public static class ResultExtensions
{
    public static List<Product> GetProducts(this Result[] results, ProductType productType)
    {
        var result = results
            .Where(p => !string.IsNullOrEmpty(p.raw.lcbo_unit_volume))
            .Select(p => new Product(p.title,
                Convert.ToInt32(p.raw.permanentid),
                p.raw.lcbo_unit_volume,
                p.uri)
            { LiquorType = productType.ToString() }
            ).ToList();

        return result;
    }

    public static string GetLiquorType(this string categoryFilter)
    {
        var result = categoryFilter switch
        {
            string a when a.Contains("Products|Wine") => ProductType.Wine.ToString(),
            string a when a.Contains("Products|Beer & Cider") => ProductType.Beer.ToString(),
            string a when a.Contains("Products|Spirits") => ProductType.Spirits.ToString(),
            _ => ProductType.Beer.ToString(),
        };

        return result;
    }
}

