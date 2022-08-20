using System.Globalization;

namespace Spurious2.Core.LcboImporting.Domain;

public class Product
{
    public string Name { get; private set; }
    public int Id { get; private set; }
    public string Size { get; private set; }
    public string LiquorType { get; set; } = string.Empty;
    public string ProductPageUrl { get; private set; }
    public Product(string name, int id, string size, string url)
    {
        this.Name = name;
        this.Id = id;
        this.Size = size;
        this.ProductPageUrl = url;
    }

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
