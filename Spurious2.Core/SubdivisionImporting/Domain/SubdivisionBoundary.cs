namespace Spurious2.Core.SubdivisionImporting.Domain;

public class SubdivisionBoundary(int id, string boundaryText, string name, string province)
{
    public int Id { get; private set; } = id;
    public string BoundaryText { get; private set; } = boundaryText;
    public string Name { get; private set; } = name;
    public string Province { get; private set; } = province;
}
