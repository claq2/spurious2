using Spurious2.Core2.Products;

namespace Spurious2.UnitTests;

[TestFixture]
public class ProductIncomingTests
{
    [Test]
    public void PackageVolumeForBeer()
    {
        var product = new ProductIncoming { Category = "Beer", Id = 1, Size = "6 x 341" };
        Assert.That(product.Volume, Is.EqualTo(6 * 341));
    }

    [Test]
    public void PackageVolumeForWine()
    {
        var product = new ProductIncoming { Category = "Wine", Id = 1, Size = "1500" };
        Assert.That(product.Volume, Is.EqualTo(1500));
    }
}
