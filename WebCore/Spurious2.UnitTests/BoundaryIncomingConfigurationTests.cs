using FluentAssertions;
using Spurious2.Infrastructure.All;

namespace Spurious2.UnitTests;

[TestFixture]
public class BoundaryIncomingConfigurationTests
{
    [Test]
    public void ReadBoundariesSucceeds()
    {
        var boundaries = BoundaryIncomingConfiguration.ReadBoundariesIncoming().ToList();
        _ = boundaries.Count.Should().Be(577);
    }
}
