namespace Spurious2.Core.SubdivisionImporting.Domain;

public class SubdivisionPopulation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Population { get; set; }

    public SubdivisionPopulation()
    {
    }

    public override string ToString()
    {
        return $"{this.Name} Pop: {this.Population}";
    }
}
