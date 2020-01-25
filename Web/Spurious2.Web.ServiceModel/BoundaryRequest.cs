using ServiceStack;
using System;

namespace Spurious2.Web.ServiceModel
{
    [Route("/subdivisions/{Id}/boundary")]
    public class BoundaryRequest : IReturn<string>
    {
        public int Id { get; set; }
    }
}
