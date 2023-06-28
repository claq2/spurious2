using Ardalis.GuardClauses;
using Spurious2.Core;
using Spurious2.Core.LcboImporting.Domain;
using System.Collections.ObjectModel;

namespace LcboWebsiteAdapter;

public class CategorizedProductListClient
{
    private static readonly IReadOnlyDictionary<ProductType, string> productTypeTabTemplateMap =
        new ReadOnlyDictionary<ProductType, string>(
            new Dictionary<ProductType, string>
            {
                { ProductType.Beer, "clp-products-beer_&_cider-{0}" },
                { ProductType.Wine, "clp-products-wine-{0}" },
                { ProductType.Spirits, "clp-products-spirits-{0}" },
                { ProductType.Coolers, "clp-products-coolers-{0}" },
            });

    private static readonly IReadOnlyDictionary<ProductType, string> productTypeCategoryTemplateMap =
       new ReadOnlyDictionary<ProductType, string>(
           new Dictionary<ProductType, string>
           {
                { ProductType.Beer, "\\\"Products|Beer & Cider|{0}\\\"" },
                { ProductType.Wine, "\\\"Products|Wine|{0}\\\"" },
                { ProductType.Spirits, "\\\"Products|Spirits|{0}\\\"" },
                { ProductType.Coolers, "\\\"Products|Coolers|{0}\\\"" },
           });

    private readonly Dictionary<ProductSubtype, (string Tab, string Category)> subsTabCat = new Dictionary<ProductSubtype, (string Tab, string Category)>
    {
        // Beer
        { ProductSubtype.Ale, ("ale", "Ale") },
        { ProductSubtype.Cider, ("cider", "Cider") },
        { ProductSubtype.SpecialityBeer, ("non-alcoholic_&_specialty", "Non-Alcoholic & Specialty") },
        { ProductSubtype.SamplersBeer, ("gifts_and_samplers", "Gifts And Samplers") },
        { ProductSubtype.Lager, ("lager", "Lager") },
        // Wine
        { ProductSubtype.Red, ("red_wine", "Red Wine") },
        { ProductSubtype.White, ("white_wine", "White Wine") },
        { ProductSubtype.Rose, ("rose_wine", "Rose Wine") },
        { ProductSubtype.Champagne, ("champagne", "Champagne") },
        { ProductSubtype.Sparkling, ("sparkling_wine", "Sparkling Wine") },
        { ProductSubtype.Dessert, ("dessert_wine", "Dessert Wine") },
        { ProductSubtype.Ice, ("icewine", "Icewine") },
        { ProductSubtype.Fortified, ("fortified_wine", "Fortified Wine") },
        { ProductSubtype.SpecialityWine, ("specialty_wine", "Speciality Wine") },
        { ProductSubtype.SamplersWine, ("gifts_and_samplers", "Gifts And Samplers") },
        { ProductSubtype.Sake, ("sake_&_rice_wine", "Sake & Rice Wine") },
        // Sprits
        { ProductSubtype.Whiskey, ("whiskey", "Whiskey") },
        { ProductSubtype.Liqueur, ("liqueur", "Liqueur") },
        { ProductSubtype.Tequila, ("tequila", "Tequila") },
        { ProductSubtype.Vodka, ("vodka", "Vodka") },
        { ProductSubtype.Rum, ("rum", "Rum") },
        { ProductSubtype.Gin, ("gin", "Gin") },
        { ProductSubtype.CongacBrandy, ("cognac_&_brandy", "Cognac & Brandy") },
        { ProductSubtype.Grappa, ("grappa", "Grappa") },
        { ProductSubtype.Soju, ("soju", "Soju") },
        // Coolers
        { ProductSubtype.HardSeltzers, ("hard_seltzers", "Hard Seltzers") },
        { ProductSubtype.HardTeas, ("hard_teas", "Hard Teas") },
        { ProductSubtype.LightCoolers, ("light_coolers", "Light Coolers") },
        { ProductSubtype.TraditionalCoolers, ("traditional_coolers", "Traditional Coolers") },
        { ProductSubtype.Caesers, ("caesars", "Caesars") },
        { ProductSubtype.Cocktails,  ("cocktails", "Cocktails") },

    };

    private static readonly IReadOnlyDictionary<ProductType, List<(string Tab, string Category)>> ps =
        new ReadOnlyDictionary<ProductType, List<(string Tab, string Category)>>(
            new Dictionary<ProductType, List<(string Tab, string Category)>>
            {
                {
                    ProductType.Beer, new List<(string Tab, string Category)>
                    {
                        ("ale", "Ale"),
                        ("lager", "Lager"),
                        ("cider", "Cider"),
                        ("non-alcoholic_&_specialty", "Non-Alcoholic & Specialty"),
                        ("gifts_and_samplers", "Gifts And Samplers"),
                    }
                },
                {
                    ProductType.Wine, new List<(string Tab, string Category)>
                    {
                        ("red_wine", "Red Wine"),
                        ("white_wine", "White Wine"),
                        ("rose_wine", "Rose Wine"),
                        ("champagne", "Champagne"),
                        ("sparkling_wine", "Sparkling Wine"),
                        ("dessert_wine", "Dessert Wine"),
                        ("icewine", "Icewine"),
                        ("fortified_wine", "Fortified Wine"),
                        ("specialty_wine", "Speciality Wine"),
                        ("gifts_and_samplers", "Gifts And Samplers"),
                        ("sake_&_rice_wine", "Sake & Rice Wine"),
                    }
                },
                {
                    ProductType.Coolers, new List<(string Tab, string Category)>
                    {
                        ("hard_seltzers", "Hard Seltzers"),
                        ("hard_teas", "Hard Teas"),
                        ("light_coolers", "Light Coolers"),
                        ("traditional_coolers", "Traditional Coolers"),
                        ("caesars", "Caesars"),
                        ("cocktails", "Cocktails"),
                    }
                },
                {
                    ProductType.Spirits, new List<(string Tab, string Category)>
                    {
                        ("whiskey", "Whiskey"),
                        ("liqueur", "Liqueur"),
                        ("tequila", "Tequila"),
                        ("vodka", "Vodka"),
                        ("rum", "Rum"),
                        ("gin", "Gin"),
                        ("cognac_&_brandy", "Cognac & Brandy"),
                        ("grappa", "Grappa"),
                        ("soju", "Soju"),
                    }
                },
            });

    private readonly HttpClient httpClient;

    public CategorizedProductListClient(HttpClient httpClient)
    {
        this.httpClient = httpClient;

        var headers = new Dictionary<string, string>
        {
            { "Accept", "*/*" },
            { "Accept-Language", "en-US,en;q=0.5" },
            { "Authorization", "Bearer xx883b5583-07fb-416b-874b-77cce565d927" },
            { "DNT", "1" },
            { "Connection", " keep-alive" },
            { "Sec-Fetch-Dest", "empty" },
            { "Sec-Fetch-Mode", "cors" },
            { "Sec-Fetch-Site", "cross-site" },
            { "Sec-GPC", "1" },
        };

        foreach (KeyValuePair<string, string> pair in headers)
        {
            this.httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
        }

        this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
        this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.137 Safari/537.36");
    }

    //public async Task<Rootobject> GetProductList(int start, ProductType productType)
    //{
    //    var formVars = new Dictionary<string, string>
    //    {
    //        { "firstResult", start.ToString() },
    //        { "numberOfResults", "9" },
    //        { "locale", "en" },
    //        { "searchHub", "WebClpEN" },
    //        { "tab", productTypeTabTemplateMap[productType] },
    //        { "aq", $"@ec_category=={productTypeCategoryTemplateMap[productType]}" },
    //    };

    //    var content = new FormUrlEncodedContent(formVars);
    //    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
    //    var productsResp = await this.httpClient.PostAsync("https://platform.cloud.coveo.com/rest/search/v2?organizationId=lcboproductionx2kwygnc",
    //        content);
    //    var s = await productsResp.Content.ReadAsStringAsync();
    //    var sj = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(s);

    //    Guard.Against.NullValue(sj, nameof(sj));

    //    return sj;
    //}

    public async Task<Rootobject> GetProductList(int start, ProductType productType, ProductSubtype productSubtype)
    {
        var tabFormat = productTypeTabTemplateMap[productType];
        var categoryFormat = productTypeCategoryTemplateMap[productType];
        var (tab, category) = this.subsTabCat[productSubtype];

        var formVars = new Dictionary<string, string>
        {
            { "firstResult", start.ToString() },
            { "numberOfResults", "9" },
            { "locale", "en" },
            { "searchHub", "WebClpEN" },
            { "tab", string.Format(tabFormat, tab) },
            { "aq", $"@ec_category=={string.Format(categoryFormat, category)}" },
            //{ "sortCriteria", "@ec_price ascending" },
            { "sortCriteria", "@created_at descending" },
        };

        var content = new FormUrlEncodedContent(formVars);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
        var productsResp = await this.httpClient.PostAsync("https://platform.cloud.coveo.com/rest/search/v2?organizationId=lcboproductionx2kwygnc",
            content);
        var s = await productsResp.Content.ReadAsStringAsync();
        var sj = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(s);

        Guard.Against.NullValue(sj, nameof(sj));

        return sj;
    }
}
