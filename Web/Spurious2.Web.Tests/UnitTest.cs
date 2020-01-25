using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using Spurious2.Web.ServiceInterface;
using Spurious2.Web.ServiceModel;

namespace Spurious2.Web.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<MyServices>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

    
    }
}
