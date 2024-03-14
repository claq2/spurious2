using System.Collections.ObjectModel;
using System.Globalization;
using Spurious2.Core2;
using Spurious2.Core2.Lcbo;

namespace Lcbo;

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

    private readonly Dictionary<ProductSubtype, (string Tab, string Category)> subsTabCat = new()
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

        foreach (var pair in headers)
        {
            this.httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
        }

        _ = this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
        _ = this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.137 Safari/537.36");
    }

    public async Task<Rootobject> GetProductList(int start, ProductType productType, ProductSubtype productSubtype)
    {
        var tabFormat = productTypeTabTemplateMap[productType];
        var categoryFormat = productTypeCategoryTemplateMap[productType];
        var (tab, category) = this.subsTabCat[productSubtype];

        var formVars = new Dictionary<string, string>
        {
            { "firstResult", start.ToString(CultureInfo.InvariantCulture) },
            { "numberOfResults", "9" },
            { "locale", "en" },
            { "searchHub", "WebClpEN" },
            { "tab", string.Format(CultureInfo.InvariantCulture, tabFormat, tab) },
            { "aq", $"@ec_category=={string.Format(CultureInfo.InvariantCulture, categoryFormat, category)}" },
            //{ "sortCriteria", "@ec_price ascending" },
            { "sortCriteria", "@created_at descending" },
        };

        using var content = new FormUrlEncodedContent(formVars);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
        var url = new Uri("https://platform.cloud.coveo.com/rest/search/v2?organizationId=lcboproductionx2kwygnc");
        var productsResp = await this.httpClient.PostAsync(url, content).ConfigAwait();
        var s = await productsResp.Content.ReadAsStringAsync().ConfigAwait();
        var sj = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(s);

        return sj ?? throw new EmptyProductListException();
    }
}
