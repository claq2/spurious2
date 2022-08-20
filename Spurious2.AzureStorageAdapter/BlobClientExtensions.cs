using Azure.Storage.Blobs;
using Spurious2.Core;
using System.Text;

namespace Spurious2.AzureStorageAdapter;

public static class BlobClientExtensions
{
    public static async Task UploadTextAsync(this BlobClient client, string text)
    {
        var byteContents = Encoding.UTF8.GetBytes(text);
        using var ms = new MemoryStream(byteContents);
        await client.UploadAsync(ms, false).ConfigAwait();
    }
}
