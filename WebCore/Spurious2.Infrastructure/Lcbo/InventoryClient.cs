using System.Globalization;
using System.Text;
using Spurious2.Core2;

namespace Spurious2.Infrastructure.Lcbo;

public class InventoryClient
{
    private static readonly CompositeFormat InventoryUrlFormat = CompositeFormat.Parse("https://www.lcbo.com/en/storeinventory/?sku={0}");
    private readonly HttpClient httpClient;

    public InventoryClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;

        var headers = new Dictionary<string, string>
        {
            { "Accept", "*/*" },
            { "Accept-Language", "en-US,en;q=0.5" },
            { "Authorization", "Bearer xx883b5583-07fb-416b-874b-77cce565d927" },
            { "DNT", "1" },
            { "Connection", " keep-alive" },
            { "Sec-Fetch-Dest", "empty" },
            { "Sec-Fetch-Mode", "cors" },
            { "Sec-Fetch-Site", "cross-site" },
            { "Sec-GPC", "1" },
        };

        foreach (var pair in headers)
        {
            this.httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
        }

        _ = this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
        _ = this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.137 Safari/537.36");
    }

    public async Task<string> GetInventoryPage(string productId)
    {
        var inventoryUrl = new Uri(string.Format(CultureInfo.InvariantCulture, InventoryUrlFormat, productId));
        var result = await this.httpClient.GetStringAsync(inventoryUrl).ConfigAwait();
        return result;
    }
}
