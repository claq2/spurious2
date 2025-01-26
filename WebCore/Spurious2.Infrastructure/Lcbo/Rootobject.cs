using System.Text.Json.Serialization;

namespace Spurious2.Infrastructure.Lcbo;

#pragma warning disable IDE1006 // Naming Styles
public class Rootobject
{
    public int totalCount { get; set; }
    public int totalCountFiltered { get; set; }
    public int duration { get; set; }
    public int indexDuration { get; set; }
    public int requestDuration { get; set; }
    public string? searchUid { get; set; }
    public string? pipeline { get; set; }
    public int apiVersion { get; set; }
    public string? index { get; set; }
    public string? indexRegion { get; set; }
    public string? indexToken { get; set; }
    public IEnumerable<object>? refinedKeywords { get; set; }
    public IEnumerable<object>? triggers { get; set; }
    public Termstohighlight? termsToHighlight { get; set; }
    public Phrasestohighlight? phrasesToHighlight { get; set; }
    public IEnumerable<object>? queryCorrections { get; set; }
    public IEnumerable<object>? groupByResults { get; set; }
    public IEnumerable<object>? facets { get; set; }
    public IEnumerable<object>? suggestedFacets { get; set; }
    public IEnumerable<object>? categoryFacets { get; set; }
    public IEnumerable<Result> results { get; set; }

    public Rootobject() => this.results = [];
}

public class Termstohighlight
{
}

public class Phrasestohighlight
{
}

#pragma warning disable CA1708 // Identifiers should differ by more than case
public class Result
#pragma warning restore CA1708 // Identifiers should differ by more than case
{
    public string title { get; set; }
    public Uri uri { get; set; }
    public Uri? printableUri { get; set; }
    public Uri? clickUri { get; set; }
    public string? uniqueId { get; set; }
    public string? excerpt { get; set; }
    public object? firstSentences { get; set; }
    public object? summary { get; set; }
    public string? flags { get; set; }
    public bool hasHtmlVersion { get; set; }
    public bool hasMobileHtmlVersion { get; set; }
    public int score { get; set; }
    public float percentScore { get; set; }
    public object? rankingInfo { get; set; }
    public float rating { get; set; }
    public bool isTopResult { get; set; }
    public bool isRecommendation { get; set; }
    public bool isUserActionView { get; set; }
    public IEnumerable<object>? titleHighlights { get; set; }
    public IEnumerable<object>? firstSentencesHighlights { get; set; }
    public IEnumerable<object>? excerptHighlights { get; set; }
    public IEnumerable<object>? printableUriHighlights { get; set; }
    public IEnumerable<object>? summaryHighlights { get; set; }
    public object? parentResult { get; set; }
    public IEnumerable<object>? childResults { get; set; }
    public int totalNumberOfChildResults { get; set; }
    public IEnumerable<object>? absentTerms { get; set; }
    public Raw raw { get; set; }
    public string? Title { get; set; }
    public Uri? Uri { get; set; }
    public Uri? PrintableUri { get; set; }
    public Uri? ClickUri { get; set; }
    public string? UniqueId { get; set; }
    public string? Excerpt { get; set; }
    public object? FirstSentences { get; set; }
    public string? rankingModifier { get; set; }

    public Result()
    {
        this.title = string.Empty;
        this.raw = new Raw();
    }
}

public class Raw
{
    [JsonPropertyName("view_rank_monthly")]
    public int ViewRankMonthly { get; set; }
    public string? systitle { get; set; }
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
    [JsonPropertyName("is_buyable")]
    public string? IsBuyable { get; set; }
    public string? sysurihash { get; set; }
    public string? urihash { get; set; }
    public string? sysuri { get; set; }
    [JsonPropertyName("lcbo_stock_type")]
    public string? LcboStockType { get; set; }
    public string? commontabs { get; set; }
    public int systransactionid { get; set; }
    [JsonPropertyName("ec_rating")]
    public float EcRating { get; set; }
    public float validityscore { get; set; }
    [JsonPropertyName("stores_low_stock_combined")]
    public string? StoresLowStockCombined { get; set; }
    [JsonPropertyName("min_cart_qty")]
    public int MinCartQty { get; set; }
    [JsonPropertyName("lcbo_current_offer")]
    public IEnumerable<string>? LcboCurrentOoffer { get; set; }
    [JsonPropertyName("ec_skus")]
    public IEnumerable<string>? EcSkus { get; set; }
    public long sysindexeddate { get; set; }
    [JsonPropertyName("out_of_stock_threshold")]
    public int OutOfStockThreshold { get; set; }
    public string? permanentid { get; set; }
    public int transactionid { get; set; }
    public string? title { get; set; }
    [JsonPropertyName("ec_brand")]
    public string? EcBrand { get; set; }
    public long date { get; set; }
    public string? objecttype { get; set; }
    [JsonPropertyName("stores_stock")]
    public string? StoresStock { get; set; }
    [JsonPropertyName("low_stock_threshold")]
    public int LowStockThreshold { get; set; }
    [JsonPropertyName("country_of_manufacture")]
    public string? CountryOfManufacture { get; set; }
    [JsonPropertyName("online_inventory")]
    public int OnlineInventory { get; set; }
    public long rowid { get; set; }
    [JsonPropertyName("stores_stock_combined")]
    public string? StoresStockCombined { get; set; }
    [JsonPropertyName("ec_overall_promo_price")]
    public float EcOverallPromoPrice { get; set; }
    public int size { get; set; }
    [JsonPropertyName("ec_name")]
    public string? EcName { get; set; }
    public string? clickableuri { get; set; }
    [JsonPropertyName("avg_reviews")]
    public int AvgReviews { get; set; }
    public string? syssource { get; set; }
    public long orderingid { get; set; }
    public int syssize { get; set; }
    public long sysdate { get; set; }
    [JsonPropertyName("lcbo_alcohol_percent")]
    public float LcboAlcoholPercent { get; set; }
    [JsonPropertyName("ec_thumbnails")]
    public string? EcThumbnails { get; set; }
    [JsonPropertyName("stores_low_stock")]
    public string? StoresLowStock { get; set; }
    [JsonPropertyName("lcbo_bottles_per_pack")]
    public int LcboBottlesPerPack { get; set; }
    [JsonPropertyName("out_of_stock")]
    public string? OutOfStock { get; set; }
    [JsonPropertyName("lcbo_unit_volume")]
    public string LcboUnitVolume { get; set; }
    [JsonPropertyName("lcbo_selling_package_name")]
    public string? LcboSellingPackageName { get; set; }
    public string? enabled { get; set; }
    public int wordcount { get; set; }
    [JsonPropertyName("ec_category")]
    public IEnumerable<string>? EcCategory { get; set; }
    public string? source { get; set; }
    [JsonPropertyName("ec_price")]
    public float EcPrice { get; set; }
    [JsonPropertyName("ec_category_filter")]
    public IEnumerable<string> EcCategoryFilter { get; set; }
    [JsonPropertyName("sell_rank_monthly")]
    public int SellRankMonthly { get; set; }
    public string? collection { get; set; }
    [JsonPropertyName("qty_increments")]
    public int QtyIncrements { get; set; }
    public long indexeddate { get; set; }
    [JsonPropertyName("default_low_stock")]
    public string? DefaultLowStock { get; set; }
    public string? sysclickableuri { get; set; }
    [JsonPropertyName("lcbo_total_volume")]
    public int LcboTotalVolume { get; set; }
    [JsonPropertyName("ec_promo_price")]
    public float EcPromoPrice { get; set; }
    [JsonPropertyName("default_stock")]
    public string? DefaultStock { get; set; }
    [JsonPropertyName("stores_inventory")]
    public string? StoresInventory { get; set; }
    [JsonPropertyName("lcbo_unit_volume_int")]
    public int LcboUnitVolumeInt { get; set; }
    public long sysrowid { get; set; }
    public Uri? uri { get; set; }
    public string? syscollection { get; set; }
    [JsonPropertyName("max_cart_qty")]
    public int MaxCartQty { get; set; }
    [JsonPropertyName("lcbo_bottles_per_case")]
    public int LcboBottlesPerCase { get; set; }
    [JsonPropertyName("ec_overall_price")]
    public float EcOverallPrice { get; set; }
    [JsonPropertyName("lcbo_region_name")]
    public string? LcboRegionName { get; set; }
    public string? sysconcepts { get; set; }
    public string? concepts { get; set; }
    public IEnumerable<string>? syslanguage { get; set; }
    [JsonPropertyName("ec_shortdesc")]
    public string? EcShortdesc { get; set; }
    [JsonPropertyName("lcbo_tastingnotes")]
    public string? LcboTastingnotes { get; set; }
    public string? filetype { get; set; }
    public string? sysfiletype { get; set; }
    public IEnumerable<string>? language { get; set; }
    [JsonPropertyName("lcbo_program")]
    public IEnumerable<string>? LcboProgram { get; set; }

    public Raw()
    {
        this.EcCategoryFilter = [];
        this.LcboUnitVolume = string.Empty;
    }
}
#pragma warning restore IDE1006 // Naming Styles
