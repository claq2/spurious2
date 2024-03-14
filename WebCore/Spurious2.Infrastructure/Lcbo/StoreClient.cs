using Spurious2.Core2;

namespace Lcbo;

public class StoreClient
{
    private readonly HttpClient httpClient;

    public StoreClient(HttpClient httpClient)
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

    public async Task<string> GetStorePage(Uri storeUri)
    {
        var result = await this.httpClient.GetStringAsync(storeUri).ConfigAwait();
        return result;
    }
}
