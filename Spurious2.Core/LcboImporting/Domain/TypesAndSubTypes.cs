using System.Collections.ObjectModel;

namespace Spurious2.Core.LcboImporting.Domain;

public static class TypesAndSubTypes
{
    public static readonly ReadOnlyDictionary<ProductType, List<ProductSubtype>> ProductsToSubtypeMap = new(
        new Dictionary<ProductType, List<ProductSubtype>>
        {
            {
                ProductType.Beer, new List<ProductSubtype>
                {
                    ProductSubtype.Ale,
                    ProductSubtype.Lager,
                    ProductSubtype.Cider,
                    ProductSubtype.SpecialityBeer,
                    ProductSubtype.SamplersBeer,
                }
            },
            {
                ProductType.Wine, new List<ProductSubtype>
                {
                    ProductSubtype.Red,
                    ProductSubtype.White,
                    ProductSubtype.Rose,
                    ProductSubtype.Champagne,
                    ProductSubtype.Sparkling,
                    ProductSubtype.Dessert,
                    ProductSubtype.Ice,
                    ProductSubtype.Fortified,
                    ProductSubtype.SpecialityWine,
                    ProductSubtype.SamplersWine,
                    ProductSubtype.Sake,
                }
            },
            {
                ProductType.Coolers, new List<ProductSubtype>
                {
                    ProductSubtype.HardSeltzers,
                    ProductSubtype.HardTeas,
                    ProductSubtype.LightCoolers,
                    ProductSubtype.TraditionalCoolers,
                    ProductSubtype.Caesers,
                    ProductSubtype.Cocktails,
                }
            },
            {
                ProductType.Spirits, new List<ProductSubtype>
                {
                    ProductSubtype.Whiskey,
                    ProductSubtype.Liqueur,
                    ProductSubtype.Tequila,
                    ProductSubtype.Vodka,
                    ProductSubtype.Rum,
                    ProductSubtype.Gin,
                    ProductSubtype.CongacBrandy,
                    ProductSubtype.Grappa,
                    ProductSubtype.Soju,
                }
            },
        });
}
