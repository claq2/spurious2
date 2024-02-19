using FluentAssertions;

namespace Spurious2.UnitTests;

[TestFixture]
public class StoreConfigurationTests
{
    [Test]
    public void ReadStoresSucceeds()
    {
        var stores = Infrastructure.All.StoreConfiguration.ReadStores().ToList();
        stores.Count.Should().Be(653);
    }
}
