namespace Spurious2.Core.Reading.Domain;

public class Subdivision
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Population { get; set; }
    public decimal Density { get; set; }
    public string Centre { get; set; } = string.Empty;
    public long Volume { get; set; }
}
