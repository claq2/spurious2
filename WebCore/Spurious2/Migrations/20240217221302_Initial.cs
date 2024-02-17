using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BoundaryIncoming",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                BoundaryWellKnownText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SubdivisionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                Province = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                OriginalBoundary = table.Column<Geometry>(type: "geography", nullable: true),
                ReorientedBoundary = table.Column<Geometry>(type: "geography", nullable: true)
            },
            constraints: table =>
            {
            });

        migrationBuilder.CreateTable(
            name: "InventoryIncoming",
            columns: table => new
            {
                ProductId = table.Column<int>(type: "int", nullable: false),
                StoreId = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_InventoryIncoming", x => new { x.ProductId, x.StoreId });
            });

        migrationBuilder.CreateTable(
            name: "PopulationIncoming",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                Population = table.Column<int>(type: "int", nullable: true),
                SubdivisionName = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PopulationIncoming", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Product",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                ProductName = table.Column<string>(type: "text", nullable: true),
                Category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                Volume = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Product", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProductIncoming",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                ProductName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                Category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                Volume = table.Column<int>(type: "int", nullable: true),
                ProductDone = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProductIncoming", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Store",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                StoreName = table.Column<string>(type: "text", nullable: true),
                City = table.Column<string>(type: "text", nullable: true),
                BeerVolume = table.Column<int>(type: "int", nullable: true),
                WineVolume = table.Column<int>(type: "int", nullable: true),
                SpiritsVolume = table.Column<int>(type: "int", nullable: true),
                Location = table.Column<Point>(type: "geography", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Store", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "StoreIncoming",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                StoreName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                StoreDone = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StoreIncoming", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Subdivision",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false),
                Population = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                BeerVolume = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                WineVolume = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                SpiritsVolume = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                Province = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                SubdivisionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                AverageIncome = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                MedianIncome = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                MedianAfterTaxIncome = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                AverageAfterTaxIncome = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                AlcoholDensity = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                BeerDensity = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                WineDensity = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                SpiritsDensity = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                GeographicCentre = table.Column<Point>(type: "geography", nullable: true),
                Boundary = table.Column<Geometry>(type: "geography", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Subdivision", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Inventory",
            columns: table => new
            {
                ProductId = table.Column<int>(type: "int", nullable: false),
                StoreId = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Inventory", x => new { x.ProductId, x.StoreId });
                table.ForeignKey(
                    name: "FK_Inventory_Product",
                    column: x => x.ProductId,
                    principalTable: "Product",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Inventory_Store",
                    column: x => x.StoreId,
                    principalTable: "Store",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Inventory_StoreId",
            table: "Inventory",
            column: "StoreId");

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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP PROC [UpdateSubdivisionVolumes]");
        migrationBuilder.Sql("DROP PROC [UpdateStoreVolumes]");
        migrationBuilder.Sql("DROP PROC [UpdateInventoriesFromIncoming]");
        migrationBuilder.Sql("DROP PROC [UpdateProductsFromIncoming]");
        migrationBuilder.Sql("DROP PROC [UpdateStoresFromIncoming]");
        migrationBuilder.Sql("DROP PROC [UpdatePopulationsFromIncoming]");
        migrationBuilder.Sql("DROP PROC [UpdateBoundariesFromIncoming]");

        migrationBuilder.DropTable(
            name: "BoundaryIncoming");

        migrationBuilder.DropTable(
            name: "Inventory");

        migrationBuilder.DropTable(
            name: "InventoryIncoming");

        migrationBuilder.DropTable(
            name: "PopulationIncoming");

        migrationBuilder.DropTable(
            name: "ProductIncoming");

        migrationBuilder.DropTable(
            name: "StoreIncoming");

        migrationBuilder.DropTable(
            name: "Subdivision");

        migrationBuilder.DropTable(
            name: "Product");

        migrationBuilder.DropTable(
            name: "Store");
    }
}
