namespace Spurious2.Core2.Subdivisions;

public class PopulationIncoming
{
    public int Id { get; set; }

    public int Population { get; set; }

    public string? SubdivisionName { get; set; }
    public string Province { get; set; }
    public string GeoName { get; set; }
    public string GeoLevel { get; set; }
    public int CharacteristicId { get; set; }
    public string CharacteristicName { get; set; }
}
