using System.Net;

namespace Lcbo;

public class LcboHttpClientHandler : HttpClientHandler
{
    public LcboHttpClientHandler() => this.AutomaticDecompression = DecompressionMethods.GZip
                    | DecompressionMethods.Deflate
                    | DecompressionMethods.Brotli;
}
