using Funq;
using NUnit.Framework;
using ServiceStack;
using Spurious2.Web.ServiceInterface;

namespace Spurious2.Web.Tests;

public class IntegrationTest
{
    private const string BaseUri = "http://localhost:2000/";
    private readonly ServiceStackHost appHost;

    private class AppHost : AppSelfHostBase
    {
        public AppHost() : base(nameof(IntegrationTest), typeof(MyServices).Assembly) { }

        public override void Configure(Container container)
        {
        }
    }

    public IntegrationTest()
    {
        this.appHost = new AppHost()
            .Init()
            .Start(BaseUri);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => this.appHost.Dispose();

    public static IServiceClient CreateClient() => new JsonServiceClient(BaseUri);
}
