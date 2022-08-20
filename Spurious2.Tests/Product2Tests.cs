using NUnit.Framework;
using Spurious2.Core.LcboImporting.Domain;

namespace Spurious2.Tests;

[TestFixture]
public class Product2Tests
{
    [Test]
    public void PackageVolumeForBeer()
    {
        var product = new Product("Beer", 1, "6 x 341", "");
        Assert.That(product.PackageVolume, Is.EqualTo(6 * 341));
    }

    [Test]
    public void PackageVolumeForWine()
    {
        var product = new Product("Beer", 1, "1500", "");
        Assert.That(product.PackageVolume, Is.EqualTo(1500));
    }
}
