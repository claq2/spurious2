using System.Text;
using Azure.Storage.Blobs;
using Spurious2.Core2;

namespace Spurious2.Infrastructure.AzureStorage;

public static class BlobClientExtensions
{
    public static async Task UploadTextAsync(this BlobClient client, string text)
    {
        ArgumentNullException.ThrowIfNull(client);
        var byteContents = Encoding.UTF8.GetBytes(text);
        using var ms = new MemoryStream(byteContents);
        _ = await client.UploadAsync(ms, false).ConfigAwait();
    }
}
