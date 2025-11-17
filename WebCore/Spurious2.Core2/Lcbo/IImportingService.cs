namespace Spurious2.Core2.Lcbo;

public interface IImportingService
{
    public Task ProcessStoreBlob(string storeId);
    public Task ProcessInventoryBlob(string productId);
    public Task ProcessStoreBlob(string storeId, Stream storeStream);
    public Task SignalLastProductDone();
    public Task StartImporting();
    public Task ProcessInventoryBlob(string productId, Stream inventoryStream);
    public Task ProcessProductBlob(string productId);
    public Task ProcessLastProductBlob(string contents);
    public Task ProcessLastInventoryBlob(string contents);
    public Task EndImporting();
    public Task GetProductPages(ProductType productType);
    public Task UpdateAll();
}
