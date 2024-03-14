using System.Globalization;

namespace Spurious2.Core2.Products;

public partial class ProductIncoming
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string Category { get; set; }

    public bool ProductDone { get; set; }

    public Uri ProductPageUrl { get; set; }
    public string Size { get; set; }

    public int Volume
    {
        get
        {
            // 6 x 341
            // 750
            var productSizeElements = this.Size.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var packageHasMultipleContainers = productSizeElements.Length > 1;
            //var containerElements = packageHasMultipleContainers ? productSizeElements[0].Split('x') : Array.Empty<string>();
            var units = packageHasMultipleContainers ? Convert.ToInt32(productSizeElements[0], CultureInfo.InvariantCulture) : 1;
            var unitVolume = packageHasMultipleContainers ? Convert.ToInt32(productSizeElements[2], CultureInfo.InvariantCulture) : Convert.ToInt32(productSizeElements[0], CultureInfo.InvariantCulture);
            return units * unitVolume;
        }
        set => _ = value;
    }
}
