using System.Diagnostics.CodeAnalysis;

namespace Lcbo;

#pragma warning disable IDE1006 // Naming Styles
[SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>")]
[SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
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
    public List<object>? refinedKeywords { get; set; }
    public List<object>? triggers { get; set; }
    public Termstohighlight? termsToHighlight { get; set; }
    public Phrasestohighlight? phrasesToHighlight { get; set; }
    public List<object>? queryCorrections { get; set; }
    public List<object>? groupByResults { get; set; }
    public List<object>? facets { get; set; }
    public List<object>? suggestedFacets { get; set; }
    public List<object>? categoryFacets { get; set; }
    public List<Result> results { get; set; }

    public Rootobject() => this.results = [];
}

public class Termstohighlight
{
}

public class Phrasestohighlight
{
}

#pragma warning disable CA1708 // Identifiers should differ by more than case
[SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>")]
[SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
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
    public List<object>? titleHighlights { get; set; }
    public List<object>? firstSentencesHighlights { get; set; }
    public List<object>? excerptHighlights { get; set; }
    public List<object>? printableUriHighlights { get; set; }
    public List<object>? summaryHighlights { get; set; }
    public object? parentResult { get; set; }
    public List<object>? childResults { get; set; }
    public int totalNumberOfChildResults { get; set; }
    public List<object>? absentTerms { get; set; }
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

[SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "<Pending>")]
[SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>")]
[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
public class Raw
{
    public int view_rank_monthly { get; set; }
    public string? systitle { get; set; }
    public long created_at { get; set; }
    public string? is_buyable { get; set; }
    public string? sysurihash { get; set; }
    public string? urihash { get; set; }
    public string? sysuri { get; set; }
    public string? lcbo_stock_type { get; set; }
    public string? commontabs { get; set; }
    public int systransactionid { get; set; }
    public float ec_rating { get; set; }
    public float validityscore { get; set; }
    public string? stores_low_stock_combined { get; set; }
    public int min_cart_qty { get; set; }
    public List<string>? lcbo_current_offer { get; set; }
    public List<string>? ec_skus { get; set; }
    public long sysindexeddate { get; set; }
    public int out_of_stock_threshold { get; set; }
    public string? permanentid { get; set; }
    public int transactionid { get; set; }
    public string? title { get; set; }
    public string? ec_brand { get; set; }
    public long date { get; set; }
    public string? objecttype { get; set; }
    public string? stores_stock { get; set; }
    public int low_stock_threshold { get; set; }
    public string? country_of_manufacture { get; set; }
    public int online_inventory { get; set; }
    public long rowid { get; set; }
    public string? stores_stock_combined { get; set; }
    public float ec_overall_promo_price { get; set; }
    public int size { get; set; }
    public string? ec_name { get; set; }
    public string? clickableuri { get; set; }
    public int avg_reviews { get; set; }
    public string? syssource { get; set; }
    public long orderingid { get; set; }
    public int syssize { get; set; }
    public long sysdate { get; set; }
    public float lcbo_alcohol_percent { get; set; }
    public string? ec_thumbnails { get; set; }
    public string? stores_low_stock { get; set; }
    public int lcbo_bottles_per_pack { get; set; }
    public string? out_of_stock { get; set; }
    public string lcbo_unit_volume { get; set; }
    public string? lcbo_selling_package_name { get; set; }
    public string? enabled { get; set; }
    public int wordcount { get; set; }
    public List<string>? ec_category { get; set; }
    public string? source { get; set; }
    public float ec_price { get; set; }
    public List<string> ec_category_filter { get; set; }
    public int sell_rank_monthly { get; set; }
    public string? collection { get; set; }
    public int qty_increments { get; set; }
    public long indexeddate { get; set; }
    public string? default_low_stock { get; set; }
    public string? sysclickableuri { get; set; }
    public int lcbo_total_volume { get; set; }
    public float ec_promo_price { get; set; }
    public string? default_stock { get; set; }
    public string? stores_inventory { get; set; }
    public int lcbo_unit_volume_int { get; set; }
    public long sysrowid { get; set; }
    public Uri? uri { get; set; }
    public string? syscollection { get; set; }
    public int max_cart_qty { get; set; }
    public int lcbo_bottles_per_case { get; set; }
    public float ec_overall_price { get; set; }
    public string? lcbo_region_name { get; set; }
    public string? sysconcepts { get; set; }
    public string? concepts { get; set; }
    public List<string>? syslanguage { get; set; }
    public string? ec_shortdesc { get; set; }
    public string? lcbo_tastingnotes { get; set; }
    public string? filetype { get; set; }
    public string? sysfiletype { get; set; }
    public List<string>? language { get; set; }
    public List<string>? lcbo_program { get; set; }

    public Raw()
    {
        this.ec_category_filter = [];
        this.lcbo_unit_volume = string.Empty;
    }
}
#pragma warning restore IDE1006 // Naming Styles
