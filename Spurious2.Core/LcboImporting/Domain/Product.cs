using System.Globalization;

namespace Spurious2.Core.LcboImporting.Domain;

public class Product(string name, int id, string size, string url)
{
    public string Name { get; private set; } = name;
    public int Id { get; private set; } = id;
    public string Size { get; private set; } = size;
    public string LiquorType { get; set; } = string.Empty;
    public Uri ProductPageUrl { get; private set; } = new Uri(url);

    public int PackageVolume {
        get {
            // 6 x 341
            // 750
            var productSizeElements = this.Size.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var packageHasMultipleContainers = productSizeElements.Length > 1;
            //var containerElements = packageHasMultipleContainers ? productSizeElements[0].Split('x') : Array.Empty<string>();
            var units = packageHasMultipleContainers ? Convert.ToInt32(productSizeElements[0], CultureInfo.InvariantCulture) : 1;
            var unitVolume = packageHasMultipleContainers ? Convert.ToInt32(productSizeElements[2], CultureInfo.InvariantCulture) : Convert.ToInt32(productSizeElements[0], CultureInfo.InvariantCulture);
            return units * unitVolume;
        }
    }

    public override string ToString()
    {
        return $"ID {this.Id} Name {this.Name} Size {this.Size} Volume {this.PackageVolume} URL {this.ProductPageUrl}";
    }
}
