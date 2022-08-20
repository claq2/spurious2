namespace Spurious2.Core.Reading.Domain;

public interface ISubdivisionRepository
{
    Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType, EndOfDistribution endOfDistribution, int limit);
    Task<string> GetBoundaryForSubdivision(int id);
}
