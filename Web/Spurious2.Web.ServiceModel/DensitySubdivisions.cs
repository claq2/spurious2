namespace Spurious2.Web.ServiceModel;

[Route("/densities/{Name}/subdivisions")]
public class DensitySubdivisions : IReturn<List<Subdivision>>
{
    public string Name { get; set; }
}
