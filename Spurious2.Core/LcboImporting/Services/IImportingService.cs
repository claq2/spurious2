using Spurious2.Core.LcboImporting.Domain;

namespace Spurious2.Core.LcboImporting.Services;

public interface IImportingService
{
    Task ProcessStoreBlob(string storeId, Stream storeStream);
    Task SignalLastProductDone();
    Task StartImporting();
    Task ProcessInventoryBlob(string productId, Stream inventoryStream);
    Task ProcessProductBlob(string productId);
    Task ProcessLastProductBlob(string contents);
    Task ProcessLastInventoryBlob(string contents);
    Task EndImporting();
    Task GetProductPages(ProductType productType);
    Task UpdateAll();
}
