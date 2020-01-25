using ServiceStack;
using System;
using System.Collections.Generic;

namespace Spurious2.Web.ServiceModel
{
    [Route("/densities")]
    public class Densities : IReturn<List<DensityInfo>>
    {

    }
}
