using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Linq;

namespace Spurious2.Web.ServiceInterface
{
    [Exclude(Feature.Metadata)]
    [FallbackRoute("/{PathInfo*}", Matches = "AcceptsHtml")]
    public class FallbackForClientRoutes
    {
        public string PathInfo { get; set; }
    }
}
