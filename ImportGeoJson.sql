DECLARE @geojson NVARCHAR(max) = 
    N'{
    "type": "FeatureCollection",
    "name": "gcsd000a11g_e",
    "crs": {
        "type": "name",
        "properties": {
            "name": "urn:ogc:def:crs:EPSG::4269"
        }
    },
    "features": [{
            "type": "Feature",
            "properties": {
                "gml_id": "id421d0215-e267-47ae-bcec-51d04d93caa6",
                "CSDUID": "4614042",
                "CSDNAME": "Teulon",
                "CSDTYPE": "T",
                "PRUID": "46",
                "PRNAME": "Manitoba",
                "CDUID": "4614",
                "CDNAME": "Division No. 14",
                "CDTYPE": "CDR",
                "CMAUID": "",
                "CMANAME": "",
                "CMATYPE": "",
                "CMAPUID": "",
                "SACTYPE": "5",
                "SACCODE": "997",
                "ERUID": "4660",
                "ERNAME": "Interlake",
                "CCSUID": "4614036",
                "CCSNAME": "Rockwood"
            },
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    [
                        [[-97.2700029559999, 50.390721120000102], [-97.270002932999901, 50.393284308], [-97.264344585999893, 50.393306848000101], [-97.252613628999896, 50.393352554], [-97.252610724999897, 50.392913072000098], [-97.252603244999904, 50.3917800360001], [-97.252591256999906, 50.389958243000102], [-97.252585331999896, 50.389059162000102], [-97.241432231999894, 50.389093401000103], [-97.241014792999906, 50.389094681000103], [-97.240985597999895, 50.381739835000097], [-97.252560603999896, 50.3818013630001], [-97.252554712999896, 50.380067345], [-97.252554488999905, 50.379990869], [-97.253378351999899, 50.380003413000097], [-97.2533714919999, 50.377173664000097], [-97.254877077999893, 50.377160504000102], [-97.254871558999895, 50.376716782000102], [-97.2548456479999, 50.374630153000098], [-97.255348113999901, 50.374616105000101], [-97.260917746999894, 50.3744604010001], [-97.262588571999899, 50.374413598], [-97.263714445999895, 50.374413252000103], [-97.264360683, 50.374413077], [-97.264366760999906, 50.374413084000103], [-97.264813822999898, 50.374413195], [-97.270003013999897, 50.374413836000102], [-97.270002997999896, 50.381583416000097], [-97.270002943999899, 50.389222098000097], [-97.2700029559999, 50.390721120000102]]
                    ]
                ]
            }
        }
    ]
}'

--select @geojson = BulkColumn
--from openrowset(bulk 'D:\Downloads\gcsd000a11g_e\subdiv.json', single_clob) as JSON;
--select 
--subdivname
----,geography::STPolyFromText('POLYGON ((' + STRING_AGG(CAST(Long + ' ' + Lat as varchar(max)), ',') + '))',4269).ReorientObject() AS boundary
--from
--OPENJSON(@geojson, '$.features')
--WITH(
--subdivname nvarchar(255) '$.properties.CSDNAME'
--  --              ,Long varchar(100) '$[0]'
--    --            ,Lat varchar(100) '$[1]'
--)
SELECT --subdivname
     string_agg(d.x + ' ' + d.y, ',')
FROM (
    SELECT x
        , y
    FROM openjson(@geojson, '$.features') WITH (
            subdivname NVARCHAR(255) '$.properties.CSDNAME'
            , [geometry] NVARCHAR(max) AS json
            ) AS f
    CROSS APPLY openjson([geometry], '$.coordinates') AS l1
    CROSS APPLY OPENJSON(l1.value) AS l2
    CROSS APPLY OPENJSON(l2.value) WITH (
            x varchar(100) '$[0]'
            , y varchar(100) '$[1]'
            )
    )d
    --SELECT l1.[key] as polygon, l2.[key] as line, x, y
    --FROM OPENJSON(@geojson, '$.features') as f
    ----with (n nvarchar(255) '$.properties.CSDNAME') as f
    --CROSS APPLY OPENJSON(f.value, '$.geometry.coordinates') as l1
    --       CROSS APPLY OPENJSON(l1.value) as l2
    --              CROSS APPLY OPENJSON(l2.value)
    --                     WITH (x float '$[0]', y float '$[1]')
