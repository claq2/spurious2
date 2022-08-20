namespace Spurious2.Core.LcboImporting.Domain;

public interface IProductRepository : IDisposable
{
    Task UpdateProductsFromIncoming();
    Task ImportAFew(IEnumerable<Product> products);
    Task ClearIncomingProducts();
    Task MarkIncomingProductDone(string productId);
}
