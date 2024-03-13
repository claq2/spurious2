namespace Spurious2.Core2.Lcbo;

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