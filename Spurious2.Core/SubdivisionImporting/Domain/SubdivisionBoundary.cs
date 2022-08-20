namespace Spurious2.Core.SubdivisionImporting.Domain;

public class SubdivisionBoundary
{
    public int Id { get; private set; }
    public string BoundaryText { get; private set; }
    public string Name { get; private set; }
    public string Province { get; private set; }

    public SubdivisionBoundary(int id, string boundaryText, string name, string province)
    {
        this.Id = id;
        this.BoundaryText = boundaryText;
        this.Name = name;
        this.Province = province;
    }
}
