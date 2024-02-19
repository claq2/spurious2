using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0053:Use expression body for lambda expression", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1861: Avoid constant arrays as arguments", Justification = "<Pending>")]
public partial class StoreData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Store",
            columns: ["Id", "BeerVolume", "City", "Location", "SpiritsVolume", "StoreName", "WineVolume"],
            values: new object[,]
            {
                { 1, 957096, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.531037 43.712679)"), 382200, "Hwy 401 & Weston (Crossroads)", 236625 },
                { 2, 43043, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.415366 43.683997)"), 6000, "St. Clair & Bathurst", 1125 },
                { 3, 146885, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.42351 43.649585)"), 134550, "Dundas & Dovercourt", 42000 },
                { 4, 366830, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.356106 43.67688)"), 658400, "Danforth & Broadview", 390000 },
                { 5, 423824, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.314978 43.666877)"), 213400, "Queen & Coxwell (The Beach)", 185100 },
                { 6, 290807, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.43239 43.642136)"), 82250, "Queen & Lansdowne", 7500 },
                { 7, 28191, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.3948 43.707374)"), 22750, "Eglinton & Dunfield", 65250 },
                { 8, 183298, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.433905 43.680158)"), 29250, "St. Clair & Oakwood", 6750 },
                { 9, 115553, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.425133 43.661944)"), 50250, "Bloor & Ossington", 0 },
                { 10, 810753, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.390516 43.68089)"), 446050, "Yonge & Summerhill", 602050 },
                { 11, 41398, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.341101 43.66818)"), 0, "Gerrard & Pape", 3000 },
                { 12, 34538, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.368661 43.665544)"), 31500, "Parliament & Wellesly (Cabbagetown)", 0 },
                { 13, 66960, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.333301 43.681034)"), 27750, "Danforth & Greenwood", 0 },
                { 14, 146426, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.398469 43.654843)"), 80550, "Spadina & Dundas", 6000 },
                { 15, 296449, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.405677 43.675325)"), 166150, "Dupont & Spadina", 207375 },
                { 16, 25548, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.412504 43.768838)"), 42600, "Yonge & Empress (Empress Walk)", 2250 },
                { 17, 192197, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.364894 43.651216)"), 93500, "Front & Sherbourne", 114750 },
                { 18, 285971, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.419912 43.639925)"), 159350, "King & Dufferin (Liberty Village)", 165750 },
                { 19, 256354, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.472894 43.670479)"), 454450, "St. Clair & Keele (The Stockyards)", 302625 },
                { 20, 176419, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.499292 43.602313)"), 21750, "Lake Shore & Islington", 0 },
                { 21, 436587, "Ancaster", (Point)new WKTReader().Read("SRID=4326;POINT (-79.95496 43.226231)"), 98500, "Golf Links & Hwy 403 (Meadowlands)", 22425 },
                { 22, 324381, "Barrie", (Point)new WKTReader().Read("SRID=4326;POINT (-79.650842 44.356011)"), 125250, "Big Bay Point Rd & Yonge", 70875 },
                { 23, 487270, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.81333 43.251816)"), 104650, "Barton & Kenilworth (Centre Mall)", 19125 },
                { 24, 83116, "Port Colborne", (Point)new WKTReader().Read("SRID=4326;POINT (-79.255131 42.885961)"), 6750, "Clarence & Elm St", 450 },
                { 25, 402429, "Dundas", (Point)new WKTReader().Read("SRID=4326;POINT (-79.941243 43.257921)"), 44150, "Osler & Main W", 114000 },
                { 26, 257336, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.25184 42.98019)"), 67050, "York & Ridout", 3950 },
                { 27, 562634, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.227089 43.003516)"), 90750, "Oxford St E & Gammage St", 9750 },
                { 28, 91674, "Dunnville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.615577 42.90325)"), 30750, "Broad & Maple St", 14250 },
                { 30, 0, "Walkerton", (Point)new WKTReader().Read("SRID=4326;POINT (-81.147181 44.133793)"), 0, "Victoria & Durham St", 15000 },
                { 31, 533759, "Collingwood", (Point)new WKTReader().Read("SRID=4326;POINT (-80.218285 44.502973)"), 153600, "First & Pine", 30750 },
                { 32, 98209, "Windsor", (Point)new WKTReader().Read("SRID=4326;POINT (-83.043376 42.316644)"), 27000, "University & Church", 0 },
                { 33, 153033, "Niagara Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-79.121101 43.065997)"), 123800, "Mcleod & Qew", 21775 },
                { 34, 213235, "Windsor", (Point)new WKTReader().Read("SRID=4326;POINT (-83.001219 42.313571)"), 21750, "Walker & Ottawa", 19500 },
                { 35, 314539, "Peterborough", (Point)new WKTReader().Read("SRID=4326;POINT (-78.339503 44.28233)"), 58150, "Lansdowne W & Parkway", 82350 },
                { 36, 262804, "Ottawa-Kanata", (Point)new WKTReader().Read("SRID=4326;POINT (-75.887426 45.300179)"), 87100, "Hazeldean & Castlefrank (Market Square)", 24000 },
                { 38, 450421, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.68631 45.428756)"), 358350, "Rideau & King Edward", 455625 },
                { 39, 122034, "Cobourg", (Point)new WKTReader().Read("SRID=4326;POINT (-78.167699 43.958475)"), 7500, "3rd & Albert St", 0 },
                { 40, 192517, "Kingston", (Point)new WKTReader().Read("SRID=4326;POINT (-76.480741 44.232974)"), 37100, "Barrack & King", 94875 },
                { 41, 185191, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.639729 45.410776)"), 49800, "Industrial & St. Laurent (Trainyards)", 15750 },
                { 42, 0, "Brockville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.6878 44.5876)"), 4500, "King & John St", 0 },
                { 43, 129626, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.495458 43.452188)"), 6300, "King St W & Victoria St", 30200 },
                { 44, 108805, "Brantford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.261709 43.135525)"), 41500, "Market S & Icomm", 15000 },
                { 45, 107679, "Peterborough", (Point)new WKTReader().Read("SRID=4326;POINT (-78.321327 44.30101)"), 38150, "Sherbrooke & George", 6000 },
                { 46, 0, "Sarnia", (Point)new WKTReader().Read("SRID=4326;POINT (-82.405685 42.97539)"), 15750, "Christina & George St", 450 },
                { 47, 136798, "St. Catharines", (Point)new WKTReader().Read("SRID=4326;POINT (-79.243622 43.161598)"), 34250, "King & Academy", 16500 },
                { 48, 61737, "Niagara Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-79.073049 43.106212)"), 0, "Victoria & Valley Way", 7025 },
                { 49, 175154, "Cornwall", (Point)new WKTReader().Read("SRID=4326;POINT (-74.704968 45.025045)"), 46000, "Second St & Glengarry Blvd", 0 },
                { 50, 61264, "Sudbury", (Point)new WKTReader().Read("SRID=4326;POINT (-80.990836 46.492618)"), 18250, "Elm & Paris St", 0 },
                { 51, 156319, "Guelph", (Point)new WKTReader().Read("SRID=4326;POINT (-80.248151 43.546098)"), 16550, "Wellington & Gordon", 3750 },
                { 52, 8041, "Welland", (Point)new WKTReader().Read("SRID=4326;POINT (-79.239729 42.97614)"), 4250, "Ontario & Southworth (Rose City Plaza)", 24000 },
                { 53, 61963, "Cookstown", (Point)new WKTReader().Read("SRID=4326;POINT (-79.708881 44.188993)"), 11650, "Queen & King", 9000 },
                { 54, 140331, "North Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-79.462896 46.312902)"), 69500, "Worthington & Fraser St", 0 },
                { 55, 146193, "Pembroke", (Point)new WKTReader().Read("SRID=4326;POINT (-77.081439 45.820838)"), 8750, "Pembroke St E & Angus Campbell Dr", 18000 },
                { 56, 8987, "Prescott", (Point)new WKTReader().Read("SRID=4326;POINT (-75.516434 44.70781)"), 0, "Hwy 401 & 2", 0 },
                { 57, 172424, "Belleville", (Point)new WKTReader().Read("SRID=4326;POINT (-77.404896 44.187202)"), 37500, "Bell & N Front (Near Quinte Mall)", 58125 },
                { 58, 324005, "Timmins", (Point)new WKTReader().Read("SRID=4326;POINT (-81.337054 48.477872)"), 40500, "Algonquin", 45000 },
                { 59, 2838, "Barrie", (Point)new WKTReader().Read("SRID=4326;POINT (-79.692366 44.38894)"), 8050, "Mary & Dunlop", 0 },
                { 60, 186334, "Stratford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.983308 43.368968)"), 25350, "Wellington & St. Patrick", 78750 },
                { 61, 61737, "Chatham", (Point)new WKTReader().Read("SRID=4326;POINT (-82.188145 42.403857)"), 17750, "Wellington & Raleigh", 9000 },
                { 62, 75927, "St. Thomas", (Point)new WKTReader().Read("SRID=4326;POINT (-81.17698 42.778006)"), 41500, "First & Talbot St", 18000 },
                { 63, 103608, "Sault Ste. Marie", (Point)new WKTReader().Read("SRID=4326;POINT (-84.338068 46.512082)"), 34300, "Bay & Bruce (Station Mall)", 15750 },
                { 64, 356162, "Woodstock", (Point)new WKTReader().Read("SRID=4326;POINT (-80.732119 43.13448)"), 30750, "Dundas & Springbank", 69125 },
                { 65, 723958, "Trenton", (Point)new WKTReader().Read("SRID=4326;POINT (-77.57917 44.103306)"), 20250, "Front & Division", 64875 },
                { 67, 9460, "Haileybury", (Point)new WKTReader().Read("SRID=4326;POINT (-79.631899 47.449651)"), 0, "Lakeshore & Broadway", 14250 },
                { 68, 20339, "Kenora", (Point)new WKTReader().Read("SRID=4326;POINT (-94.482424 49.767685)"), 9750, "Park & Railway St", 0 },
                { 69, 47434, "Fort Frances", (Point)new WKTReader().Read("SRID=4326;POINT (-93.398536 48.612916)"), 4900, "Third & Mowat St", 3375 },
                { 70, 149468, "Smiths Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-76.029731 44.890612)"), 1500, "Lombard St & Ferrara Dr", 4500 },
                { 71, 34529, "Gananoque", (Point)new WKTReader().Read("SRID=4326;POINT (-76.160782 44.330528)"), 0, "King & Charles St", 375 },
                { 72, 148049, "Perth", (Point)new WKTReader().Read("SRID=4326;POINT (-76.265024 44.907607)"), 0, "Dufferin & Wilson", 5250 },
                { 73, 34776, "Cambridge-Preston", (Point)new WKTReader().Read("SRID=4326;POINT (-80.352231 43.392911)"), 15750, "King & Dolph", 15000 },
                { 74, 147112, "Simcoe", (Point)new WKTReader().Read("SRID=4326;POINT (-80.288299 42.848298)"), 71900, "Queensway & Ireland", 27000 },
                { 76, 21300, "Cobalt", (Point)new WKTReader().Read("SRID=4326;POINT (-79.68601 47.395688)"), 0, "Grandview Ave & Silver St", 0 },
                { 77, 97950, "Penetang", (Point)new WKTReader().Read("SRID=4326;POINT (-79.932434 44.765962)"), 18750, "Poyntz St & Penetang Rd", 0 },
                { 78, 14910, "New Liskeard", (Point)new WKTReader().Read("SRID=4326;POINT (-79.671108 47.506908)"), 18750, "Armstrong & Cedar", 28500 },
                { 79, 23177, "Port Hope", (Point)new WKTReader().Read("SRID=4326;POINT (-78.293811 43.95208)"), 3750, "Walton St & Peter St", 0 },
                { 80, 160853, "Parry Sound", (Point)new WKTReader().Read("SRID=4326;POINT (-80.03281 45.343693)"), 98250, "Sequin & Miller", 66000 },
                { 81, 177300, "Sturgeon Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-79.930471 46.366258)"), 6750, "Front & Holditch", 0 },
                { 82, 319415, "Lindsay", (Point)new WKTReader().Read("SRID=4326;POINT (-78.766523 44.348417)"), 31950, "Kent & Hwy 35", 48000 },
                { 83, 442749, "Hawkesbury", (Point)new WKTReader().Read("SRID=4326;POINT (-74.592488 45.590747)"), 214750, "Hwy 17 & Tupper St", 177250 },
                { 84, 112819, "Picton", (Point)new WKTReader().Read("SRID=4326;POINT (-77.151122 44.003336)"), 57750, "Hwy 62/33 & County Rd 10", 55500 },
                { 85, 407499, "Arnprior", (Point)new WKTReader().Read("SRID=4326;POINT (-76.36798 45.421812)"), 73000, "Hwy 417 & Daniel St", 30450 },
                { 86, 107271, "Dryden", (Point)new WKTReader().Read("SRID=4326;POINT (-92.834381 49.784309)"), 22500, "King St & Kirkpatrick", 0 },
                { 87, 30272, "Sioux Lookout", (Point)new WKTReader().Read("SRID=4326;POINT (-91.915197 50.098149)"), 0, "Front St & Fourth Ave", 0 },
                { 88, 51566, "Paris", (Point)new WKTReader().Read("SRID=4326;POINT (-80.384284 43.19193)"), 0, "Mechanic St & Broadway St", 0 },
                { 89, 7095, "Mattawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.700615 46.316931)"), 0, "Mcconnell & Main St", 7500 },
                { 90, 250038, "Oshawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.865965 43.898715)"), 69750, "Ritson & Adelaide", 7500 },
                { 91, 19637, "Kirkland Lake", (Point)new WKTReader().Read("SRID=4326;POINT (-80.036184 48.152289)"), 7000, "Summer Hayes & Duncan Ave", 7500 },
                { 92, 70992, "Englehart", (Point)new WKTReader().Read("SRID=4326;POINT (-79.874126 47.825658)"), 0, "Fourth Ave & Third St", 0 },
                { 93, 104780, "Fort Erie", (Point)new WKTReader().Read("SRID=4326;POINT (-78.94136 42.907259)"), 19250, "Thompson & Garrison Rd", 29250 },
                { 94, 0, "Eganville", (Point)new WKTReader().Read("SRID=4326;POINT (-77.105489 45.543871)"), 0, "Hwy 41 & Hwy 60", 6750 },
                { 95, 182355, "Cambridge", (Point)new WKTReader().Read("SRID=4326;POINT (-80.286644 43.35709)"), 83000, "Franklin & Dundas (South Cambridge Ctr)", 35625 },
                { 96, 21300, "Deseronto", (Point)new WKTReader().Read("SRID=4326;POINT (-77.050258 44.194914)"), 750, "Dundas St & St. George St", 15000 },
                { 97, 159885, "Napanee", (Point)new WKTReader().Read("SRID=4326;POINT (-76.961305 44.256711)"), 9000, "Alkenbrack & Centre St N", 21000 },
                { 98, 132440, "Alexandria", (Point)new WKTReader().Read("SRID=4326;POINT (-74.628159 45.300252)"), 7000, "Main St S & Albert St", 27000 },
                { 99, 65274, "Embrun", (Point)new WKTReader().Read("SRID=4326;POINT (-75.302438 45.268598)"), 3000, "Notre Dame & Ste Marie", 12000 },
                { 101, 138939, "Grimsby", (Point)new WKTReader().Read("SRID=4326;POINT (-79.561885 43.192488)"), 28900, "Christie & Main St", 41175 },
                { 102, 107453, "Gravenhurst", (Point)new WKTReader().Read("SRID=4326;POINT (-79.370012 44.908482)"), 7950, "Edward St & Talisman Dr", 87750 },
                { 103, 82076, "Espanola", (Point)new WKTReader().Read("SRID=4326;POINT (-81.771534 46.264366)"), 15000, "Hwy 6 & Barber St", 0 },
                { 104, 55490, "Thessalon", (Point)new WKTReader().Read("SRID=4326;POINT (-83.556865 46.255875)"), 2250, "Hwy 17b & Huron St", 0 },
                { 105, 0, "Tamworth", (Point)new WKTReader().Read("SRID=4326;POINT (-76.994411 44.487858)"), 14250, "Hwy 41 & Country Rd 4", 7500 },
                { 106, 184512, "Huntsville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.20305 45.335318)"), 58100, "Hwy 11b & William St", 59325 },
                { 107, 334424, "Palmerston", (Point)new WKTReader().Read("SRID=4326;POINT (-80.857548 43.832961)"), 8750, "Hwy 89 & 23", 13500 },
                { 108, 16082, "Blind River", (Point)new WKTReader().Read("SRID=4326;POINT (-82.953457 46.186663)"), 0, "Woodward Ave & Murray St", 0 },
                { 109, 45408, "Amherstburg", (Point)new WKTReader().Read("SRID=4326;POINT (-83.107217 42.093849)"), 20250, "Sandwich & Pickering", 15000 },
                { 110, 33583, "Wallaceburg", (Point)new WKTReader().Read("SRID=4326;POINT (-82.386594 42.588052)"), 0, "Mcnaughton & Hwy 40", 0 },
                { 111, 17974, "Westport", (Point)new WKTReader().Read("SRID=4326;POINT (-76.396127 44.679125)"), 3750, "Church & Bedford", 6750 },
                { 112, 37500, "Kapuskasing", (Point)new WKTReader().Read("SRID=4326;POINT (-82.417588 49.419496)"), 0, "Brunetville Rd & Ash St", 0 },
                { 113, 62210, "Barry's Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-77.680097 45.487988)"), 0, "Hwy 60 & 62", 0 },
                { 115, 660676, "Waterloo", (Point)new WKTReader().Read("SRID=4326;POINT (-80.521753 43.462025)"), 188250, "King S & William (Waterloo Town Sq)", 114225 },
                { 116, 130795, "Elmira", (Point)new WKTReader().Read("SRID=4326;POINT (-80.559869 43.599978)"), 15750, "Church & Arthur St", 21000 },
                { 117, 10406, "Chapleau", (Point)new WKTReader().Read("SRID=4326;POINT (-83.400869 47.842639)"), 0, "Lorne St & Birch Rd", 14250 },
                { 118, 112625, "Capreol", (Point)new WKTReader().Read("SRID=4326;POINT (-80.921894 46.706205)"), 17500, "Hanna & Dennie", 0 },
                { 119, 56551, "Merrickville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.834823 44.915425)"), 1500, "Elgin & Wellington St", 0 },
                { 120, 42600, "Pelee Island", (Point)new WKTReader().Read("SRID=4326;POINT (-82.688709 41.762038)"), 0, "West Dock", 21750 },
                { 122, 15265, "Hearst", (Point)new WKTReader().Read("SRID=4326;POINT (-83.663013 49.689058)"), 0, "8th St & George St", 0 },
                { 123, 156572, "Lancaster", (Point)new WKTReader().Read("SRID=4326;POINT (-74.498738 45.141261)"), 0, "Molan St & Hwy 34", 0 },
                { 124, 195661, "Niagara-On-The-Lake", (Point)new WKTReader().Read("SRID=4326;POINT (-79.071642 43.254982)"), 5250, "Queen & King", 34000 },
                { 125, 39732, "Rockland", (Point)new WKTReader().Read("SRID=4326;POINT (-75.30561 45.533946)"), 15750, "Richelieu & Poupart", 8625 },
                { 126, 8514, "Tilbury", (Point)new WKTReader().Read("SRID=4326;POINT (-82.432135 42.257722)"), 6750, "Hwy 2 & Queen St", 6750 },
                { 127, 39732, "Morrisburg", (Point)new WKTReader().Read("SRID=4326;POINT (-75.183728 44.900358)"), 0, "Hwy 31 & 2", 17250 },
                { 129, 30272, "Geraldton", (Point)new WKTReader().Read("SRID=4326;POINT (-86.948768 49.725892)"), 6750, "3 St & Main St", 0 },
                { 130, 127749, "Bracebridge", (Point)new WKTReader().Read("SRID=4326;POINT (-79.326824 45.044681)"), 72750, "Wellington St & Muskoka Rd", 51750 },
                { 131, 46354, "Ridgetown", (Point)new WKTReader().Read("SRID=4326;POINT (-81.887476 42.438766)"), 15750, "County Rd Hwy 19 & 21", 0 },
                { 132, 162824, "Carleton Place", (Point)new WKTReader().Read("SRID=4326;POINT (-76.118611 45.132482)"), 60000, "Mcneely Ave & Trans - Canada Hwy", 900 },
                { 133, 21758, "Petrolia", (Point)new WKTReader().Read("SRID=4326;POINT (-82.134233 42.881308)"), 0, "Petrolia Line & Hwy 21", 0 },
                { 135, 0, "Madoc", (Point)new WKTReader().Read("SRID=4326;POINT (-77.47839 44.510093)"), 11250, "Hwy 62 & 7", 0 },
                { 136, 10406, "Minden", (Point)new WKTReader().Read("SRID=4326;POINT (-78.723347 44.926638)"), 0, "Hwy 35 & South Water St", 0 },
                { 137, 81603, "Red Lake", (Point)new WKTReader().Read("SRID=4326;POINT (-93.822793 51.018042)"), 0, "Hwy 105 & Discovery Rd", 0 },
                { 139, 360447, "Sutton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.366672 44.312199)"), 65500, "Dalton & Black River Rd", 7500 },
                { 140, 97266, "Ottawa-Vanier", (Point)new WKTReader().Read("SRID=4326;POINT (-75.661987 45.435778)"), 33500, "Montreal Rd & Vanier Parkway", 0 },
                { 141, 35948, "Harrow", (Point)new WKTReader().Read("SRID=4326;POINT (-82.920659 42.035346)"), 14000, "King St & Walker Rd", 38250 },
                { 142, 145056, "Hastings", (Point)new WKTReader().Read("SRID=4326;POINT (-77.957588 44.308911)"), 15750, "Hwy 45 & Front St", 30000 },
                { 143, 336303, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.891037 43.254536)"), 114500, "Dundurn & Charlton", 81750 },
                { 144, 99339, "Renfrew", (Point)new WKTReader().Read("SRID=4326;POINT (-76.65705 45.477587)"), 32200, "O'brien & Wrangler Rd", 19500 },
                { 145, 134826, "Bradford", (Point)new WKTReader().Read("SRID=4326;POINT (-79.591385 44.107696)"), 163100, "Holland St W & Summerlyn Trail", 24375 },
                { 146, 2720, "Southampton", (Point)new WKTReader().Read("SRID=4326;POINT (-81.372772 44.496831)"), 0, "Grosvenor & High St", 0 },
                { 147, 19170, "Nipigon", (Point)new WKTReader().Read("SRID=4326;POINT (-88.261929 49.012543)"), 0, "Third St & Newton Ave", 2250 },
                { 148, 347376, "Oakville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.70455 43.445936)"), 111950, "N Service Rd & Dorval (Town Ctr W)", 35625 },
                { 149, 793818, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.50807 43.648296)"), 217250, "Bloor & Royal York (Kingsway)", 602250 },
                { 150, 342955, "Hamilton-Winona", (Point)new WKTReader().Read("SRID=4326;POINT (-79.641338 43.217179)"), 122550, "Qew & Fifty Rd", 33750 },
                { 151, 31227, "Marathon", (Point)new WKTReader().Read("SRID=4326;POINT (-86.380794 48.718531)"), 10500, "Ontario & Peninsula Rd", 0 },
                { 152, 56061, "Oshawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.850215 43.869052)"), 9750, "Wentworth & Cedar (Lake Vista Plaza)", 0 },
                { 153, 65048, "Leamington", (Point)new WKTReader().Read("SRID=4326;POINT (-82.598097 42.033465)"), 22500, "Erie S & Seacliff Dr", 17625 },
                { 154, 429948, "Kingston", (Point)new WKTReader().Read("SRID=4326;POINT (-76.574494 44.262723)"), 87800, "Midland & Princess", 105000 },
                { 155, 215249, "Thunder Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-89.216634 48.437819)"), 31500, "Cumberland & Water St", 3500 },
                { 156, 272284, "Georgetown", (Point)new WKTReader().Read("SRID=4326;POINT (-79.901206 43.649767)"), 74450, "Guelph & Mountainview", 55500 },
                { 157, 240371, "Gore Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-82.464941 45.918282)"), 0, "Meredith & Dawson St", 0 },
                { 158, 33119, "Little Current", (Point)new WKTReader().Read("SRID=4326;POINT (-81.925637 45.979276)"), 22500, "Hwy 6 & Manitowaning Rd", 0 },
                { 159, 0, "South River", (Point)new WKTReader().Read("SRID=4326;POINT (-79.376723 45.837003)"), 0, "Hwy 124 & Ottawa St", 7500 },
                { 160, 132216, "Cayuga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.856325 42.950003)"), 18000, "Hwy 54 & 3", 15000 },
                { 161, 138760, "Dresden", (Point)new WKTReader().Read("SRID=4326;POINT (-82.180528 42.592058)"), 0, "Hwy 21 & Lindsley St", 0 },
                { 162, 0, "Matheson", (Point)new WKTReader().Read("SRID=4326;POINT (-80.462573 48.532316)"), 9000, "Hwy 11 & Burton Rd", 0 },
                { 163, 158409, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.285849 43.691089)"), 36750, "Danforth & Victoria Park", 42000 },
                { 164, 622044, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.360024 43.709687)"), 387600, "Eglinton & Laird", 401500 },
                { 165, 57477, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.861547 43.221017)"), 22500, "Mohawk & Upper Wentworth", 16700 },
                { 166, 24599, "Essex", (Point)new WKTReader().Read("SRID=4326;POINT (-82.820211 42.173977)"), 15750, "Arthur & Talbot St", 7500 },
                { 167, 247203, "Kingston", (Point)new WKTReader().Read("SRID=4326;POINT (-76.51047 44.240921)"), 28250, "Princess & Concession", 7500 },
                { 168, 345784, "Orangeville", (Point)new WKTReader().Read("SRID=4326;POINT (-80.119589 43.903819)"), 52100, "Riddell & Centennial", 9100 },
                { 169, 82858, "Ingersoll", (Point)new WKTReader().Read("SRID=4326;POINT (-80.886057 43.03864)"), 33000, "Thames & Charles St", 1500 },
                { 170, 27686, "Atikokan", (Point)new WKTReader().Read("SRID=4326;POINT (-91.62055 48.757818)"), 0, "Main & Burns", 0 },
                { 171, 292752, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.797662 43.726729)"), 221600, "Sandalwood & Kennedy (Heart Lake)", 13875 },
                { 172, 12771, "Thedford", (Point)new WKTReader().Read("SRID=4326;POINT (-81.853203 43.164107)"), 0, "Louisa & Main St", 0 },
                { 173, 99140, "Terrace Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-87.102816 48.782546)"), 5250, "Hwy 17 N", 0 },
                { 174, 0, "Tweed", (Point)new WKTReader().Read("SRID=4326;POINT (-77.314144 44.479886)"), 750, "Victoria St & Bridge St", 7500 },
                { 175, 176438, "Orillia", (Point)new WKTReader().Read("SRID=4326;POINT (-79.413587 44.610571)"), 6750, "Front & Mississaga St", 0 },
                { 176, 14663, "Marmora", (Point)new WKTReader().Read("SRID=4326;POINT (-77.682784 44.483333)"), 0, "Hwy 7 & 14", 0 },
                { 177, 91603, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.411102 43.760142)"), 60900, "Yonge & Sheppard", 15375 },
                { 178, 29338, "Campbellford", (Point)new WKTReader().Read("SRID=4326;POINT (-77.797802 44.30702)"), 19250, "Bridge St & Front St", 0 },
                { 179, 213369, "Oakville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.694584 43.482326)"), 57250, "Upper Middle & 8th Line", 34125 },
                { 181, 48984, "Delhi", (Point)new WKTReader().Read("SRID=4326;POINT (-80.493544 42.854761)"), 0, "James & Hwy 3", 7500 },
                { 182, 63864, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.579492 43.557743)"), 65500, "Lakeshore & Hwy 10 (Port Credit)", 14625 },
                { 183, 8520, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.762335 43.685738)"), 12000, "Main & Queen", 8250 },
                { 184, 116358, "Wawa", (Point)new WKTReader().Read("SRID=4326;POINT (-84.772098 47.995727)"), 0, "Broadway Ave & Ganley St", 0 },
                { 186, 53220, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.510382 43.721155)"), 78000, "Jane & Wilson (Sheridan Mall)", 3000 },
                { 187, 463762, "Barrie", (Point)new WKTReader().Read("SRID=4326;POINT (-79.713243 44.414496)"), 177300, "Bayfield & Hanmer", 148050 },
                { 188, 177146, "Whitby", (Point)new WKTReader().Read("SRID=4326;POINT (-78.939877 43.876797)"), 23250, "Brock St & Dundas", 0 },
                { 189, 8987, "Smooth Rock Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-81.625477 49.274994)"), 1500, "Hwy 11 & Main Ave", 0 },
                { 190, 115666, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.801649 43.238358)"), 20500, "Main At Traffic Circle", 8250 },
                { 191, 157982, "Ajax", (Point)new WKTReader().Read("SRID=4326;POINT (-79.028273 43.897121)"), 121500, "Salem & Taunton", 40875 },
                { 192, 276420, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.237535 42.971269)"), 21750, "Wellington & Grand", 6750 },
                { 193, 25542, "Belle River", (Point)new WKTReader().Read("SRID=4326;POINT (-82.699343 42.293133)"), 6000, "Notre Dame St & 11th St", 0 },
                { 194, 0, "Elmvale", (Point)new WKTReader().Read("SRID=4326;POINT (-79.86482 44.585439)"), 9750, "Shaw & Kerr St", 0 },
                { 195, 273647, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.292394 43.729482)"), 123000, "Eglinton & Warden (Smart Centre)", 75375 },
                { 196, 410931, "Grand Bend", (Point)new WKTReader().Read("SRID=4326;POINT (-81.752523 43.310719)"), 13750, "Hwy 81 & Hwy 21", 9375 },
                { 197, 82302, "Deep River", (Point)new WKTReader().Read("SRID=4326;POINT (-77.488524 46.100441)"), 58500, "Champlain Ave & Deep River", 33000 },
                { 198, 118777, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.184462 43.771474)"), 71750, "Kingston & Morningside", 50625 },
                { 199, 0, "Tillsonburg", (Point)new WKTReader().Read("SRID=4326;POINT (-80.729308 42.862865)"), 32750, "Broadway & Bridge", 30000 },
                { 200, 205834, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.174458 43.003199)"), 51000, "Clarke & Dundas (Argyle Mall)", 30000 },
                { 201, 163905, "Ajax", (Point)new WKTReader().Read("SRID=4326;POINT (-79.022259 43.841982)"), 50250, "Bayly & Harwood", 0 },
                { 202, 158986, "Sarnia", (Point)new WKTReader().Read("SRID=4326;POINT (-82.365671 43.015098)"), 7000, "Lakeshore & Murphy Nw Corner", 12000 },
                { 203, 18456, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.795572 43.325975)"), 13500, "Lakeshore & Elizabeth", 0 },
                { 205, 22231, "Markdale", (Point)new WKTReader().Read("SRID=4326;POINT (-80.652112 44.321688)"), 0, "Hwy 10 & Toronto St", 0 },
                { 207, 182781, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.553694 43.631319)"), 78500, "Dundas & Hwy 427 (Cloverdale)", 98825 },
                { 208, 126065, "Bancroft", (Point)new WKTReader().Read("SRID=4326;POINT (-77.859606 45.082316)"), 215750, "Hwy 62 & Hwy 28", 37500 },
                { 209, 348495, "Alliston", (Point)new WKTReader().Read("SRID=4326;POINT (-79.881914 44.149272)"), 162500, "Hwy 89 & Young", 6750 },
                { 210, 100282, "Kincardine", (Point)new WKTReader().Read("SRID=4326;POINT (-81.614967 44.176108)"), 30000, "Hwy 9 & Hwy 21", 21000 },
                { 211, 207647, "Kemptville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.63065 45.029906)"), 53250, "Hwy 416 & Hwy 43", 0 },
                { 212, 144778, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.748934 45.394105)"), 68875, "Richmond & Kirkwood", 205875 },
                { 213, 65298, "Dutton", (Point)new WKTReader().Read("SRID=4326;POINT (-81.503109 42.663159)"), 35500, "Main & Mary St", 0 },
                { 214, 278345, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.481871 43.629866)"), 91750, "Queensway & Park Lawn", 87375 },
                { 216, 87166, "Strathroy", (Point)new WKTReader().Read("SRID=4326;POINT (-81.609315 42.94709)"), 0, "Carroll & Caradoc", 21000 },
                { 217, 647547, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.372312 43.643023)"), 341050, "Queens Quay & Cooper Street", 288100 },
                { 218, 119761, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.577524 43.590598)"), 109100, "N Service & Dixie (Applewood Village)", 15375 },
                { 220, 36210, "Manitouwadge", (Point)new WKTReader().Read("SRID=4326;POINT (-85.826963 49.127158)"), 0, "Huron & Barber Walk", 0 },
                { 221, 313263, "Port Perry", (Point)new WKTReader().Read("SRID=4326;POINT (-78.962465 44.095062)"), 35250, "Scugog Line 6 & Hwy 7a", 66750 },
                { 222, 85860, "Bobcaygeon", (Point)new WKTReader().Read("SRID=4326;POINT (-78.541359 44.535991)"), 58500, "King St & Hwy 36", 0 },
                { 223, 61490, "Haliburton", (Point)new WKTReader().Read("SRID=4326;POINT (-78.507593 45.04641)"), 0, "Hwy 121 & 118", 53250 },
                { 224, 124872, "Beaverton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.155429 44.429022)"), 0, "Bay & Mill", 0 },
                { 225, 99142, "Elliot Lake", (Point)new WKTReader().Read("SRID=4326;POINT (-82.647973 46.386346)"), 0, "Hillside & Ontario St", 4875 },
                { 226, 321337, "Newmarket", (Point)new WKTReader().Read("SRID=4326;POINT (-79.479451 44.055685)"), 94150, "Yonge & Davis (Yonge - Davis Centre)", 75625 },
                { 227, 148008, "Midland", (Point)new WKTReader().Read("SRID=4326;POINT (-79.871759 44.729134)"), 72250, "King St & Heritage Dr", 52850 },
                { 228, 263907, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.314052 43.757795)"), 134050, "Ellesmere & Victoria Park (Parkway Mall)", 57750 },
                { 229, 146576, "Fergus", (Point)new WKTReader().Read("SRID=4326;POINT (-80.388228 43.710872)"), 54750, "Parkside & St. David", 29250 },
                { 230, 74169, "Keewatin", (Point)new WKTReader().Read("SRID=4326;POINT (-94.554131 49.761915)"), 0, "Front & Tenth St", 0 },
                { 231, 182373, "Havelock", (Point)new WKTReader().Read("SRID=4326;POINT (-77.889729 44.433577)"), 0, "Hwy 7 & 30", 10875 },
                { 232, 29326, "Burk's Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-79.402068 45.60989)"), 0, "Hwy 520 & Hwy 11", 0 },
                { 233, 394643, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.876617 43.23525)"), 103150, "Upper James & Fennell", 27000 },
                { 234, 50862, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.557104 43.675378)"), 90300, "Eglinton & Kipling", 67875 },
                { 236, 0, "Chelmsford", (Point)new WKTReader().Read("SRID=4326;POINT (-81.197967 46.579179)"), 106250, "Errington & Hwy144 1/2 Mile", 6750 },
                { 237, 451349, "Brooklin", (Point)new WKTReader().Read("SRID=4326;POINT (-78.960354 43.953441)"), 9750, "Winchester & Baldwin", 3000 },
                { 238, 66953, "Port Dover", (Point)new WKTReader().Read("SRID=4326;POINT (-80.201348 42.787693)"), 10500, "Hwy 6 (Main St ) & Market", 19875 },
                { 239, 12771, "Aylmer", (Point)new WKTReader().Read("SRID=4326;POINT (-80.987281 42.77169)"), 0, "Wellington & Hwy 3", 3000 },
                { 240, 0, "Port Rowan", (Point)new WKTReader().Read("SRID=4326;POINT (-80.451041 42.625424)"), 0, "Main & Centre St", 10875 },
                { 241, 52051, "Temagami", (Point)new WKTReader().Read("SRID=4326;POINT (-79.789803 47.063964)"), 0, "Hwy 11 & Wildflower St", 0 },
                { 242, 202863, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.248159 43.735622)"), 31500, "Brimley & Eglinton", 2250 },
                { 243, 262856, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.661437 45.366618)"), 126800, "Bank & Walkley", 35625 },
                { 244, 69058, "Wasaga Beach", (Point)new WKTReader().Read("SRID=4326;POINT (-80.074927 44.475126)"), 26800, "Mosley & 45th", 4125 },
                { 246, 8514, "Point Edward", (Point)new WKTReader().Read("SRID=4326;POINT (-82.406415 42.991183)"), 17250, "Lite & Louisa St", 0 },
                { 247, 14663, "Brighton", (Point)new WKTReader().Read("SRID=4326;POINT (-77.73746 44.041313)"), 3000, "Hwy 2 & Park", 33750 },
                { 248, 107145, "Richmond Hill", (Point)new WKTReader().Read("SRID=4326;POINT (-79.438573 43.880668)"), 104550, "Yonge St & Crosby Ave", 48750 },
                { 249, 84470, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.837052 43.228566)"), 32250, "Upper Gage & Fennell (Fennell Square)", 39000 },
                { 250, 78991, "Stoney Creek", (Point)new WKTReader().Read("SRID=4326;POINT (-79.746176 43.223518)"), 85950, "Gray's Rd & Hwy 8", 11625 },
                { 251, 101273, "Mactier", (Point)new WKTReader().Read("SRID=4326;POINT (-79.769767 45.134129)"), 33000, "Hwy 169 & 612", 0 },
                { 252, 533205, "St. Catharines", (Point)new WKTReader().Read("SRID=4326;POINT (-79.215152 43.136909)"), 182800, "Glendale & Merritt", 115275 },
                { 253, 130796, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.346141 43.736328)"), 82700, "Lawrence & Don Mills", 141750 },
                { 254, 216230, "Millbrook", (Point)new WKTReader().Read("SRID=4326;POINT (-78.448556 44.152089)"), 0, "Tupper & King St", 15000 },
                { 255, 353437, "Milton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.887433 43.508683)"), 18750, "Main & Bronte St", 10125 },
                { 256, 74294, "Massey", (Point)new WKTReader().Read("SRID=4326;POINT (-82.077072 46.211956)"), 8750, "Hwy 17 & Imperial N & S", 7500 },
                { 258, 48264, "Manitowaning", (Point)new WKTReader().Read("SRID=4326;POINT (-81.806883 45.744635)"), 0, "Queen & Spragge", 7500 },
                { 259, 0, "Coldwater", (Point)new WKTReader().Read("SRID=4326;POINT (-79.643273 44.706217)"), 6750, "Hwy 12 Sturgeon Bay Rd & Coldwater Rd", 0 },
                { 260, 263843, "Hanover", (Point)new WKTReader().Read("SRID=4326;POINT (-81.005478 44.155337)"), 36500, "10th St & 22nd Ave", 24375 },
                { 261, 77099, "Lakefield", (Point)new WKTReader().Read("SRID=4326;POINT (-78.270441 44.421208)"), 0, "Nichols & Water", 6000 },
                { 262, 15136, "Iroquois", (Point)new WKTReader().Read("SRID=4326;POINT (-75.316066 44.849493)"), 0, "Heritage Hwy 4 & 2", 7500 },
                { 263, 363366, "Bowmanville", (Point)new WKTReader().Read("SRID=4326;POINT (-78.712948 43.910332)"), 47050, "Hwy 2 & Green Rd", 47250 },
                { 264, 13244, "Lucan", (Point)new WKTReader().Read("SRID=4326;POINT (-81.40542 43.189028)"), 0, "Main & Market St", 3000 },
                { 265, 289277, "Bolton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.713523 43.862874)"), 124000, "Hwy 50 & King Side Rd", 54250 },
                { 267, 180978, "Creemore", (Point)new WKTReader().Read("SRID=4326;POINT (-80.103786 44.325607)"), 0, "Elizabeth & Mill St", 0 },
                { 268, 67413, "Caledonia", (Point)new WKTReader().Read("SRID=4326;POINT (-79.960103 43.064002)"), 24000, "Hwy 6 & Haddington St", 18750 },
                { 269, 90224, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.232619 43.757942)"), 33550, "Markham & Lawrence (Cedarbrae Mall)", 6000 },
                { 270, 333308, "Goderich", (Point)new WKTReader().Read("SRID=4326;POINT (-81.688327 43.729113)"), 22000, "Huron Rd & Suncoast Dr E", 16500 },
                { 271, 46394, "Wingham", (Point)new WKTReader().Read("SRID=4326;POINT (-81.309569 43.891445)"), 0, "Albert & Josephine", 15000 },
                { 272, 68028, "White River", (Point)new WKTReader().Read("SRID=4326;POINT (-85.280253 48.590535)"), 2800, "Elgin & Superior St", 0 },
                { 273, 168570, "Seaforth", (Point)new WKTReader().Read("SRID=4326;POINT (-81.392104 43.554741)"), 7500, "Main St N & Hwy 8 W", 17250 },
                { 274, 87532, "Zurich", (Point)new WKTReader().Read("SRID=4326;POINT (-81.618875 43.421454)"), 0, "Main & East St", 15750 },
                { 275, 127710, "Exeter", (Point)new WKTReader().Read("SRID=4326;POINT (-81.481383 43.352493)"), 0, "Main St & Wellington", 3000 },
                { 276, 159062, "Listowel", (Point)new WKTReader().Read("SRID=4326;POINT (-80.967868 43.73224)"), 12000, "Hwy 23 & 86", 17250 },
                { 277, 155617, "Forest", (Point)new WKTReader().Read("SRID=4326;POINT (-82.00093 43.096459)"), 25500, "Victoria & Main St", 15000 },
                { 278, 229852, "Tecumseh", (Point)new WKTReader().Read("SRID=4326;POINT (-82.867846 42.305608)"), 129750, "E.c. Row & Manning", 39750 },
                { 279, 364210, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.476349 43.708487)"), 125100, "Keele & Lawrence (North Park Plaza)", 57375 },
                { 280, 38340, "Elk Lake", (Point)new WKTReader().Read("SRID=4326;POINT (-80.338236 47.732113)"), 0, "Monroe Cres & First St", 7500 },
                { 281, 58194, "Omemee", (Point)new WKTReader().Read("SRID=4326;POINT (-78.559396 44.298734)"), 0, "King & Sturgeon St", 0 },
                { 282, 128183, "Clinton", (Point)new WKTReader().Read("SRID=4326;POINT (-81.537781 43.619947)"), 0, "Hwy 4 & Hwy 8", 21750 },
                { 283, 89460, "Tobermory", (Point)new WKTReader().Read("SRID=4326;POINT (-81.662331 45.255077)"), 0, "Brock & Bay St", 13500 },
                { 284, 143837, "Chesley", (Point)new WKTReader().Read("SRID=4326;POINT (-81.096343 44.303185)"), 0, "Bruce Rd 10 & First Ave", 15000 },
                { 285, 5680, "Emo", (Point)new WKTReader().Read("SRID=4326;POINT (-93.830154 48.633393)"), 0, "Hwy 11 & Hwy 71", 0 },
                { 286, 251026, "Kingsville", (Point)new WKTReader().Read("SRID=4326;POINT (-82.721484 42.038229)"), 27750, "Main & Jasperson", 41250 },
                { 287, 294516, "Sudbury", (Point)new WKTReader().Read("SRID=4326;POINT (-80.942472 46.521143)"), 51100, "Lasalle & Barrydowne (Supermall)", 66750 },
                { 288, 387581, "Barrie", (Point)new WKTReader().Read("SRID=4326;POINT (-79.690508 44.337044)"), 207100, "Mapleview & Bryne Dr", 52500 },
                { 289, 57233, "St. Marys", (Point)new WKTReader().Read("SRID=4326;POINT (-81.143045 43.260993)"), 0, "Wellington St & Parkview D", 11250 },
                { 290, 5203, "Port Loring", (Point)new WKTReader().Read("SRID=4326;POINT (-79.985582 45.916035)"), 0, "Hwy 522 & Loring Rd", 0 },
                { 293, 184092, "Arthur", (Point)new WKTReader().Read("SRID=4326;POINT (-80.533748 43.828141)"), 0, "Hwy 6 & 9", 7500 },
                { 294, 120420, "Glencoe", (Point)new WKTReader().Read("SRID=4326;POINT (-81.712869 42.746855)"), 12000, "Currie & Mckellar St", 15000 },
                { 295, 469256, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.488857 43.666103)"), 148150, "Dundas & Jane", 86625 },
                { 296, 150941, "Milverton", (Point)new WKTReader().Read("SRID=4326;POINT (-80.925532 43.560979)"), 2250, "Main St & Mill St", 15000 },
                { 297, 115186, "Acton", (Point)new WKTReader().Read("SRID=4326;POINT (-80.030937 43.634405)"), 0, "Young & Queen", 3375 },
                { 298, 471168, "Orangeville", (Point)new WKTReader().Read("SRID=4326;POINT (-80.087392 43.921029)"), 37150, "Broadway & Townline", 6750 },
                { 299, 77819, "Waterford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.293521 42.933728)"), 1500, "Alice Street & St James Stret South", 15000 },
                { 300, 287740, "Owen Sound", (Point)new WKTReader().Read("SRID=4326;POINT (-80.925958 44.574096)"), 43000, "9th Ave & 16th St", 76000 },
                { 301, 23186, "Almonte", (Point)new WKTReader().Read("SRID=4326;POINT (-76.193253 45.228398)"), 18750, "Queen & Ottawa", 0 },
                { 302, 55341, "Mount Forest", (Point)new WKTReader().Read("SRID=4326;POINT (-80.735317 43.979765)"), 0, "Hwy 6 & 89", 6750 },
                { 303, 184994, "Chesterville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.236764 45.101476)"), 0, "Hwy 43 & Queen St", 6000 },
                { 304, 116278, "Cardinal", (Point)new WKTReader().Read("SRID=4326;POINT (-75.382468 44.788355)"), 6000, "Hwy 2 & Dundas St", 0 },
                { 305, 70760, "Coboconk", (Point)new WKTReader().Read("SRID=4326;POINT (-78.798766 44.658301)"), 0, "Albert St & Hwy 35", 0 },
                { 306, 0, "Fenelon Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-78.738368 44.537622)"), 1500, "Hwy 121 & Colbourne St", 0 },
                { 307, 69546, "Kinmount", (Point)new WKTReader().Read("SRID=4326;POINT (-78.652935 44.784462)"), 0, "Hwy 503 & Hwy 121", 0 },
                { 308, 21767, "St. Isidore", (Point)new WKTReader().Read("SRID=4326;POINT (-74.906169 45.388587)"), 2100, "St. Isidore & Main St", 0 },
                { 309, 77141, "Maxville", (Point)new WKTReader().Read("SRID=4326;POINT (-74.854704 45.287733)"), 0, "Hwy 43 S & 417 N", 0 },
                { 310, 348503, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.293082 42.985575)"), 92500, "Wonderland & Oxford", 86700 },
                { 311, 63156, "Aurora", (Point)new WKTReader().Read("SRID=4326;POINT (-79.465497 43.989193)"), 109150, "Yonge & Brookland", 55500 },
                { 312, 67175, "Welland", (Point)new WKTReader().Read("SRID=4326;POINT (-79.227546 43.014899)"), 28000, "Primeway & Woodlawn", 27100 },
                { 313, 194999, "Wasaga Beach", (Point)new WKTReader().Read("SRID=4326;POINT (-80.020667 44.521529)"), 66000, "Mosley & Second", 7500 },
                { 314, 33110, "Killaloe", (Point)new WKTReader().Read("SRID=4326;POINT (-77.416195 45.555157)"), 0, "Hwy 512 & 62", 5250 },
                { 315, 43516, "Vermilion Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-93.37668 49.857343)"), 0, "Hwy 17 & Tower Rd", 0 },
                { 316, 62436, "Athens", (Point)new WKTReader().Read("SRID=4326;POINT (-75.950869 44.626357)"), 0, "Elgin St & Main St", 0 },
                { 317, 48719, "Durham", (Point)new WKTReader().Read("SRID=4326;POINT (-80.818647 44.176011)"), 0, "Hwy 4 & 6", 7500 },
                { 318, 87330, "Apsley", (Point)new WKTReader().Read("SRID=4326;POINT (-78.086427 44.753862)"), 9750, "Burleigh & Wellington", 0 },
                { 319, 21285, "Whitney", (Point)new WKTReader().Read("SRID=4326;POINT (-78.238291 45.492535)"), 0, "Ottawa St & Haycreek Rd", 24750 },
                { 320, 57871, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.345588 43.706395)"), 6750, "Overlea & Millwood (East York Twn Ctr)", 9200 },
                { 321, 77673, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.717738 43.582757)"), 23250, "Britannia & Queen (Streetsville)", 2625 },
                { 322, 12298, "Noelville", (Point)new WKTReader().Read("SRID=4326;POINT (-80.431114 46.139925)"), 0, "Hwy 64 & 535", 0 },
                { 323, 58188, "Pointe Au Baril", (Point)new WKTReader().Read("SRID=4326;POINT (-80.37376 45.595835)"), 39000, "Hwy 69 & 644", 0 },
                { 324, 112580, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.739381 45.386401)"), 15000, "Carling & Kirkwood (Hampton Park)", 13125 },
                { 325, 652620, "Guelph", (Point)new WKTReader().Read("SRID=4326;POINT (-80.257098 43.567376)"), 83050, "Speedvale & Stevenson", 37500 },
                { 326, 193877, "Waterdown", (Point)new WKTReader().Read("SRID=4326;POINT (-79.911195 43.316189)"), 95450, "Hwy 5 & Hwy 6", 94425 },
                { 327, 50138, "Hanmer", (Point)new WKTReader().Read("SRID=4326;POINT (-80.988345 46.651742)"), 15000, "Hwy 69 N & Elmview Dr", 0 },
                { 328, 168924, "Watford", (Point)new WKTReader().Read("SRID=4326;POINT (-81.87851 42.946976)"), 0, "Main & Huron St", 7500 },
                { 329, 205843, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.71936 43.67786)"), 162500, "Steeles & Hwy 410 (Orion Gate)", 57375 },
                { 330, 0, "Port Stanley", (Point)new WKTReader().Read("SRID=4326;POINT (-81.215073 42.664676)"), 18000, "Bridge & Carlow St", 0 },
                { 331, 53922, "Meaford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.59322 44.607515)"), 2250, "Nelson & Sykes St", 0 },
                { 332, 4615, "Nakina", (Point)new WKTReader().Read("SRID=4326;POINT (-86.709824 50.178307)"), 13500, "Quebec & Railroad St", 0 },
                { 333, 0, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.382178 43.64857)"), 0, "Bay & King (First Canadian Place)", 40125 },
                { 334, 434637, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.758153 43.581877)"), 157250, "Winston Churchill & Aquitaine/battleford", 57750 },
                { 335, 34080, "Kakabeka Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-89.614587 48.401442)"), 0, "Hwy 17 & Oliver Rd", 0 },
                { 336, 0, "Kearney", (Point)new WKTReader().Read("SRID=4326;POINT (-79.223887 45.55561)"), 8750, "Hwy 518 & Main St", 0 },
                { 337, 7095, "Courtright", (Point)new WKTReader().Read("SRID=4326;POINT (-82.472965 42.820428)"), 0, "Thompson St & St. Clair Parkway", 0 },
                { 339, 51447, "Mitchell", (Point)new WKTReader().Read("SRID=4326;POINT (-81.195866 43.467811)"), 3750, "Montreal & St. Andrew", 15000 },
                { 340, 347902, "Keswick", (Point)new WKTReader().Read("SRID=4326;POINT (-79.450846 44.234093)"), 58900, "Woodbine & Arlington", 11250 },
                { 341, 4257, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.412897 43.68894)"), 36750, "Spadina Rd & Lonsdale Rd", 84750 },
                { 343, 200558, "Peterborough", (Point)new WKTReader().Read("SRID=4326;POINT (-78.290023 44.293113)"), 16500, "Lansdowne E & Ashburnham", 3750 },
                { 344, 200603, "Cambridge", (Point)new WKTReader().Read("SRID=4326;POINT (-80.324554 43.419435)"), 123500, "Hwy 24 & Hwy 401", 7350 },
                { 345, 389996, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.474245 43.460617)"), 48000, "Victoria & Edna", 32250 },
                { 346, 72387, "Woodbridge", (Point)new WKTReader().Read("SRID=4326;POINT (-79.548541 43.791022)"), 479150, "Hwy 7 & Weston", 180000 },
                { 347, 58179, "Rosseau", (Point)new WKTReader().Read("SRID=4326;POINT (-79.640161 45.257571)"), 0, "Victoria St & Parry Sound", 0 },
                { 348, 58652, "Blenheim", (Point)new WKTReader().Read("SRID=4326;POINT (-81.998426 42.333479)"), 0, "Talbot St (Main) & Hwy 3", 0 },
                { 349, 34056, "Vankleek Hill", (Point)new WKTReader().Read("SRID=4326;POINT (-74.652612 45.518108)"), 3750, "Hwy 34 & Mill St", 24000 },
                { 350, 0, "Rodney", (Point)new WKTReader().Read("SRID=4326;POINT (-81.683625 42.568782)"), 15750, "Furnival Rd & Queen St", 0 },
                { 351, 267449, "Brockville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.688395 44.611199)"), 123750, "North Augusta & Parkdale", 66750 },
                { 352, 39259, "Elgin", (Point)new WKTReader().Read("SRID=4326;POINT (-76.228193 44.611684)"), 0, "Hwy 15 & Perth St", 0 },
                { 353, 132141, "Markham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.261589 43.883252)"), 7500, "Main St & Ramona Ave", 3000 },
                { 354, 37367, "New Hamburg", (Point)new WKTReader().Read("SRID=4326;POINT (-80.712122 43.386688)"), 0, "Hwy 7 & 8", 7500 },
                { 355, 276792, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.384523 43.769818)"), 166800, "Bayview & Sheppard (Bayview Village)", 184150 },
                { 358, 264394, "Peterborough", (Point)new WKTReader().Read("SRID=4326;POINT (-78.33922 44.33057)"), 76500, "Chemong & Milroy (Portage Place)", 70500 },
                { 359, 370639, "Sault Ste. Marie", (Point)new WKTReader().Read("SRID=4326;POINT (-84.318696 46.544079)"), 71750, "Great Northern & Second Line", 66750 },
                { 360, 296390, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.454742 43.732711)"), 227550, "Wilson & Dufferin", 138000 },
                { 361, 187451, "Stoney Creek", (Point)new WKTReader().Read("SRID=4326;POINT (-79.777475 43.174595)"), 70100, "Rymal & Upper Centennial Pkwy", 5000 },
                { 362, 110697, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.58492 43.741296)"), 59300, "Albion & Kipling (Albion Mall)", 75375 },
                { 363, 122281, "North Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-79.443499 46.272552)"), 15750, "Lakeshore & Marshall Park", 0 },
                { 364, 0, "Frankford", (Point)new WKTReader().Read("SRID=4326;POINT (-77.596785 44.201521)"), 28500, "Trent St & Mill St", 21750 },
                { 366, 211446, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.564741 43.683343)"), 58500, "Martingrove & The Westway", 7500 },
                { 367, 335695, "Windsor", (Point)new WKTReader().Read("SRID=4326;POINT (-83.005792 42.27416)"), 291150, "Howard & E.c. Row Expwy (Roundhouse Ctr)", 31500 },
                { 368, 48966, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.595315 45.318941)"), 14250, "Bank & Findlay Creek", 5625 },
                { 369, 132970, "Harriston", (Point)new WKTReader().Read("SRID=4326;POINT (-80.870413 43.914176)"), 7500, "Queen & Arthur", 14250 },
                { 370, 289280, "Lasalle", (Point)new WKTReader().Read("SRID=4326;POINT (-83.060377 42.249309)"), 95750, "Malden Rd & Elmdale Ave", 36750 },
                { 371, 188505, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.632494 43.508082)"), 165150, "Southdown & Royal Windsor (Clarkson)", 14625 },
                { 372, 88029, "Wheatley", (Point)new WKTReader().Read("SRID=4326;POINT (-82.453234 42.096998)"), 0, "Hwy 3 & Kent Rd", 0 },
                { 373, 212146, "Brantford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.24027 43.172631)"), 61000, "Lynden & Wayne Gretzky Pkwy", 25125 },
                { 374, 101722, "Parkhill", (Point)new WKTReader().Read("SRID=4326;POINT (-81.683157 43.165007)"), 0, "Hwy 81 & 7", 7500 },
                { 375, 96965, "Plantagenet", (Point)new WKTReader().Read("SRID=4326;POINT (-74.991629 45.539799)"), 0, "Old Hwy 17 & Hwy 17", 7500 },
                { 377, 22704, "Sharbot Lake", (Point)new WKTReader().Read("SRID=4326;POINT (-76.682527 44.797058)"), 0, "Hwy 7 & Hwy 38", 5250 },
                { 378, 343220, "Oshawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.875836 43.887284)"), 87250, "Gibb & Stevenson (Oshawa Centre)", 55125 },
                { 380, 76680, "Colborne", (Point)new WKTReader().Read("SRID=4326;POINT (-77.889212 44.005647)"), 0, "Hwy 2 & Church St", 6750 },
                { 381, 105952, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.451545 43.4192)"), 55500, "Fairway & Manitou", 4650 },
                { 382, 53223, "Cambridge", (Point)new WKTReader().Read("SRID=4326;POINT (-80.329675 43.352685)"), 23250, "Cedar & St Andrew (Westgate Centre)", 6000 },
                { 383, 76869, "Vaughan", (Point)new WKTReader().Read("SRID=4326;POINT (-79.488204 43.860582)"), 122050, "Dufferin & Major Mackenzie", 101625 },
                { 384, 723916, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.685463 45.410699)"), 24000, "Metcalfe & Isabella", 17250 },
                { 385, 229643, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.688563 43.61306)"), 280700, "Mavis & Britannia (Heartland Town Ctr)", 84000 },
                { 386, 184266, "Uxbridge", (Point)new WKTReader().Read("SRID=4326;POINT (-79.134989 44.086926)"), 11950, "Hwy 47 & Brock Rd N", 60000 },
                { 387, 72378, "North Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-79.467049 46.330624)"), 67500, "Hwy 11 N & Hwy 17 W", 69000 },
                { 388, 317904, "Ottawa-Nepean", (Point)new WKTReader().Read("SRID=4326;POINT (-75.816707 45.33018)"), 81500, "Robertson & Stafford (Bells Corners)", 23625 },
                { 389, 244305, "Kingston", (Point)new WKTReader().Read("SRID=4326;POINT (-76.542817 44.239286)"), 12000, "Queen Mary & Bath", 18000 },
                { 390, 177022, "Markham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.281836 43.874134)"), 212800, "Mccowan & Hwy 7", 74275 },
                { 391, 430991, "East Gwillimbury", (Point)new WKTReader().Read("SRID=4326;POINT (-79.482134 44.074053)"), 146450, "Yonge St & Green Lane", 77250 },
                { 392, 119196, "St. Catharines", (Point)new WKTReader().Read("SRID=4326;POINT (-79.269515 43.151753)"), 46600, "Vansickle & Fourth", 7700 },
                { 393, 447319, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.27371 43.027196)"), 209750, "Richmond & Fanshawe Pk (Masonville)", 23625 },
                { 394, 368489, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.769941 45.370018)"), 74850, "Carling & Woodroffe (Fairlawn Plaza)", 30750 },
                { 395, 206228, "Lively", (Point)new WKTReader().Read("SRID=4326;POINT (-81.14336 46.422817)"), 8750, "Rr 55 & Rr 1", 0 },
                { 397, 191812, "Vaughan", (Point)new WKTReader().Read("SRID=4326;POINT (-79.555689 43.844922)"), 140200, "Major Mackenzie & Weston", 88500 },
                { 398, 75216, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.576202 43.642134)"), 306350, "Burnhamthorpe & Hwy 427", 116250 },
                { 399, 89436, "Dorset", (Point)new WKTReader().Read("SRID=4326;POINT (-78.893521 45.246182)"), 0, "Hwy 117 & 35", 0 },
                { 400, 39732, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.62477 45.399256)"), 0, "St. Laurent & Smyth (Elmvale)", 6000 },
                { 401, 362344, "Niagara Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-79.098626 43.118836)"), 107900, "Portage Rd & Colborne E", 84200 },
                { 402, 196798, "Belleville", (Point)new WKTReader().Read("SRID=4326;POINT (-77.35656 44.167307)"), 0, "Dundas St E & Herchimer St", 0 },
                { 403, 29326, "Port Carling", (Point)new WKTReader().Read("SRID=4326;POINT (-79.579046 45.120953)"), 56100, "Hwy 118 & Medora St", 265500 },
                { 404, 161062, "Stouffville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.264833 43.968117)"), 93650, "Main St & Mostar St", 100500 },
                { 405, 0, "Azilda", (Point)new WKTReader().Read("SRID=4326;POINT (-81.118177 46.551696)"), 7000, "St. Agnes & Notre Dame", 0 },
                { 406, 83968, "Angus", (Point)new WKTReader().Read("SRID=4326;POINT (-79.890455 44.329193)"), 25500, "Simcoe Rd 10 & Commerce Rd", 24000 },
                { 407, 463540, "Sudbury", (Point)new WKTReader().Read("SRID=4326;POINT (-81.004646 46.441463)"), 129100, "Regent St & Long Lake Rd", 110100 },
                { 408, 24123, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.519274 43.758887)"), 53450, "Jane & Finch (York Gate Mall)", 0 },
                { 409, 0, "Killarney", (Point)new WKTReader().Read("SRID=4326;POINT (-81.511596 45.970298)"), 6750, "Hwy 637 & Channel St", 7500 },
                { 411, 129327, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.384245 43.664461)"), 125250, "Yonge & Wellesley", 58875 },
                { 412, 206884, "Ottawa-Nepean", (Point)new WKTReader().Read("SRID=4326;POINT (-75.727821 45.334306)"), 108200, "Hunt Club & Merivale (Nepean Crossroads)", 129800 },
                { 413, 34812, "Port Burwell", (Point)new WKTReader().Read("SRID=4326;POINT (-80.806491 42.646291)"), 0, "Hwy 19 & County Rd 42", 7500 },
                { 414, 72417, "Stirling", (Point)new WKTReader().Read("SRID=4326;POINT (-77.549103 44.299473)"), 6750, "Hwy 14 & Victoria", 6750 },
                { 415, 280360, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.518033 43.661292)"), 65950, "Royal York & Dundas (Humbertown)", 163500 },
                { 416, 194458, "Brampton-Bramalea", (Point)new WKTReader().Read("SRID=4326;POINT (-79.723509 43.712149)"), 239000, "Dixie & Hwy 7 (Bramalea City Centre)", 46875 },
                { 417, 288238, "Waterloo", (Point)new WKTReader().Read("SRID=4326;POINT (-80.533908 43.501119)"), 163250, "King N & Northfield", 126000 },
                { 418, 51084, "Erin", (Point)new WKTReader().Read("SRID=4326;POINT (-80.061036 43.76842)"), 0, "Main & Millwood", 0 },
                { 419, 53449, "Shelburne", (Point)new WKTReader().Read("SRID=4326;POINT (-80.190564 44.081322)"), 0, "Hwy 10 19 & 24", 0 },
                { 420, 145965, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.474313 43.651766)"), 29750, "Bloor & Runnymede (Bloor W Village)", 57750 },
                { 421, 8520, "Bruce Mines", (Point)new WKTReader().Read("SRID=4326;POINT (-83.788274 46.301174)"), 0, "Hwy 17 & 561", 0 },
                { 423, 18920, "Lansdowne", (Point)new WKTReader().Read("SRID=4326;POINT (-76.018404 44.400403)"), 0, "Hwy 3 & King St", 0 },
                { 424, 17028, "Moosonee", (Point)new WKTReader().Read("SRID=4326;POINT (-80.646483 51.274234)"), 0, "Main & Bay St", 0 },
                { 425, 49533, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.601499 43.611589)"), 83800, "Bloor & Tomken Rd", 3375 },
                { 426, 109561, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.518123 43.623237)"), 98750, "Queensway & Islington", 7500 },
                { 427, 356907, "Chatham", (Point)new WKTReader().Read("SRID=4326;POINT (-82.219818 42.431646)"), 141750, "St. Clair & Pioneer Line", 28500 },
                { 428, 39732, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.204709 43.796802)"), 135000, "Morningside & 401", 6750 },
                { 429, 125658, "Burford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.427241 43.103005)"), 0, "King St & Park St", 0 },
                { 430, 24133, "Hagersville", (Point)new WKTReader().Read("SRID=4326;POINT (-80.050147 42.963407)"), 18750, "King St & Indian Line", 15000 },
                { 431, 123481, "Ottawa-Nepean", (Point)new WKTReader().Read("SRID=4326;POINT (-75.758589 45.35115)"), 86150, "Woodroffe & Baseline (College Square)", 64500 },
                { 432, 216404, "Whitby", (Point)new WKTReader().Read("SRID=4326;POINT (-78.960232 43.915717)"), 140700, "Taunton & Brock", 103125 },
                { 433, 2838, "Calabogie", (Point)new WKTReader().Read("SRID=4326;POINT (-76.731283 45.294299)"), 0, "Hwy 508 & Mill St", 0 },
                { 434, 245258, "Pickering", (Point)new WKTReader().Read("SRID=4326;POINT (-79.113145 43.818841)"), 116250, "Whites & Kingston (Hwy 2)", 26250 },
                { 435, 93181, "Long Sault", (Point)new WKTReader().Read("SRID=4326;POINT (-74.894879 45.029104)"), 10500, "Hwy 2 & Moulinette Rd", 3750 },
                { 436, 110209, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.636611 43.719396)"), 48850, "Derry Rd & Goreway Dr", 0 },
                { 437, 217218, "Oakville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.706257 43.411219)"), 44250, "3rd Line & Rebecca (Hopedale Mall)", 12375 },
                { 438, 281121, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.280232 42.937682)"), 22250, "Wonderland & Southdale (Power Centre)", 82050 },
                { 439, 22231, "Portland", (Point)new WKTReader().Read("SRID=4326;POINT (-76.188894 44.698698)"), 0, "Colborne St & Campbell St", 8250 },
                { 440, 80931, "Lucknow", (Point)new WKTReader().Read("SRID=4326;POINT (-81.515823 43.959391)"), 0, "Inglis St & Willoughby St", 15000 },
                { 441, 33110, "Casselman", (Point)new WKTReader().Read("SRID=4326;POINT (-75.082647 45.308057)"), 15000, "Hwy 417 & St. Albert St", 23250 },
                { 442, 96998, "Cobden", (Point)new WKTReader().Read("SRID=4326;POINT (-76.886549 45.627543)"), 0, "Truelove St - At A Dead End", 1875 },
                { 443, 178770, "Ottawa-Gloucester", (Point)new WKTReader().Read("SRID=4326;POINT (-75.608824 45.433671)"), 108250, "Blair & Ogilvie", 175125 },
                { 444, 118610, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.289742 43.784748)"), 42750, "Kennedy & Sheppard (Agincourt Mall)", 15750 },
                { 445, 342509, "Oakville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.719921 43.485081)"), 259150, "Dundas & Trafalgar (Oak Park)", 191625 },
                { 446, 121335, "Beamsville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.47595 43.166706)"), 15750, "Hwy 8 & Ontario St", 15000 },
                { 447, 68160, "Wilberforce", (Point)new WKTReader().Read("SRID=4326;POINT (-78.225147 45.036621)"), 0, "Hwy 648 & Esson Rd", 0 },
                { 448, 36894, "Honey Harbour", (Point)new WKTReader().Read("SRID=4326;POINT (-79.822416 44.868637)"), 0, "Hwy 69 & Muskoka Rd 5", 0 },
                { 449, 96965, "Wellington", (Point)new WKTReader().Read("SRID=4326;POINT (-77.365314 43.949099)"), 18000, "Main & Prince Edward Dr", 6750 },
                { 450, 8514, "Dundalk", (Point)new WKTReader().Read("SRID=4326;POINT (-80.379855 44.178136)"), 0, "County Rd 9 & Hwy 10", 0 },
                { 452, 177342, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.419446 43.730987)"), 222100, "Avenue & Lawrence", 205550 },
                { 453, 381209, "Newmarket", (Point)new WKTReader().Read("SRID=4326;POINT (-79.430294 44.062447)"), 60100, "Leslie & Davis", 54750 },
                { 454, 293995, "Port Elgin", (Point)new WKTReader().Read("SRID=4326;POINT (-81.397592 44.426714)"), 62500, "Iving & Goderich St", 12000 },
                { 455, 49682, "Carp", (Point)new WKTReader().Read("SRID=4326;POINT (-76.03782 45.344742)"), 22500, "Carp Rd & Hwy 17", 3000 },
                { 456, 177390, "Georgetown", (Point)new WKTReader().Read("SRID=4326;POINT (-79.877025 43.631164)"), 20250, "Mountainview Rd & Argyll Rd", 10125 },
                { 457, 202288, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.400041 43.707278)"), 14400, "Yonge & Eglinton (Yonge Eglinton Ctr)", 48225 },
                { 458, 57510, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.621378 43.58273)"), 115750, "Hurontario & Dundas (Hwys 10 & 5)", 5250 },
                { 459, 165521, "Sarnia", (Point)new WKTReader().Read("SRID=4326;POINT (-82.346646 42.986497)"), 47750, "Exmouth & Lambton Mall Rd", 31350 },
                { 460, 0, "Delta", (Point)new WKTReader().Read("SRID=4326;POINT (-76.120935 44.612085)"), 0, "Hwy 42 & Main St", 18750 },
                { 461, 122980, "Elora", (Point)new WKTReader().Read("SRID=4326;POINT (-80.428891 43.680019)"), 6750, "Metcalfe & Mill St", 10600 },
                { 462, 49880, "Smithville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.537957 43.09534)"), 0, "Hwy 20 & Industrial Park Rd", 14250 },
                { 463, 13723, "Mattice", (Point)new WKTReader().Read("SRID=4326;POINT (-83.259752 49.611265)"), 7500, "Hwy 11 & King St", 0 },
                { 464, 51471, "Schreiber", (Point)new WKTReader().Read("SRID=4326;POINT (-87.264104 48.807961)"), 0, "Manitoba & Winnipeg St", 0 },
                { 465, 236908, "Oshawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.844016 43.942808)"), 47250, "Harmony & Taunton (Harmony Shopping Ctr)", 63375 },
                { 466, 103114, "Alfred", (Point)new WKTReader().Read("SRID=4326;POINT (-74.889832 45.556377)"), 7500, "Philipe St & Hwy 17", 15000 },
                { 467, 110007, "St. Catharines", (Point)new WKTReader().Read("SRID=4326;POINT (-79.243746 43.204704)"), 23250, "Lakeshore Rd & Geneva St", 21750 },
                { 468, 51084, "Bridgenorth", (Point)new WKTReader().Read("SRID=4326;POINT (-78.383232 44.381869)"), 0, "Ward St & Causeway", 0 },
                { 469, 264792, "Washago", (Point)new WKTReader().Read("SRID=4326;POINT (-79.336768 44.753422)"), 22800, "Hwy 169 & Hwy 11", 6750 },
                { 470, 141427, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.816331 43.683127)"), 133200, "Worthington & Bovaird", 3000 },
                { 471, 91310, "Kirkfield", (Point)new WKTReader().Read("SRID=4326;POINT (-78.982705 44.561861)"), 0, "Hwy 48 & Kirkfield Rd (County Rd 6)", 0 },
                { 472, 56314, "Warkworth", (Point)new WKTReader().Read("SRID=4326;POINT (-77.886495 44.201904)"), 6750, "Main & Church St", 0 },
                { 473, 51120, "Goulais River", (Point)new WKTReader().Read("SRID=4326;POINT (-84.349209 46.733879)"), 6750, "Hwy 17 N", 0 },
                { 474, 99592, "Rossmore", (Point)new WKTReader().Read("SRID=4326;POINT (-77.38266 44.137691)"), 0, "Baybridge Rd & Hwy 28", 6750 },
                { 475, 35948, "Sydenham", (Point)new WKTReader().Read("SRID=4326;POINT (-76.597204 44.410098)"), 0, "George St & Mill St", 17250 },
                { 476, 8514, "Denbigh", (Point)new WKTReader().Read("SRID=4326;POINT (-77.266844 45.128207)"), 0, "Hwy 41 & 28", 7500 },
                { 477, 228010, "Nobleton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.652153 43.908167)"), 45950, "Hwy 27 & Sheardown Rd", 48000 },
                { 478, 58203, "Maynooth", (Point)new WKTReader().Read("SRID=4326;POINT (-77.936555 45.231182)"), 0, "Hwy 62 & 127 N", 0 },
                { 479, 61987, "Lanark", (Point)new WKTReader().Read("SRID=4326;POINT (-76.365142 45.018344)"), 0, "George & Clarence St", 0 },
                { 481, 270679, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.818948 43.330691)"), 98100, "Fairview & Maple (Maple Mews)", 37875 },
                { 482, 26015, "Port Severn", (Point)new WKTReader().Read("SRID=4326;POINT (-79.716765 44.804643)"), 0, "Hwy 400 & Port Severn Rd", 0 },
                { 483, 53922, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.380152 43.646527)"), 17850, "Front & Bay (Royal Bank Plaza)", 44250 },
                { 484, 182352, "Sault Ste. Marie", (Point)new WKTReader().Read("SRID=4326;POINT (-84.365473 46.536837)"), 14250, "Korah Rd & Second Line", 0 },
                { 485, 384324, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.328859 43.696878)"), 125900, "Coxwell Ave & O'connor", 106175 },
                { 486, 403473, "Oakville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.678496 43.457771)"), 106850, "Cornwall & Trafalgar", 499500 },
                { 487, 76626, "Iron Bridge", (Point)new WKTReader().Read("SRID=4326;POINT (-83.214167 46.277089)"), 0, "Hwy 17 & James St", 0 },
                { 488, 176998, "St. Charles", (Point)new WKTReader().Read("SRID=4326;POINT (-80.415143 46.362694)"), 0, "Hwy 535 & King St", 0 },
                { 489, 249883, "Newcastle", (Point)new WKTReader().Read("SRID=4326;POINT (-78.58988 43.916342)"), 60750, "King & Church", 6750 },
                { 490, 315973, "Windsor", (Point)new WKTReader().Read("SRID=4326;POINT (-82.938778 42.315283)"), 252750, "Tecumseh & Lauzon", 29250 },
                { 491, 217937, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.554074 43.425411)"), 99750, "Ira Needles & Highland", 31450 },
                { 492, 96052, "Callander", (Point)new WKTReader().Read("SRID=4326;POINT (-79.365258 46.222824)"), 23250, "Landsdown & Main St", 0 },
                { 494, 251367, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.674557 43.527915)"), 139550, "Dundas & Winston Churchill (Woodchester)", 12000 },
                { 495, 250169, "Guelph", (Point)new WKTReader().Read("SRID=4326;POINT (-80.239432 43.514116)"), 27000, "Scottsdale & Stone Rd W", 3375 },
                { 496, 105771, "Richmond", (Point)new WKTReader().Read("SRID=4326;POINT (-75.842545 45.193561)"), 1500, "Perth St & Eagleson Rd", 0 },
                { 497, 259717, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.821629 43.367035)"), 62050, "Guelph Line & Upper Middle Rd", 38675 },
                { 499, 371097, "Ottawa-Nepean", (Point)new WKTReader().Read("SRID=4326;POINT (-75.741798 45.268906)"), 213100, "Strandherd & Greenbank (Barrhaven)", 124375 },
                { 500, 459015, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.685693 45.399647)"), 119350, "Bank & Holmwood (Lansdowne Pk)", 31125 },
                { 501, 199696, "Thunder Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-89.251311 48.452022)"), 60400, "Dawson & Hwy 11/17 (Landmark Inn)", 58125 },
                { 502, 127786, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.297515 42.90956)"), 33000, "Hwys 2 & 4 (Lambeth)", 0 },
                { 503, 23783, "Plevna", (Point)new WKTReader().Read("SRID=4326;POINT (-76.985263 44.960931)"), 0, "Main & Buckshot St", 0 },
                { 504, 139101, "Norwood", (Point)new WKTReader().Read("SRID=4326;POINT (-77.981275 44.381446)"), 0, "Hwy 7 & 45", 7500 },
                { 505, 181801, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.653093 43.608283)"), 122600, "Hwy 10 & Eglinton", 54000 },
                { 506, 40825, "Earlton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.82462 47.710781)"), 0, "10th Ave & 10th St", 0 },
                { 507, 19866, "Garson", (Point)new WKTReader().Read("SRID=4326;POINT (-80.876551 46.548996)"), 0, "Regional Rd 86 (Falconbridge Hwy)", 0 },
                { 509, 202218, "Fonthill", (Point)new WKTReader().Read("SRID=4326;POINT (-79.278253 43.047778)"), 13500, "Hwy 20 & Rice Rd", 31500 },
                { 510, 197735, "Langton", (Point)new WKTReader().Read("SRID=4326;POINT (-80.58003 42.745567)"), 31250, "Hwy 59", 15000 },
                { 511, 322793, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.394611 43.645332)"), 535400, "Front & Spadina", 13500 },
                { 512, 32411, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.385919 43.67083)"), 6000, "Yonge & Bloor (Hudson Bay Centre)", 3375 },
                { 513, 36894, "Stayner", (Point)new WKTReader().Read("SRID=4326;POINT (-80.094378 44.41544)"), 8250, "Hwy 26 & Airport Rd", 0 },
                { 514, 21300, "Magnetawan", (Point)new WKTReader().Read("SRID=4326;POINT (-79.642827 45.664873)"), 0, "Hwy 520 & Spark St", 0 },
                { 515, 40988, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.67599 45.386187)"), 6750, "Bank & Riverside (Billingsbridge)", 0 },
                { 516, 54395, "Baysville", (Point)new WKTReader().Read("SRID=4326;POINT (-79.119253 45.148186)"), 0, "Brunel Rd & Regional Rd 117", 0 },
                { 517, 0, "Amherstview", (Point)new WKTReader().Read("SRID=4326;POINT (-76.644927 44.219366)"), 1500, "Manitou Cres & Sherwood Ave", 7500 },
                { 519, 85200, "Echo Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-84.071099 46.48566)"), 0, "Hwy 17 E & Church St", 0 },
                { 520, 12352, "Seeley's Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-76.238092 44.474011)"), 0, "Hwy 15 & 32", 0 },
                { 521, 32646, "Ottawa-Manotick", (Point)new WKTReader().Read("SRID=4326;POINT (-75.682882 45.223493)"), 10500, "Rideau Valley Dr & County R", 16500 },
                { 522, 277080, "St. Catharines", (Point)new WKTReader().Read("SRID=4326;POINT (-79.242217 43.178879)"), 131050, "Geneva & Scott (Fairview Mall)", 125550 },
                { 523, 128073, "Thornhill", (Point)new WKTReader().Read("SRID=4326;POINT (-79.450057 43.808412)"), 216150, "Bathurst & Centre (The Promenade)", 124700 },
                { 524, 52509, "Hagar", (Point)new WKTReader().Read("SRID=4326;POINT (-80.415887 46.454616)"), 0, "Hwy 17 & 535", 0 },
                { 526, 156563, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.693019 45.411435)"), 60000, "Bank & Mcleod", 3750 },
                { 527, 46833, "Dorchester", (Point)new WKTReader().Read("SRID=4326;POINT (-81.048852 42.985145)"), 15750, "Hamilton Rd & Oakwood Dr", 0 },
                { 528, 236923, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.388134 43.669186)"), 138000, "Bay & Bloor (Manulife Centre)", 186750 },
                { 529, 225868, "Tottenham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.803837 44.019869)"), 39200, "Hwy 9 & Tottenham Rd", 0 },
                { 530, 172607, "Tavistock", (Point)new WKTReader().Read("SRID=4326;POINT (-80.83923 43.320393)"), 12000, "William St & Hope St", 15000 },
                { 532, 282912, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.372204 43.648832)"), 79750, "Front St & Jarvis St", 172875 },
                { 533, 90278, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.138816 43.779723)"), 19500, "Lawrence & Pt Union Rd", 3750 },
                { 534, 210371, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.450751 43.655339)"), 93550, "Dundas & Bloor (Roncesvalles)", 170250 },
                { 536, 243524, "Maple", (Point)new WKTReader().Read("SRID=4326;POINT (-79.534514 43.849132)"), 192750, "Major Mackenzie & Jane", 71250 },
                { 537, 334237, "Thornbury", (Point)new WKTReader().Read("SRID=4326;POINT (-80.456422 44.56323)"), 24000, "Arthur St W & Elma St N", 9750 },
                { 538, 40688, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.691346 45.425672)"), 0, "Rideau & Colonel By Dr (Rideau Centre)", 3000 },
                { 539, 296000, "Windsor", (Point)new WKTReader().Read("SRID=4326;POINT (-83.057322 42.287819)"), 70000, "Tecumseh & Huron Line (Ambassador Plaza)", 10500 },
                { 540, 181159, "Cornwall", (Point)new WKTReader().Read("SRID=4326;POINT (-74.74604 45.02481)"), 65750, "Cumberland St & Ninth St W", 29250 },
                { 541, 63900, "Brussels", (Point)new WKTReader().Read("SRID=4326;POINT (-81.250594 43.742657)"), 0, "Country Rd 16 & Turnberry St", 20250 },
                { 542, 92851, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.631294 43.568007)"), 150250, "Dundas & Mavis", 54375 },
                { 544, 204204, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.44389 43.449534)"), 71300, "Ottawa & River (Stanley Park Plaza)", 25100 },
                { 545, 352135, "Stoney Creek", (Point)new WKTReader().Read("SRID=4326;POINT (-79.763353 43.231879)"), 159850, "Centennial & Queenston (Eastgate Sq)", 22500 },
                { 546, 66451, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.344984 43.778596)"), 54750, "Don Mills & Sheppard (Fairview Mall)", 49975 },
                { 547, 45538, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.692859 45.419751)"), 27000, "Nepean St & Elgin St", 66750 },
                { 548, 0, "Belmont", (Point)new WKTReader().Read("SRID=4326;POINT (-81.084692 42.882252)"), 12000, "Caesar Rd & Main St (Hwy 74)", 14250 },
                { 549, 355203, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.510003 43.439161)"), 72750, "Highland & Westmount (Highland Rd Plaza)", 26625 },
                { 550, 147889, "Bayfield", (Point)new WKTReader().Read("SRID=4326;POINT (-81.691447 43.555135)"), 0, "Mill Rd & Hwy 21", 0 },
                { 551, 318357, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.823983 43.392909)"), 96750, "Walkers Line & Dundas St", 103875 },
                { 552, 24366, "Val Caron", (Point)new WKTReader().Read("SRID=4326;POINT (-81.00666 46.612928)"), 8750, "Hwy 64 & 575", 0 },
                { 553, 221976, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.557711 43.614667)"), 143250, "Queensway & 427 (Near Sherway)", 127875 },
                { 554, 193088, "Richmond Hill", (Point)new WKTReader().Read("SRID=4326;POINT (-79.451574 43.9271)"), 243700, "Yonge St & Stouffville Rd", 91125 },
                { 555, 179282, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.721233 43.761869)"), 142050, "Airport & Bovaird", 9825 },
                { 556, 332885, "Ottawa-Kanata", (Point)new WKTReader().Read("SRID=4326;POINT (-75.921198 45.310769)"), 88500, "Terry Fox & Campeau", 142125 },
                { 557, 74300, "Dwight", (Point)new WKTReader().Read("SRID=4326;POINT (-78.994228 45.322098)"), 0, "Hwy 35 & 60", 0 },
                { 558, 87978, "Norwich", (Point)new WKTReader().Read("SRID=4326;POINT (-80.605137 42.986185)"), 0, "Florence & Main St W", 15000 },
                { 559, 165568, "Brantford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.281067 43.183198)"), 0, "King George Rd & Powerline Rd", 5625 },
                { 560, 15609, "Victoria Harbour", (Point)new WKTReader().Read("SRID=4326;POINT (-79.776052 44.750948)"), 0, "John & Albert St", 0 },
                { 561, 51120, "Buckhorn", (Point)new WKTReader().Read("SRID=4326;POINT (-78.34912 44.556885)"), 15750, "Kings Hwy 36 & 507", 0 },
                { 562, 41636, "Cannington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.03771 44.350178)"), 8250, "Hwy 12 & Cameron St E", 0 },
                { 563, 77819, "Oshawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.849389 43.919396)"), 8250, "Rossland & Wilson", 2250 },
                { 564, 217596, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.329518 43.794196)"), 45750, "Victoria Park & Finch", 20625 },
                { 566, 214530, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.785752 43.355415)"), 55300, "Fairview & Walkers Line(Woodview Place)", 15750 },
                { 567, 64349, "Thamesville", (Point)new WKTReader().Read("SRID=4326;POINT (-81.983511 42.554901)"), 0, "Hwy 2 & 21", 7500 },
                { 568, 149880, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.38154 43.656375)"), 79500, "Yonge & Dundas (Atrium On Bay)", 65625 },
                { 569, 255644, "Stittsville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.931569 45.272075)"), 15600, "Hazeldean & Main", 85500 },
                { 570, 183138, "Warsaw", (Point)new WKTReader().Read("SRID=4326;POINT (-78.137576 44.430744)"), 0, "Water & Mill St", 3750 },
                { 571, 192758, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.869252 43.257188)"), 39000, "King & Bay (Jackson Sq)", 18000 },
                { 573, 372672, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.791656 43.393276)"), 124950, "Appleby & Upper Middle Rd (Millcroft)", 12000 },
                { 575, 343671, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.224335 42.934883)"), 178500, "Wellington & Bradley", 65950 },
                { 576, 78518, "Coniston", (Point)new WKTReader().Read("SRID=4326;POINT (-80.846237 46.494454)"), 8750, "Hwy 17 & 2nd Ave", 12750 },
                { 578, 15136, "Winchester", (Point)new WKTReader().Read("SRID=4326;POINT (-75.361153 45.090752)"), 14250, "Main St & Hwy 31", 0 },
                { 579, 481106, "Orillia", (Point)new WKTReader().Read("SRID=4326;POINT (-79.439299 44.609442)"), 67250, "Hwy 12b & 11", 102000 },
                { 580, 155978, "Markham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.322762 43.856606)"), 90300, "Hwy 7 & Birchmount/village Pkwy", 96375 },
                { 581, 188757, "Brechin", (Point)new WKTReader().Read("SRID=4326;POINT (-79.175247 44.544743)"), 0, "Hwy 12 & Gladstone Ave", 1500 },
                { 582, 40678, "Sauble Beach", (Point)new WKTReader().Read("SRID=4326;POINT (-81.263301 44.630174)"), 0, "Main & Sauble Falls Pkwy", 0 },
                { 583, 57733, "Grand Valley", (Point)new WKTReader().Read("SRID=4326;POINT (-80.31515 43.898319)"), 0, "Hwy 9 & 25", 12000 },
                { 584, 50138, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.270122 43.808387)"), 57750, "Mccowan & Finch (Woodside Square)", 3750 },
                { 585, 172131, "Markham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.26682 43.902147)"), 49000, "Markham Rd & Bur Oak", 88500 },
                { 586, 269001, "Niagara Falls", (Point)new WKTReader().Read("SRID=4326;POINT (-79.083271 43.089732)"), 104050, "Ferry & Stanley", 23875 },
                { 587, 428571, "Whitby", (Point)new WKTReader().Read("SRID=4326;POINT (-78.912676 43.884553)"), 84000, "Thickson & Dundas", 66375 },
                { 588, 63900, "Verner", (Point)new WKTReader().Read("SRID=4326;POINT (-80.128866 46.413394)"), 0, "Hwy 17 W & Cote St", 14250 },
                { 589, 17028, "Ottawa-Orleans", (Point)new WKTReader().Read("SRID=4326;POINT (-75.537158 45.479757)"), 19500, "Orleans & Jeanne D'arc (Convent Glen)", 0 },
                { 590, 323421, "Markham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.356244 43.849736)"), 859250, "Hwy 7 & Woodbine", 94875 },
                { 591, 6622, "Reeces Corners", (Point)new WKTReader().Read("SRID=4326;POINT (-82.120257 42.978911)"), 0, "Se Corner Oil Heritage & Lambton County", 0 },
                { 593, 75991, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.330953 42.958102)"), 31500, "Commissioners W & Boler (Byron)", 7400 },
                { 594, 38801, "Bewdley", (Point)new WKTReader().Read("SRID=4326;POINT (-78.321134 44.089264)"), 0, "Rice Lake Dr & Lake St", 0 },
                { 595, 65274, "Dunchurch", (Point)new WKTReader().Read("SRID=4326;POINT (-79.853979 45.645036)"), 750, "Hwy 124 & Hwy 520", 0 },
                { 596, 122980, "Pefferlaw", (Point)new WKTReader().Read("SRID=4326;POINT (-79.204572 44.314384)"), 7500, "Hwy 48 & Pefferlaw Rd", 6750 },
                { 597, 14190, "Beachburg", (Point)new WKTReader().Read("SRID=4326;POINT (-76.863643 45.736012)"), 0, "Beachburg Rd & Lapasse Rd", 0 },
                { 598, 62935, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.638495 45.42299)"), 6750, "St. Laurent & Queensway", 9000 },
                { 599, 72616, "Caledon", (Point)new WKTReader().Read("SRID=4326;POINT (-79.867195 43.869953)"), 9500, "Church St & Airport Rd", 3250 },
                { 601, 53449, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.755194 43.645438)"), 123850, "Mavis & Steeles", 4125 },
                { 602, 352067, "Thunder Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-89.306642 48.381438)"), 57500, "Arthur St W & Neebing W", 50625 },
                { 604, 34080, "Alban", (Point)new WKTReader().Read("SRID=4326;POINT (-80.63249 46.104286)"), 0, "Hwy 69 & 64", 0 },
                { 605, 174110, "Innisfil", (Point)new WKTReader().Read("SRID=4326;POINT (-79.574288 44.30848)"), 89350, "Innisfil Beach Rd & 20th Sr", 63375 },
                { 607, 34074, "Gooderham", (Point)new WKTReader().Read("SRID=4326;POINT (-78.380441 44.906736)"), 0, "Hwy 503 & 507", 0 },
                { 609, 271838, "Cobourg", (Point)new WKTReader().Read("SRID=4326;POINT (-78.20209 43.970393)"), 33000, "Elgin St W & Rogers Rd", 119150 },
                { 610, 96079, "Teeswater", (Point)new WKTReader().Read("SRID=4326;POINT (-81.285127 43.993415)"), 0, "Industrial Rd & Clinton St", 16700 },
                { 611, 8514, "Pontypool", (Point)new WKTReader().Read("SRID=4326;POINT (-78.628631 44.100444)"), 0, "John & Manvers E", 15000 },
                { 612, 57706, "Schomberg", (Point)new WKTReader().Read("SRID=4326;POINT (-79.677855 44.003665)"), 700, "Hwy 27 & 9", 50 },
                { 613, 26488, "Sundridge", (Point)new WKTReader().Read("SRID=4326;POINT (-79.393853 45.770328)"), 32250, "Hwy 124 & Paget St", 0 },
                { 614, 238601, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.380276 43.662111)"), 64500, "Carlton & Church", 88125 },
                { 615, 80931, "Paisley", (Point)new WKTReader().Read("SRID=4326;POINT (-81.273244 44.307375)"), 0, "County Rd 3 & 10", 7500 },
                { 616, 151506, "Thunder Bay", (Point)new WKTReader().Read("SRID=4326;POINT (-89.23929 48.401843)"), 24000, "Ft William & Main (Thunder Centre)", 38600 },
                { 617, 335434, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.482954 43.424257)"), 55250, "Ottawa & Homer Watson (Alpine Centre)", 75750 },
                { 618, 77120, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.47892 43.78384)"), 115950, "Steeles & Dufferin", 51375 },
                { 619, 386874, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.711446 43.555323)"), 181550, "Erin Mills & Eglinton", 117750 },
                { 621, 145184, "Mildmay", (Point)new WKTReader().Read("SRID=4326;POINT (-81.110673 44.039001)"), 3750, "Provincial Hwy 9 & Absalom St", 20250 },
                { 622, 80437, "Hensall", (Point)new WKTReader().Read("SRID=4326;POINT (-81.500444 43.44058)"), 0, "Hwy 4 & 84", 7500 },
                { 623, 272714, "Richmond Hill", (Point)new WKTReader().Read("SRID=4326;POINT (-79.429852 43.842391)"), 191550, "Yonge & Hwy 7", 101700 },
                { 624, 593059, "Ottawa-Orleans", (Point)new WKTReader().Read("SRID=4326;POINT (-75.496516 45.456012)"), 196650, "Innes & Tenth Line", 406500 },
                { 626, 12780, "Lafontaine", (Point)new WKTReader().Read("SRID=4326;POINT (-80.052909 44.758582)"), 0, "Conc 16 & County Rd 26", 0 },
                { 627, 75265, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.579492 43.611843)"), 65550, "Dixie & Dundas St E", 8625 },
                { 629, 118024, "Richmond Hill", (Point)new WKTReader().Read("SRID=4326;POINT (-79.392598 43.880632)"), 95150, "Major Mackenzie & Leslie", 89250 },
                { 630, 188876, "Aurora", (Point)new WKTReader().Read("SRID=4326;POINT (-79.449008 44.020378)"), 191450, "Bayview & St John", 108425 },
                { 631, 30278, "Milton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.867861 43.526687)"), 95900, "Main St & Thompson Rd", 63000 },
                { 632, 206562, "Woodbridge", (Point)new WKTReader().Read("SRID=4326;POINT (-79.625249 43.784507)"), 278000, "Hwy 27 & Innovation Dr", 142500 },
                { 633, 355442, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.539334 43.594075)"), 149500, "Lakeshore Blvd W & Browns Line", 67500 },
                { 634, 165823, "Guelph", (Point)new WKTReader().Read("SRID=4326;POINT (-80.187842 43.501666)"), 15000, "Clair & Gordon", 18375 },
                { 635, 191574, "Maple", (Point)new WKTReader().Read("SRID=4326;POINT (-79.459419 43.848648)"), 94400, "Bathurst & Rutherford", 45000 },
                { 636, 19866, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.409342 43.704246)"), 27800, "Eglinton & Avenue Rd", 3000 },
                { 637, 145969, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.384177 43.805439)"), 27000, "Steeles & Laureleaf", 5250 },
                { 638, 377655, "Ottawa-Kanata", (Point)new WKTReader().Read("SRID=4326;POINT (-75.935924 45.358284)"), 127600, "March & Maxwell Bridge", 21000 },
                { 639, 48554, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.411828 43.655404)"), 104900, "College & Bathurst", 51000 },
                { 640, 18410, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.78608 43.597307)"), 57250, "Winston Churchill & 401", 49500 },
                { 641, 521904, "Burlington", (Point)new WKTReader().Read("SRID=4326;POINT (-79.755071 43.368926)"), 165600, "Appleby Line & New St", 192575 },
                { 642, 229871, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.466258 43.758986)"), 88100, "Allen Rd & Rimrock", 10875 },
                { 643, 174318, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.35676 43.754096)"), 98700, "York Mills & Leslie (York Mills Gardens)", 114000 },
                { 644, 180705, "Stratford", (Point)new WKTReader().Read("SRID=4326;POINT (-80.944697 43.369923)"), 9750, "Ontario St & C.h. Meir Blvd", 21000 },
                { 645, 250056, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.722999 45.405793)"), 12750, "Wellington & Somerset St W", 7500 },
                { 646, 59845, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.38459 43.660505)"), 39750, "College & Bay (College Park)", 71625 },
                { 647, 56287, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.766015 43.732143)"), 0, "Bovaird Dr & Great Lakes Dr", 0 },
                { 648, 177865, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.436628 43.657397)"), 113800, "Dufferin & Bloor", 2625 },
                { 649, 115637, "Milton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.858504 43.487136)"), 132700, "Bronte St S & Louis St. Laurent", 71250 },
                { 650, 196768, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.374653 43.704062)"), 0, "Bayview & Millwood", 61500 },
                { 651, 81632, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.399588 43.687084)"), 96750, "Avenue Rd & St. Clair W", 92675 },
                { 652, 422636, "Sudbury", (Point)new WKTReader().Read("SRID=4326;POINT (-80.946948 46.500884)"), 130500, "Kingsway & Barrydowne Rd", 61150 },
                { 653, 246995, "Courtice", (Point)new WKTReader().Read("SRID=4326;POINT (-78.808225 43.907064)"), 67250, "King St E & Darlington", 6375 },
                { 654, 380562, "Kingston", (Point)new WKTReader().Read("SRID=4326;POINT (-76.500809 44.262724)"), 43250, "Division St & John Counter Blvd", 16500 },
                { 655, 50858, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.816532 43.658325)"), 51000, "Mississauga Rd & William Pkwy", 3750 },
                { 656, 79946, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.401499 43.647591)"), 26250, "Queen & Portland", 6000 },
                { 657, 92717, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.430553 43.720122)"), 59000, "Bathurst & Lawrence", 9000 },
                { 658, 184943, "Aurora", (Point)new WKTReader().Read("SRID=4326;POINT (-79.417915 44.012654)"), 46500, "Leslie & Wellington", 45675 },
                { 660, 250338, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.231553 43.724736)"), 51750, "Kingston & Mccowan", 1875 },
                { 661, 167689, "Brampton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.778508 43.771517)"), 121850, "Bramalea & Mayfield", 4500 },
                { 665, 303917, "Ancaster", (Point)new WKTReader().Read("SRID=4326;POINT (-80.022116 43.197048)"), 67500, "Hwy 2 & Mcclure Rd", 45000 },
                { 667, 155589, "Hamilton", (Point)new WKTReader().Read("SRID=4326;POINT (-79.893317 43.204921)"), 42000, "Upper James & Rymal", 11625 },
                { 668, 62909, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.423927 43.671579)"), 15900, "Dupont St & Christie St", 6750 },
                { 669, 65283, "Toronto-Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.482883 43.622754)"), 9000, "Lake Shore & Park Lawn", 750 },
                { 670, 202847, "Ottawa-Nepean", (Point)new WKTReader().Read("SRID=4326;POINT (-75.782401 45.270518)"), 107375, "Strandherd Dr & Maravista Dr", 58125 },
                { 671, 104631, "King City", (Point)new WKTReader().Read("SRID=4326;POINT (-79.506768 43.934505)"), 154750, "King Rd & Dufferin St", 67700 },
                { 672, 61746, "Etobicoke", (Point)new WKTReader().Read("SRID=4326;POINT (-79.596445 43.72245)"), 92600, "Hwy 27 & Queens Plate Dr", 11250 },
                { 673, 11352, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.347605 43.659637)"), 21000, "Queen & Broadview", 3000 },
                { 674, 263434, "Komoka", (Point)new WKTReader().Read("SRID=4326;POINT (-81.422718 42.95201)"), 5250, "Glendon & Tunks", 10500 },
                { 675, 169822, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.32844 43.65782)"), 82000, "Lakeshore Blvd E & Leslie St", 85500 },
                { 676, 127237, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.404393 43.732528)"), 61000, "Yonge & Lawrence", 180375 },
                { 677, 103400, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.380837 43.642219)"), 46500, "York & Lake Shore (Maple Leaf Square)", 3000 },
                { 679, 136724, "Virgil", (Point)new WKTReader().Read("SRID=4326;POINT (-79.119505 43.223499)"), 0, "Niagara Stone Rd & Field Rd", 4825 },
                { 680, 125345, "Stittsville", (Point)new WKTReader().Read("SRID=4326;POINT (-75.873994 45.276275)"), 15750, "Cope Dr & Terry Fox Dr", 6000 },
                { 684, 137970, "Barrie", (Point)new WKTReader().Read("SRID=4326;POINT (-79.676421 44.414602)"), 19500, "Cundles Rd E & Hwy 400", 7500 },
                { 685, 118281, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.462586 43.665261)"), 24000, "Dundas St W & Keele St", 35250 },
                { 686, 118312, "Ottawa-Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.677427 45.439901)"), 15000, "Beechwood Avenue & Mackay Street", 8625 },
                { 690, 0, "Fort Erie-Ridgeway", (Point)new WKTReader().Read("SRID=4326;POINT (-79.059888 42.886269)"), 3000, "Gorham Rd & Dominion Rd", 29250 },
                { 691, 338431, "Kitchener", (Point)new WKTReader().Read("SRID=4326;POINT (-80.431005 43.393288)"), 21750, "Homer Watson Blvd & Pioneer Dr", 3750 },
                { 693, 26961, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.392504 43.6498)"), 97300, "Queen St W & Beverly", 9375 },
                { 694, 227226, "Ottawa-Orleans", (Point)new WKTReader().Read("SRID=4326;POINT (-75.455745 45.46895)"), 106800, "Innes & Trim Rd", 82125 },
                { 695, 102168, "Whitby", (Point)new WKTReader().Read("SRID=4326;POINT (-78.94651 43.859391)"), 21750, "Victoria St & Gordon St", 13875 },
                { 697, 87469, "Toronto-Central", (Point)new WKTReader().Read("SRID=4326;POINT (-79.300474 43.670246)"), 0, "Queen & Bellefair (Beaches)", 91500 },
                { 698, 121945, "Mississauga", (Point)new WKTReader().Read("SRID=4326;POINT (-79.643193 43.597399)"), 176150, "Square One Dr & Rathburn", 6375 },
                { 699, 145003, "Ottawa", (Point)new WKTReader().Read("SRID=4326;POINT (-75.67199 45.287711)"), 34200, "Limebank & Spratt", 14625 },
                { 700, 100693, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.279695 43.774042)"), 235500, "Kennedy & 401 (Kennedy Commons)", 5625 },
                { 702, 202742, "Waterloo", (Point)new WKTReader().Read("SRID=4326;POINT (-80.56861 43.468425)"), 43500, "Columbia & Fischer Hallman (Laurelwood)", 24850 },
                { 703, 89207, "Toronto-Scarborough", (Point)new WKTReader().Read("SRID=4326;POINT (-79.256229 43.835043)"), 511600, "Steeles & Markham", 63500 },
                { 706, 136224, "Guelph", (Point)new WKTReader().Read("SRID=4326;POINT (-80.287458 43.523478)"), 33000, "Paisley & Imperial", 48050 },
                { 741, 325629, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.332859 43.009276)"), 203250, "Hyde Park & Fanshawe Park Rd", 84375 },
                { 743, 269437, "Kingston", (Point)new WKTReader().Read("SRID=4326;POINT (-76.451313 44.270289)"), 18000, "Hwy 15 & Waterside Way", 75750 },
                { 744, 69058, "Innisfil-Friday Harb", (Point)new WKTReader().Read("SRID=4326;POINT (-79.530902 44.393536)"), 76000, "Big Bay Point Rd & Sea Ray", 0 },
                { 746, 196813, "Waubaushene", (Point)new WKTReader().Read("SRID=4326;POINT (-79.705416 44.749216)"), 15750, "Hwy 400 & Hwy 12", 0 },
                { 747, 94640, "Markham", (Point)new WKTReader().Read("SRID=4326;POINT (-79.228137 43.868611)"), 59250, "9th Line & 407", 4125 },
                { 748, 168880, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.263436 43.776659)"), 30750, "Brimley Rd & Progress Ave", 1500 },
                { 749, 91298, "Binbrook", (Point)new WKTReader().Read("SRID=4326;POINT (-79.800914 43.125068)"), 24000, "Maggie Johnson Dr & Rr 56", 24750 },
                { 751, 179491, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.399433 43.636938)"), 224350, "Bathurst & Lakeshore", 78375 },
                { 753, 98384, "Oshawa", (Point)new WKTReader().Read("SRID=4326;POINT (-78.905925 43.963006)"), 1500, "Simcoe St. N & Winchester", 5250 },
                { 754, 70477, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.378416 43.649261)"), 70500, "Yonge St & King St W", 5625 },
                { 755, 0, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.39672 43.69979)"), 14250, "Yonge & Davisville", 60025 },
                { 756, 61490, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.282534 43.681422)"), 0, "Victoria Park & Kingston Rd", 3750 },
                { 758, 0, "Toronto", (Point)new WKTReader().Read("SRID=4326;POINT (-79.381113 43.645144)"), 30000, "Front St & Bay St (Union Station)", 11250 },
                { 760, 33829, "Toronto-North York", (Point)new WKTReader().Read("SRID=4326;POINT (-79.418791 43.795003)"), 17250, "Yonge St & Steeles Ave", 19500 },
                { 761, 53449, "Stoney Creek", (Point)new WKTReader().Read("SRID=4326;POINT (-79.812266 43.190615)"), 14250, "Stone Church Rd E & Upper Red Hill", 9750 },
                { 762, 220472, "London", (Point)new WKTReader().Read("SRID=4326;POINT (-81.259501 43.04606)"), 0, "Sunningdale Rd. & Adelaide St.", 3375 },
                { 771, 99396, "Ajax", (Point)new WKTReader().Read("SRID=4326;POINT (-79.023921 43.864812)"), 168750, "Kingston & Harwood (Durham Centre)", 56250 },
                { 776, 431323, "Pickering", (Point)new WKTReader().Read("SRID=4326;POINT (-79.072652 43.845567)"), 176050, "Brock & Kingston (Hwy 2)", 153000 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 5);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 6);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 7);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 8);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 9);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 10);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 11);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 12);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 13);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 14);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 15);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 16);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 17);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 18);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 19);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 20);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 21);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 22);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 23);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 24);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 25);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 26);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 27);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 28);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 30);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 31);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 32);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 33);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 34);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 35);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 36);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 38);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 39);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 40);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 41);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 42);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 43);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 44);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 45);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 46);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 47);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 48);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 49);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 50);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 51);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 52);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 53);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 54);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 55);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 56);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 57);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 58);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 59);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 60);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 61);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 62);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 63);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 64);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 65);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 67);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 68);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 69);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 70);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 71);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 72);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 73);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 74);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 76);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 77);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 78);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 79);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 80);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 81);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 82);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 83);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 84);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 85);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 86);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 87);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 88);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 89);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 90);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 91);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 92);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 93);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 94);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 95);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 96);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 97);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 98);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 99);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 101);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 102);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 103);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 104);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 105);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 106);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 107);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 108);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 109);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 110);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 111);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 112);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 113);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 115);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 116);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 117);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 118);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 119);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 120);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 122);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 123);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 124);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 125);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 126);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 127);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 129);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 130);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 131);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 132);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 133);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 135);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 136);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 137);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 139);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 140);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 141);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 142);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 143);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 144);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 145);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 146);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 147);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 148);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 149);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 150);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 151);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 152);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 153);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 154);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 155);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 156);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 157);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 158);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 159);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 160);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 161);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 162);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 163);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 164);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 165);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 166);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 167);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 168);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 169);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 170);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 171);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 172);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 173);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 174);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 175);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 176);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 177);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 178);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 179);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 181);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 182);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 183);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 184);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 186);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 187);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 188);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 189);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 190);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 191);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 192);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 193);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 194);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 195);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 196);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 197);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 198);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 199);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 200);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 201);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 202);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 203);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 205);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 207);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 208);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 209);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 210);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 211);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 212);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 213);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 214);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 216);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 217);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 218);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 220);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 221);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 222);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 223);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 224);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 225);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 226);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 227);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 228);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 229);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 230);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 231);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 232);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 233);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 234);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 236);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 237);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 238);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 239);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 240);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 241);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 242);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 243);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 244);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 246);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 247);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 248);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 249);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 250);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 251);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 252);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 253);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 254);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 255);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 256);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 258);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 259);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 260);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 261);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 262);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 263);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 264);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 265);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 267);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 268);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 269);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 270);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 271);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 272);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 273);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 274);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 275);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 276);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 277);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 278);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 279);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 280);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 281);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 282);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 283);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 284);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 285);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 286);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 287);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 288);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 289);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 290);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 293);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 294);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 295);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 296);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 297);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 298);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 299);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 300);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 301);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 302);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 303);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 304);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 305);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 306);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 307);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 308);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 309);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 310);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 311);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 312);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 313);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 314);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 315);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 316);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 317);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 318);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 319);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 320);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 321);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 322);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 323);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 324);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 325);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 326);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 327);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 328);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 329);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 330);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 331);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 332);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 333);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 334);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 335);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 336);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 337);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 339);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 340);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 341);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 343);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 344);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 345);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 346);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 347);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 348);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 349);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 350);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 351);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 352);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 353);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 354);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 355);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 358);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 359);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 360);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 361);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 362);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 363);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 364);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 366);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 367);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 368);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 369);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 370);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 371);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 372);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 373);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 374);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 375);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 377);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 378);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 380);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 381);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 382);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 383);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 384);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 385);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 386);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 387);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 388);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 389);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 390);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 391);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 392);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 393);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 394);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 395);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 397);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 398);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 399);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 400);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 401);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 402);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 403);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 404);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 405);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 406);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 407);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 408);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 409);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 411);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 412);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 413);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 414);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 415);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 416);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 417);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 418);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 419);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 420);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 421);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 423);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 424);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 425);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 426);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 427);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 428);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 429);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 430);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 431);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 432);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 433);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 434);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 435);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 436);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 437);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 438);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 439);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 440);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 441);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 442);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 443);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 444);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 445);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 446);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 447);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 448);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 449);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 450);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 452);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 453);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 454);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 455);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 456);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 457);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 458);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 459);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 460);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 461);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 462);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 463);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 464);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 465);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 466);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 467);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 468);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 469);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 470);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 471);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 472);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 473);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 474);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 475);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 476);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 477);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 478);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 479);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 481);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 482);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 483);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 484);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 485);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 486);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 487);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 488);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 489);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 490);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 491);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 492);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 494);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 495);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 496);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 497);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 499);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 500);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 501);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 502);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 503);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 504);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 505);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 506);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 507);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 509);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 510);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 511);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 512);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 513);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 514);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 515);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 516);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 517);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 519);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 520);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 521);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 522);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 523);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 524);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 526);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 527);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 528);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 529);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 530);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 532);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 533);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 534);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 536);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 537);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 538);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 539);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 540);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 541);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 542);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 544);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 545);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 546);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 547);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 548);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 549);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 550);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 551);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 552);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 553);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 554);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 555);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 556);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 557);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 558);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 559);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 560);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 561);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 562);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 563);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 564);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 566);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 567);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 568);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 569);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 570);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 571);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 573);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 575);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 576);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 578);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 579);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 580);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 581);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 582);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 583);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 584);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 585);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 586);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 587);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 588);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 589);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 590);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 591);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 593);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 594);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 595);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 596);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 597);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 598);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 599);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 601);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 602);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 604);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 605);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 607);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 609);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 610);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 611);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 612);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 613);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 614);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 615);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 616);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 617);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 618);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 619);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 621);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 622);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 623);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 624);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 626);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 627);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 629);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 630);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 631);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 632);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 633);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 634);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 635);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 636);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 637);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 638);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 639);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 640);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 641);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 642);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 643);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 644);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 645);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 646);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 647);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 648);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 649);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 650);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 651);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 652);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 653);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 654);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 655);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 656);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 657);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 658);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 660);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 661);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 665);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 667);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 668);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 669);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 670);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 671);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 672);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 673);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 674);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 675);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 676);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 677);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 679);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 680);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 684);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 685);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 686);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 690);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 691);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 693);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 694);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 695);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 697);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 698);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 699);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 700);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 702);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 703);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 706);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 741);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 743);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 744);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 746);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 747);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 748);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 749);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 751);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 753);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 754);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 755);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 756);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 758);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 760);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 761);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 762);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 771);

        migrationBuilder.DeleteData(
            table: "Store",
            keyColumn: "Id",
            keyValue: 776);
    }
}
