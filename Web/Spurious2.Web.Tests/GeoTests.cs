﻿using NetTopologySuite.IO.Converters;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Spurious2.Web.Tests
{
    [TestFixture]
    public class GeoTests
    {
        [Test]
        public void Test()
        {
            NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
    NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
    new NetTopologySuite.Geometries.PrecisionModel(1000d),
    4326 /* ,
    // Note the following arguments are only valid for NTS v2.2
    // Geometry overlay operation function set to use (Legacy or NG)
    NetTopologySuite.Geometries.GeometryOverlay.NG,
    // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
    new NetTopologySuite.Geometries.CoordinateEqualityComparer() */);

            var rdr = new NetTopologySuite.IO.WKTReader();
            var pt = rdr.Read("POINT M(7.5404 53.4837 5432)");
            var res = new JsonSerializerOptions
            { ReadCommentHandling = JsonCommentHandling.Skip };
            res.Converters.Add(new GeoJsonConverterFactory());

            string j;
            using (var ms = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(ms))
                    System.Text.Json.JsonSerializer.Serialize(writer, pt, res);
                j = Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        [Test]
        public void PolygonTest()
        {
            NetTopologySuite.NtsGeometryServices.Instance = new NetTopologySuite.NtsGeometryServices(
    NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory.Instance,
    new NetTopologySuite.Geometries.PrecisionModel(1000d),
    4326 /* ,
    // Note the following arguments are only valid for NTS v2.2
    // Geometry overlay operation function set to use (Legacy or NG)
    NetTopologySuite.Geometries.GeometryOverlay.NG,
    // Coordinate equality comparer to use (CoordinateEqualityComparer or PerOrdinateEqualityComparer)
    new NetTopologySuite.Geometries.CoordinateEqualityComparer() */);

            var pg = "POLYGON ((-53.0434129606989 46.8163175831069, -53.00447056849 46.816531955054, -52.9999999860833 46.8165475758732, -52.9976541806485 46.8165594116143, -52.9963108629991 46.8165661826446, -52.9895812164636 46.816599749645, -52.9884062730659 46.8166055423448, -52.9778145699479 46.8166572527389, -52.9530279990129 46.816773525018, -52.9495681214093 46.8167892172451, -52.9376459475794 46.8168423321533, -52.9376085949645 46.8170986246766, -52.9377981459907 46.817995352484, -52.9376470571734 46.8188978548846, -52.9380604572879 46.8194578009905, -52.9398803410524 46.820119925096, -52.9398853417111 46.8206237974667, -52.9391755649061 46.8220307290003, -52.9391000498356 46.8224819677195, -52.9392807294817 46.823495843913, -52.940052191784 46.8248956736255, -52.9398969065353 46.8256812454714, -52.9393323362867 46.8260237895506, -52.9384301252262 46.826093061818, -52.9372119802503 46.8264916810509, -52.9372273181212 46.8272833630056, -52.937730281231 46.8277787958864, -52.9379148414224 46.8281716447814, -52.9378314993868 46.8291360008263, -52.93703290641 46.8302654299687, -52.9372124702383 46.8308833458097, -52.9377135126793 46.8313248404758, -52.9387703115419 46.8315409020225, -52.9395206889894 46.8319871825909, -52.9398555925662 46.8325484493098, -52.9400390929403 46.8332742994904, -52.9405323840015 46.8334999004343, -52.9416717426035 46.8330936021804, -52.9426568655295 46.8331398801142, -52.9433155577037 46.8335876561788, -52.9437392264829 46.8344264061783, -52.9438545835371 46.8361703030704, -52.9441984684968 46.836614446574, -52.9442802064209 46.8370630127846, -52.9440574891202 46.8377957573796, -52.9441473695594 46.8384691719495, -52.9447325603579 46.8394221616142, -52.9447693407894 46.8411674108096, -52.9454228089938 46.8418312558885, -52.9464325138128 46.8425520355253, -52.9466989804498 46.8433934318876, -52.9466320511913 46.8437185301202, -52.9465440866718 46.8441880142317, -52.9467156770316 46.8445810603391, -52.9473865890393 46.8453615909602, -52.9467906625415 46.8459296751861, -52.9462582976465 46.8464426919309, -52.9461628721588 46.8467053109887, -52.9459837660558 46.8471943172214, -52.9453899800209 46.8481853300245, -52.9448274796098 46.8485908634783, -52.943024810061 46.8491614454559, -52.9418227154345 46.8500188100566, -52.9412528755753 46.8505864245314, -52.9413447064921 46.8513138280451, -52.9426193680579 46.8528310750323, -52.9430258587552 46.8535531092039, -52.9434830255829 46.8564071244503, -52.943807939837 46.8599563285911, -52.9433097603452 46.8606937588028, -52.9429234251077 46.8612582686464, -52.9421861052017 46.8615497700218, -52.9400570747077 46.8614508868426, -52.9390914855805 46.8619622177169, -52.9384289100712 46.8621444305974, -52.9368771300756 46.8620447075023, -52.9360546400791 46.8621576305123, -52.9354040195227 46.8626726031963, -52.9349380700515 46.8635803936185, -52.9350278751434 46.8642538332778, -52.9353626272583 46.8648061252276, -52.9352870110022 46.865257385227, -52.9345718784229 46.8661693887786, -52.9347373390592 46.8663915882867, -52.9351438563016 46.8663847095762, -52.9356363245088 46.8662143951956, -52.9363748849047 46.8663188830855, -52.9365537483508 46.867278792081, -52.9376711409928 46.8677008268587, -52.9378854765055 46.8678231864505, -52.9377838330411 46.8693727962296, -52.9377639044356 46.8706420509603, -52.9390151474295 46.8718627396851, -52.9394463104155 46.8725393933765, -52.9393757393229 46.8734945068481, -52.9386482863408 46.8740647738264, -52.9376785771235 46.8744681935559, -52.9370268639225 46.8749561741763, -52.9365411087845 46.875315363592, -52.9362182104343 46.8758247940194, -52.9358834944077 46.8770993722282, -52.9347705206941 46.8819148003407, -52.933633969101 46.8824289655943, -52.9319071570929 46.8829530848768, -52.9305402676443 46.8836331190203, -52.928525732121 46.8852869314395, -52.9266811525793 46.8876578434636, -52.9254678047747 46.8889651497717, -52.9240472982222 46.8914548282248, -52.9227467285678 46.8925296094309, -52.9223529215192 46.8932651636431, -52.9219954618774 46.8950170195275, -52.9197862338482 46.9011646012089, -52.9184207275613 46.9026363762832, -52.9184274982792 46.90319423145, -52.9181103887008 46.903874480182, -52.9173054800122 46.9044999116036, -52.9167409140469 46.9052383152624, -52.9168466430324 46.9063614465585, -52.9171692639597 46.9065720294254, -52.9174250492616 46.9067477263694, -52.9189912093347 46.9068474632849, -52.920067419497 46.9075673425708, -52.9211329783125 46.907621453637, -52.9216388243071 46.9081799149558, -52.9246732425145 46.9082099255154, -52.9264956804748 46.9088631968868, -52.9283183568539 46.9102543674902, -52.9294999722223 46.9117103465234, -52.930002833092 46.9125477880598, -52.931743923828 46.9134903297079, -52.9320750892297 46.9139346906638, -52.9330698024851 46.9142058787684, -52.9347261441331 46.9149788306931, -52.9361261482752 46.9151981316728, -52.9365646247236 46.9157036589637, -52.9369614597266 46.9161469032954, -52.9391823217558 46.9169102318429, -52.9407766615239 46.9188720488045, -52.9411536334126 46.9191266223642, -52.9426436115854 46.9204057781895, -52.9433251788675 46.9208129336258, -52.9451981425067 46.9223696676925, -52.9463560540658 46.9231509033281, -52.9465874935232 46.9241009018267, -52.9467204698249 46.9252325298703, -52.9483863755688 46.9266170663098, -52.9485859038233 46.9281345320401, -52.9490033331917 46.9284154130432, -52.9502982248265 46.9278983831464, -52.9509568898862 46.9279501615753, -52.9512125276112 46.928116774562, -52.9511483673372 46.9292427850445, -52.9507536674961 46.9303114146894, -52.9511674418091 46.930853315902, -52.9514807588292 46.9318685775862, -52.9523359234576 46.9331364971782, -52.9521952096033 46.9333454927869, -52.9516674027214 46.9341294963258, -52.951252102824 46.9346314925266, -52.9509707857458 46.9348433050383, -52.9504303989983 46.9350951032614, -52.950172567255 46.9352152538438, -52.9499880141838 46.9350184676384, -52.9496263040589 46.934660449783, -52.9487867753104 46.9339124736781, -52.9486777127786 46.9331995382984, -52.9478197362463 46.933088151723, -52.9470277402837 46.9329846239418, -52.9464488361078 46.9325985095194, -52.9462788935196 46.932259449688, -52.9466869766061 46.9319195229812, -52.9475820296553 46.9319672827085, -52.9477362431117 46.9307857859613, -52.9480625155141 46.9307262280041, -52.9481292168248 46.9303921252803, -52.9477086000202 46.929661328862, -52.9463821233703 46.9289459659354, -52.9445604342353 46.928337989594, -52.9424923311499 46.9274551918527, -52.9420046527829 46.9274094692695, -52.9411811493236 46.9275224436853, -52.9403772949063 46.927815036603, -52.9397954097008 46.9277079086314, -52.9388861512587 46.9272643655855, -52.9374637005162 46.925704591727, -52.9367120312003 46.9252583581045, -52.9352321615082 46.9246534309154, -52.9342229900801 46.923986543546, -52.9335543545892 46.9229269459702, -52.9332016109836 46.9215200265009, -52.9329511016238 46.9211282786533, -52.9321995056385 46.9206820232471, -52.9309722949233 46.9205227537602, -52.9302179984968 46.9196355609626, -52.9291413229852 46.9196446942243, -52.927272743519 46.9202791325184, -52.9253047469255 46.9203392503419, -52.9248981284615 46.9203550936844, -52.9243909640107 46.9201296565817, -52.9241043489766 46.9198284901762, -52.9232307929421 46.9189072625253, -52.9225622284367 46.918576519183, -52.9210008030741 46.9182517596388, -52.9200137752227 46.9181963273623, -52.917977427844 46.9192833918592, -52.9168300031196 46.9195186014535, -52.9165905619946 46.9198015862352, -52.9166735181002 46.9202861695416, -52.9167712198696 46.9208154675945, -52.9166204738293 46.9213759463445, -52.9161277311191 46.9215551843713, -52.9144074425866 46.9215659745842, -52.9139255961482 46.9216820324196, -52.9135154782253 46.9219678632331, -52.9126734262664 46.9222968926092, -52.9119648677666 46.9223177427131, -52.9110488863274 46.9220450659248, -52.9099852679669 46.9220538151613, -52.9086090575014 46.9232466740934, -52.9078965783388 46.9242664624265, -52.9064354492215 46.9252897295725, -52.9062006634865 46.9260765734088, -52.9053094538037 46.9268743504928, -52.9054080061329 46.9274306486365, -52.9058219796672 46.9279907230214, -52.9056691627054 46.9284972115432, -52.9042130399873 46.9292953803999, -52.9038936885484 46.9299216379349, -52.9022756979161 46.931343419562, -52.9016334645621 46.9321280329102, -52.9011631118783 46.933314728744, -52.8991258039854 46.934401458763, -52.8989603299303 46.9349557294345, -52.9044746405372 46.9367979667159, -52.9314837950428 46.9458144756451, -52.9376997896965 46.9478879689222, -52.9515474276339 46.9525051155149, -52.9531994982016 46.9530556863784, -52.9555153279116 46.9534102936229, -52.9748789865907 46.9563729553712, -52.9844265096723 46.957832089204, -52.9870102562581 46.9582267805446, -52.989841122805 46.9586591178337, -52.9913804016396 46.9588941861864, -52.9919353958472 46.9589789122449, -52.9999999896558 46.9602098873526, -53.0059042568306 46.9611106142626, -53.0080835960562 46.9614429982604, -53.0104679282673 46.9618065713285, -53.0210428060623 46.9634182468921, -53.0236049268018 46.9638085282695, -53.0366630528084 46.9657964808424, -53.0442298088715 46.9667955665226, -53.044131958711 46.9488164961352, -53.0440846418546 46.9401185504271, -53.0439599303052 46.9171817677465, -53.0439397508849 46.9134689833004, -53.0439105651765 46.9080952901439, -53.0439028700623 46.9066829811584, -53.0438902680594 46.904357429947, -53.0438823984477 46.9029122847976, -53.0438789946909 46.9022832852012, -53.0438730972337 46.9011959076994, -53.0437723361374 46.8826345251448, -53.0437635163711 46.881012511065, -53.0437355252062 46.8758485335322, -53.0437328562569 46.8753583736777, -53.0437207106486 46.8731255523293, -53.0437129279558 46.8716855441108, -53.0437082950095 46.8708283883064, -53.043669886814 46.8637504261649, -53.043568958461 46.8451290785498, -53.0434129606989 46.8163175831069))";
            var rdr = new NetTopologySuite.IO.WKTReader();
            var pt = rdr.Read(pg);
            var res = new JsonSerializerOptions
            { ReadCommentHandling = JsonCommentHandling.Skip };
            res.Converters.Add(new GeoJsonConverterFactory());

            string j;
            using (var ms = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(ms))
                    System.Text.Json.JsonSerializer.Serialize(writer, pt, res);
                j = Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}