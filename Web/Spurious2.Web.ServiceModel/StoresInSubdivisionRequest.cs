namespace Spurious2.Web.ServiceModel;

[Route("/subdivisions/{Id}/stores")]
public class StoresInSubdivisionRequest : IReturn<List<Store>>
{
    public int Id { get; set; }
}
