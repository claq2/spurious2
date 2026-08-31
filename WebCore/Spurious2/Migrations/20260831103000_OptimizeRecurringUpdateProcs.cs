using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
public partial class OptimizeRecurringUpdateProcs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder);

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ChangedStores TABLE (
        [Id] INT NOT NULL PRIMARY KEY
    );

    DELETE [s]
    FROM [Store] [s]
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
    OUTPUT [inserted].[Id] INTO @ChangedStores ([Id])
    SELECT [si].[Id]
        , [si].[StoreName]
        , [si].[City]
        , [nl].[NewLocation]
    FROM [StoreIncoming] [si]
    CROSS APPLY (
        SELECT GEOGRAPHY::STPointFromText([si].[LocationWellKnownText], 4326) AS [NewLocation]
        ) [nl]
    LEFT JOIN [Store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[Location] = [nl].[NewLocation]
    OUTPUT [inserted].[Id] INTO @ChangedStores ([Id])
    FROM [Store] [s]
    INNER JOIN [StoreIncoming] [si]
        ON [s].[Id] = [si].[Id]
    CROSS APPLY (
        SELECT GEOGRAPHY::STPointFromText([si].[LocationWellKnownText], 4326) AS [NewLocation]
        ) [nl]
    WHERE ISNULL([s].[StoreName], N'') <> ISNULL([si].[StoreName], N'')
        OR ISNULL([s].[City], N'') <> ISNULL([si].[City], N'')
        OR [s].[Location].STEquals([nl].[NewLocation]) = 0;

    UPDATE [s]
    SET [s].[SubdivisionId] = [su].[Id]
    FROM [Store] [s]
    INNER JOIN @ChangedStores [cs]
        ON [cs].[Id] = [s].[Id]
    OUTER APPLY (
        SELECT TOP (1) [sub].[Id]
        FROM [Subdivision] [sub]
        WHERE [sub].[Boundary].STIntersects([s].[Location]) = 1
        ORDER BY [sub].[Id]
        ) [su];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncomingCsv]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ChangedStores TABLE (
        [Id] INT NOT NULL PRIMARY KEY
    );

    DELETE [s]
    FROM [Store] [s]
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
    OUTPUT [inserted].[Id] INTO @ChangedStores ([Id])
    SELECT [si].[Id]
        , [si].[StoreName]
        , [si].[City]
        , [si].[Location]
        , [si].[BeerVolume]
        , [si].[WineVolume]
        , [si].[SpiritsVolume]
    FROM [StoreIncoming] [si]
    LEFT JOIN [Store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[Location] = [si].[Location]
        , [s].[BeerVolume] = [si].[BeerVolume]
        , [s].[WineVolume] = [si].[WineVolume]
        , [s].[SpiritsVolume] = [si].[SpiritsVolume]
    OUTPUT [inserted].[Id] INTO @ChangedStores ([Id])
    FROM [Store] [s]
    INNER JOIN [StoreIncoming] [si]
        ON [s].[Id] = [si].[Id]
    WHERE ISNULL([s].[StoreName], N'') <> ISNULL([si].[StoreName], N'')
        OR ISNULL([s].[City], N'') <> ISNULL([si].[City], N'')
        OR [s].[Location].STEquals([si].[Location]) = 0
        OR ISNULL([s].[BeerVolume], -1) <> ISNULL([si].[BeerVolume], -1)
        OR ISNULL([s].[WineVolume], -1) <> ISNULL([si].[WineVolume], -1)
        OR ISNULL([s].[SpiritsVolume], -1) <> ISNULL([si].[SpiritsVolume], -1);

    UPDATE [s]
    SET [s].[SubdivisionId] = [su].[Id]
    FROM [Store] [s]
    INNER JOIN @ChangedStores [cs]
        ON [cs].[Id] = [s].[Id]
    OUTER APPLY (
        SELECT TOP (1) [sub].[Id]
        FROM [Subdivision] [sub]
        WHERE [sub].[Boundary].STIntersects([s].[Location]) = 1
        ORDER BY [sub].[Id]
        ) [su];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateProductsFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [p]
    FROM [Product] [p]
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
    FROM [Product] [p]
    INNER JOIN [ProductIncoming] [pi]
        ON [p].[Id] = [pi].[Id]
    WHERE ISNULL([p].[ProductName], N'') <> ISNULL([pi].[ProductName], N'')
        OR ISNULL([p].[Category], N'') <> ISNULL([pi].[Category], N'')
        OR ISNULL([p].[Volume], -1) <> ISNULL([pi].[Volume], -1);
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateInventoriesFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    DELETE [i]
    FROM [Inventory] [i]
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
            AND [i].[StoreId] = [ii].[StoreId]
    WHERE [i].[ProductId] IS NULL;

    UPDATE [i]
    SET [i].[Quantity] = [ii].[Quantity]
    FROM [Inventory] [i]
    INNER JOIN [InventoryIncoming] [ii]
        ON [i].[ProductId] = [ii].[ProductId]
        AND [i].[StoreId] = [ii].[StoreId]
    WHERE [i].[Quantity] <> [ii].[Quantity];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoreVolumes]
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH [StoreVolumeAgg] AS (
        SELECT [i].[StoreId]
            , SUM(CASE
                    WHEN [p].[Category] = N'Beer'
                        THEN [i].[Quantity] * ISNULL([p].[Volume], 0)
                    ELSE 0
                    END) AS [BeerVolume]
            , SUM(CASE
                    WHEN [p].[Category] = N'Wine'
                        THEN [i].[Quantity] * ISNULL([p].[Volume], 0)
                    ELSE 0
                    END) AS [WineVolume]
            , SUM(CASE
                    WHEN [p].[Category] = N'Spirits'
                        THEN [i].[Quantity] * ISNULL([p].[Volume], 0)
                    ELSE 0
                    END) AS [SpiritsVolume]
        FROM [Inventory] [i]
        INNER JOIN [Product] [p]
            ON [p].[Id] = [i].[ProductId]
        GROUP BY [i].[StoreId]
        )
    UPDATE [s]
    SET [s].[BeerVolume] = ISNULL([a].[BeerVolume], 0)
        , [s].[WineVolume] = ISNULL([a].[WineVolume], 0)
        , [s].[SpiritsVolume] = ISNULL([a].[SpiritsVolume], 0)
    FROM [Store] [s]
    LEFT JOIN [StoreVolumeAgg] [a]
        ON [a].[StoreId] = [s].[Id];
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateSubdivisionVolumes]
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH [SubdivisionVolumeAgg] AS (
        SELECT [s].[SubdivisionId]
            , SUM(ISNULL([s].[BeerVolume], 0)) AS [BeerVolume]
            , SUM(ISNULL([s].[WineVolume], 0)) AS [WineVolume]
            , SUM(ISNULL([s].[SpiritsVolume], 0)) AS [SpiritsVolume]
        FROM [Store] [s]
        WHERE [s].[SubdivisionId] IS NOT NULL
        GROUP BY [s].[SubdivisionId]
        )
    UPDATE [sd]
    SET [sd].[BeerVolume] = ISNULL([a].[BeerVolume], 0)
        , [sd].[WineVolume] = ISNULL([a].[WineVolume], 0)
        , [sd].[SpiritsVolume] = ISNULL([a].[SpiritsVolume], 0)
    FROM [Subdivision] [sd]
    LEFT JOIN [SubdivisionVolumeAgg] [a]
        ON [a].[SubdivisionId] = [sd].[Id];

    UPDATE [sd]
    SET [sd].[AlcoholDensity] = (CAST([sd].[BeerVolume] AS BIGINT) + CAST([sd].[WineVolume] AS BIGINT) + CAST([sd].[SpiritsVolume] AS BIGINT)) * 1.0 / [sd].[Population]
        , [sd].[BeerDensity] = CAST([sd].[BeerVolume] AS BIGINT) * 1.0 / [sd].[Population]
        , [sd].[WineDensity] = CAST([sd].[WineVolume] AS BIGINT) * 1.0 / [sd].[Population]
        , [sd].[SpiritsDensity] = CAST([sd].[SpiritsVolume] AS BIGINT) * 1.0 / [sd].[Population]
    FROM [Subdivision] [sd]
    WHERE [sd].[Population] > 0;
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateAllFromIncoming]
AS
BEGIN
    SET NOCOUNT ON;

    EXEC [dbo].[UpdateStoresFromIncoming];
    EXEC [dbo].[UpdateProductsFromIncoming];
    EXEC [dbo].[UpdateInventoriesFromIncoming];
    EXEC [dbo].[UpdateStoreVolumes];
    EXEC [dbo].[UpdateSubdivisionVolumes];
END");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder);

        migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS [dbo].[UpdateAllFromIncoming]");
    }
}
