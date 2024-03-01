using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public partial class UpdateStoresFromIncomingCsv : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncomingCsv]
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [s]
    FROM [Store] [s] -- with EXISTS semi-anti-join
    WHERE NOT EXISTS (
            SELECT 1
            FROM [StoreIncoming] [si]
            WHERE [si].[Id] = [s].[Id]
            );

    INSERT INTO [Store] (
        [Id]
        , [StoreName]
        , [City]
        , [Location]
        , [BeerVolume]
        , [WineVolume]
        , [SpiritsVolume]
        )
    SELECT [si].[Id]
        , [si].[StoreName]
        , [si].[City]
        , [si].[Location]
        , [si].[BeerVolume]
        , [si].[WineVolume]
        , [si].[SpiritsVolume]
    FROM [StoreIncoming] [si]
    LEFT JOIN [store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[location] = [si].[Location]
        , [s].[BeerVolume] = [si].[BeerVolume]
        , [s].[WineVolume] = [si].[WineVolume]
        , [s].[SpiritsVolume] = [si].[SpiritsVolume]
    FROM [store] [s]
        , [StoreIncoming] [si]
    WHERE [s].[Id] = [si].[Id];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [s]
    FROM [Store] [s] -- with EXISTS semi-anti-join
    WHERE NOT EXISTS (
            SELECT 1
            FROM [StoreIncoming] [si]
            WHERE [si].[Id] = [s].[Id]
            );

    INSERT INTO [Store] (
        [Id]
        , [StoreName]
        , [City]
        , [Location]
        )
    SELECT [si].[Id]
        , [si].[StoreName]
        , [si].[City]
        , GEOGRAPHY::STPointFromText(N'POINT(' + CAST([si].[Longitude] AS VARCHAR(20)) + N' ' + CAST([si].[Latitude] AS VARCHAR(20)) + N')', 4326)
    FROM [StoreIncoming] [si]
    LEFT JOIN [store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[location] = GEOGRAPHY::STPointFromText(N'POINT(' + CAST([si].[Longitude] AS VARCHAR(20)) + N' ' + CAST([si].[Latitude] AS VARCHAR(20)) + N')', 4326)
    FROM [store] [s]
        , [StoreIncoming] [si]
    WHERE [s].[Id] = [si].[Id];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateBoundariesFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [s]
    FROM [Subdivision] [s] -- with EXISTS semi-anti-join
    WHERE NOT EXISTS (
            SELECT 1
            FROM [BoundaryIncoming] [bi]
            WHERE [bi].[Id] = [s].[Id]
            );

    INSERT INTO [Subdivision] (
        [Id]
        , [SubdivisionName]
        , [Boundary]
        , [Province]
        )
    SELECT [bi].[id]
        , [bi].[SubdivisionName]
        , (
            SELECT CASE
                    WHEN [bi].[OriginalBoundary].EnvelopeAngle() >= 90
                        THEN [bi].[ReorientedBoundary]
                    ELSE [bi].[OriginalBoundary]
                    END
            )
        , [bi].[Province]
    FROM [BoundaryIncoming] [bi]
    LEFT JOIN [Subdivision] [s]
        ON [bi].[id] = [s].[id]
    WHERE [s].[id] IS NULL;

    UPDATE [s]
    SET [Boundary] = (
            SELECT CASE
                    WHEN [bi].[OriginalBoundary].EnvelopeAngle() >= 90
                        THEN [bi].[ReorientedBoundary]
                    ELSE [bi].[OriginalBoundary]
                    END
            )
        , [Province] = [bi].[Province]
    FROM [Subdivision] [s]
        , [BoundaryIncoming] [bi]
    WHERE [s].[id] = [bi].[id];

    -- calculate centres
    UPDATE [s]
    SET [GeographicCentre] = [Boundary].EnvelopeCenter()
    FROM [Subdivision] [s]
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdatePopulationsFromIncoming]
AS
BEGIN
    DELETE [s]
    FROM [Subdivision] [s] -- with EXISTS semi-anti-join
    WHERE NOT EXISTS (
            SELECT 1
            FROM [PopulationIncoming] [pi]
            WHERE [pi].[Id] = [s].[Id]
            );

    INSERT INTO [Subdivision] (
        [Id]
        , [SubdivisionName]
        , [Population]
        )
    SELECT [pi].[Id]
        , [pi].[subdivisionname]
        , [pi].[Population]
    FROM [PopulationIncoming] [pi]
    LEFT JOIN [Subdivision] [s]
        ON [pi].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [Population] = [pi].[Population]
    FROM [PopulationIncoming] [pi]
        , [Subdivision] [s]
    WHERE [s].[Id] = [pi].[Id];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateProductsFromIncoming]
AS
BEGIN
    DELETE [p]
    FROM [Product] [p] -- with EXISTS semi-anti-join
    WHERE NOT EXISTS (
            SELECT 1
            FROM [ProductIncoming] [pi]
            WHERE [pi].[Id] = [p].[Id]
            );

    INSERT INTO [Product] (
        [Id]
        , [ProductName]
        , [Category]
        , [Volume]
        )
    SELECT [pi].[Id]
        , [pi].[ProductName]
        , [pi].[Category]
        , [pi].[Volume]
    FROM [ProductIncoming] [pi]
    LEFT JOIN [Product] [p]
        ON [p].[Id] = [pi].[Id]
    WHERE [p].[Id] IS NULL;

    UPDATE [p]
    SET [p].[ProductName] = [pi].[ProductName]
        , [p].[Category] = [pi].[Category]
        , [p].[Volume] = [pi].[Volume]
    FROM [ProductIncoming] [pi]
        , [Product] [p]
    WHERE [p].[Id] = [pi].[Id];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateInventoriesFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [i]
    FROM [Inventory] [i] -- with EXISTS semi-anti-join
    WHERE NOT EXISTS (
            SELECT 1
            FROM [InventoryIncoming] [ii]
            WHERE [ii].[ProductId] = [i].[ProductId]
                AND [ii].[StoreId] = [i].[StoreId]
            );

    INSERT INTO [Inventory] (
        [ProductId]
        , [StoreId]
        , [Quantity]
        )
    SELECT [ii].[ProductId]
        , [ii].[StoreId]
        , [ii].[Quantity]
    FROM [InventoryIncoming] [ii]
    LEFT JOIN [Inventory] [i]
        ON [i].[ProductId] = [ii].[ProductId]
            AND [ii].[StoreId] = [i].[StoreId]
    WHERE [i].[ProductId] IS NULL
        AND [i].[StoreId] IS NULL;

    UPDATE [i]
    SET [i].[Quantity] = [ii].[Quantity]
    FROM [InventoryIncoming] [ii]
        , [Inventory] [i]
    WHERE [i].[ProductId] = [ii].[ProductId]
        AND [i].[StoreId] = [ii].[StoreId];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoreVolumes]
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Store]
    SET [BeerVolume] = (
            SELECT SUM([i].[Quantity] * [p].[Volume])
            FROM [Inventory] [i]
            INNER JOIN [Product] [p]
                ON [p].[Id] = [i].[ProductId]
            WHERE [i].[StoreId] = [Store].[id]
                AND [p].[Category] = N'Beer'
            )
        , [WineVolume] = (
            SELECT SUM([i].[Quantity] * [p].[Volume])
            FROM [Inventory] [i]
            INNER JOIN [Product] [p]
                ON [p].[Id] = [i].[ProductId]
            WHERE [i].[StoreId] = [Store].[Id]
                AND [p].[Category] = N'Wine'
            )
        , [SpiritsVolume] = (
            SELECT SUM([i].[Quantity] * [p].[Volume])
            FROM [Inventory] [i]
            INNER JOIN [Product] [p]
                ON [p].[Id] = [i].[ProductId]
            WHERE [i].[StoreId] = [Store].[Id]
                AND [p].[Category] = N'Spirits'
            )
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateSubdivisionVolumes]
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [sd]
    SET [BeerVolume] = (
            SELECT COALESCE(SUM(CAST([s].[BeerVolume] AS BIGINT)), 0)
            FROM [Store] [s]
            WHERE [sd].[boundary].STIntersects([s].[location]) = 1
            )
        , [WineVolume] = (
            SELECT COALESCE(SUM(CAST([s].[WineVolume] AS BIGINT)), 0)
            FROM [Store] [s]
            WHERE [sd].[boundary].STIntersects([s].[location]) = 1
            )
        , [SpiritsVolume] = (
            SELECT COALESCE(SUM(CAST([s].[SpiritsVolume] AS BIGINT)), 0)
            FROM [Store] [s]
            WHERE [sd].[boundary].STIntersects([s].[location]) = 1
            )
    FROM [Subdivision] [sd]
    WHERE [Population] > 0

    UPDATE [sd]
    SET [AlcoholDensity] = (CAST([BeerVolume] AS BIGINT) + CAST([WineVolume] AS BIGINT) + CAST([SpiritsVolume] AS BIGINT)) * 1.0 / [Population]
        , [BeerDensity] = CAST([BeerVolume] AS BIGINT) * 1.0 / [Population]
        , [WineDensity] = CAST([WineVolume] AS BIGINT) * 1.0 / [Population]
        , [SpiritsDensity] = CAST([SpiritsVolume] AS BIGINT) * 1.0 / [Population]
    FROM [Subdivision] [sd]
    WHERE [Population] > 0
END");

        // Spatial indexes

        migrationBuilder.Sql(@"CREATE SPATIAL INDEX [SPATIAL_Store] ON [dbo].[Store] ([Location]) USING GEOGRAPHY_GRID
    WITH (
            GRIDS = (
                LEVEL_1 = MEDIUM
                ,LEVEL_2 = MEDIUM
                ,LEVEL_3 = MEDIUM
                ,LEVEL_4 = MEDIUM
                )
            ,CELLS_PER_OBJECT = 16
            ,PAD_INDEX = OFF
            ,STATISTICS_NORECOMPUTE = OFF
            ,SORT_IN_TEMPDB = OFF
            ,DROP_EXISTING = OFF
            ,ONLINE = OFF
            ,ALLOW_ROW_LOCKS = ON
            ,ALLOW_PAGE_LOCKS = ON
            ) ON [PRIMARY]");

        migrationBuilder.Sql(@"CREATE SPATIAL INDEX [SPATIAL_Subdivision] ON [dbo].[Subdivision] ([Boundary]) USING GEOGRAPHY_GRID
    WITH (
            GRIDS = (
                LEVEL_1 = MEDIUM
                ,LEVEL_2 = MEDIUM
                ,LEVEL_3 = MEDIUM
                ,LEVEL_4 = MEDIUM
                )
            ,CELLS_PER_OBJECT = 16
            ,PAD_INDEX = OFF
            ,STATISTICS_NORECOMPUTE = OFF
            ,SORT_IN_TEMPDB = OFF
            ,DROP_EXISTING = OFF
            ,ONLINE = OFF
            ,ALLOW_ROW_LOCKS = ON
            ,ALLOW_PAGE_LOCKS = ON
            ) ON [PRIMARY]");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
