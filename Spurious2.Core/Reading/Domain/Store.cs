namespace Spurious2.Core.Reading.Domain;

public partial class Store
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Location { get; set; } = string.Empty;
    public int BeerVolume { get; set; }
    public int WineVolume { get; set; }
    public int SpiritsVolume { get; set; }
}
