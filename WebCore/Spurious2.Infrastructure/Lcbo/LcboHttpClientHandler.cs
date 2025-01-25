using System.Net;

namespace Spurious2.Infrastructure.Lcbo;

public class LcboHttpClientHandler : HttpClientHandler
{
    public LcboHttpClientHandler() => this.AutomaticDecompression = DecompressionMethods.GZip
                    | DecompressionMethods.Deflate
                    | DecompressionMethods.Brotli;
}
