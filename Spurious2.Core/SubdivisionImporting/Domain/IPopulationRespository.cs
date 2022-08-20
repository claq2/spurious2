namespace Spurious2.Core.SubdivisionImporting.Domain;

public interface IPopulationRespository : IDisposable
{
    void Import(IEnumerable<SubdivisionPopulation> populations);
}
