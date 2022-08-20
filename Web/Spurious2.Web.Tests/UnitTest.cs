using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using Spurious2.Web.ServiceInterface;

namespace Spurious2.Web.Tests;

public class UnitTest
{
    private readonly ServiceStackHost appHost;

    public UnitTest()
    {
        this.appHost = new BasicAppHost().Init();
        this.appHost.Container.AddTransient<MyServices>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        this.appHost.Dispose();
    }
}
