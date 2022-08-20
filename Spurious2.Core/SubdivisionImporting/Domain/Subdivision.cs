namespace Spurious2.Core.SubdivisionImporting.Domain;

public partial class Subdivision
{
    public int Id { get; set; }
    public int Population { get; set; }
    public long BeerVolume { get; set; }
    public long WineVolume { get; set; }
    public long SpiritsVolume { get; set; }
    public string Province { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int AverageIncome { get; set; }
    public int MedianIncome { get; set; }
    public int MedianAfterTaxIncome { get; set; }
    public int AverageAfterTaxIncome { get; set; }
}
