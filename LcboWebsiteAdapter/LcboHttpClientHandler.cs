using System.Net;

namespace LcboWebsiteAdapter;

public class LcboHttpClientHandler : HttpClientHandler
{
    public LcboHttpClientHandler()
    {
        this.AutomaticDecompression = DecompressionMethods.GZip
                    | DecompressionMethods.Deflate
                    | DecompressionMethods.Brotli;
    }
}
