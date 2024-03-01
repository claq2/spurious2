--WARNING! ERRORS ENCOUNTERED DURING SQL PARSING!
USE [Spurious]
GO

IF OBJECT_ID(N'[dbo].[BoundaryIncoming]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[BoundaryIncoming] (
        [Id] [int] NOT NULL
        , [BoundaryWellKnownText] [nvarchar](max) NULL
        , [SubdivisionName] [nvarchar](255) NOT NULL
        , [Province] [nvarchar](255) NOT NULL
        , [OriginalBoundary] [geography] NULL
        , [ReorientedBoundary] [geography] NULL
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF OBJECT_ID(N'[dbo].[PopulationIncoming]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[PopulationIncoming] (
        [Id] [int] NOT NULL
        , [Population] [int] NULL
        , [SubdivisionName] [text] NULL
        , CONSTRAINT [pi_firstkey] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            )
        ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF OBJECT_ID(N'[dbo].[Subdivision]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Subdivision] (
        [Id] [int] NOT NULL
        , [Population] [int] NOT NULL
        , [Boundary] [geography] NULL
        , [BeerVolume] [bigint] NOT NULL
        , [WineVolume] [bigint] NOT NULL
        , [SpiritsVolume] [bigint] NOT NULL
        , [Province] [nvarchar](255) NULL
        , [SubdivisionName] [nvarchar](255) NULL
        , [AverageIncome] [int] NOT NULL
        , [MedianIncome] [int] NOT NULL
        , [MedianAfterTaxIncome] [int] NOT NULL
        , [AverageAfterTaxIncome] [int] NOT NULL
        , [GeographicCentre] [geography] NULL
        , [AlcoholDensity] [decimal](9, 2) NULL
        , [BeerDensity] [decimal](9, 2) NULL
        , [WineDensity] [decimal](9, 2) NULL
        , [SpiritsDensity] [decimal](9, 2) NULL
        , CONSTRAINT [firstkey] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            )
        ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [Population]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [BeerVolume]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [WineVolume]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [SpiritsVolume]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [AverageIncome]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [MedianIncome]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [MedianAfterTaxIncome]
GO

ALTER TABLE [dbo].[Subdivision] ADD DEFAULT((0))
FOR [AverageAfterTaxIncome]
GO

IF OBJECT_ID(N'[dbo].[StoreIncoming]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[StoreIncoming] (
        [Id] [int] NOT NULL
        , [City] [nvarchar](255) NOT NULL
        , [StoreName] [nvarchar](255) NOT NULL
        , [Latitude] [decimal](9, 6) NOT NULL
        , [Longitude] [decimal](9, 6) NOT NULL
        , CONSTRAINT [PK_StoreIncoming] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            , OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
            )
        ON [PRIMARY]
        ) ON [PRIMARY]
END
GO

IF OBJECT_ID(N'[dbo].[Store]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Store] (
        [Id] [int] NOT NULL
        , [StoreName] [text] NULL
        , [City] [text] NULL
        , [BeerVolume] [int] NULL
        , [WineVolume] [int] NULL
        , [SpiritsVolume] [int] NULL
        , [Location] [geography] NULL
        , CONSTRAINT [stores_pkey] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            )
        ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

CREATE SPATIAL INDEX [SPATIAL_Store] ON [dbo].[Store] ([Location]) USING GEOGRAPHY_GRID
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
            ) ON [PRIMARY]
GO

IF OBJECT_ID(N'[dbo].[Product]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Product] (
        Id INTEGER NOT NULL
        , [ProductName] TEXT
        , [Category] NVARCHAR(255)
        , [Volume] INTEGER
        , PRIMARY KEY ([Id])
        )
END
GO

IF OBJECT_ID(N'[dbo].[ProductIncoming]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[ProductIncoming](
    [Id] [int] NOT NULL,
    [ProductName] [nvarchar](255) NULL,
    [Category] [nvarchar](255) NULL,
    [Volume] [int] NULL,
    [ProductDone] [bit] NOT NULL,
    [InventoryDone] [bit] NOT NULL,
 CONSTRAINT [pri_firstkey] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[ProductIncoming] ADD  CONSTRAINT [DF_ProductIncoming_ProductDone]  DEFAULT ((0)) FOR [ProductDone]
GO
ALTER TABLE [dbo].[ProductIncoming] ADD  CONSTRAINT [DF_ProductIncoming_InventoryDone]  DEFAULT ((0)) FOR [InventoryDone]
GO

IF OBJECT_ID(N'[dbo].[InventoryPage]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[InventoryPage] (
        [Content] [nvarchar](max) NULL
        , [ProductId] [int] NOT NULL
        , [CompressedContent] [varbinary](max) NULL
        , CONSTRAINT [inventory_page_pkey] PRIMARY KEY CLUSTERED ([ProductId] ASC) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            , OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
            )
        ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

IF OBJECT_ID(N'[dbo].[InventoryIncoming]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[InventoryIncoming] (
        [ProductId] [int] NOT NULL
        , [StoreId] [int] NOT NULL
        , [Quantity] [int] NOT NULL
        , CONSTRAINT [PK_InventoryIncoming] PRIMARY KEY CLUSTERED (
            [ProductId] ASC
            , [StoreId] ASC
            ) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            , OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
            )
        ON [PRIMARY]
        ) ON [PRIMARY]
END
GO

IF OBJECT_ID(N'[dbo].[Inventory]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Inventory] (
        [ProductId] [int] NOT NULL
        , [StoreId] [int] NOT NULL
        , [Quantity] [int] NOT NULL
        , CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED (
            [ProductId] ASC
            , [StoreId] ASC
            ) WITH (
            PAD_INDEX = OFF
            , STATISTICS_NORECOMPUTE = OFF
            , IGNORE_DUP_KEY = OFF
            , ALLOW_ROW_LOCKS = ON
            , ALLOW_PAGE_LOCKS = ON
            , OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
            )
        ON [PRIMARY]
        ) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[Inventory]
    WITH CHECK ADD CONSTRAINT [FK_Inventory_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([Id]) ON

DELETE CASCADE
GO

ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Product]
GO

ALTER TABLE [dbo].[Inventory]
    WITH CHECK ADD CONSTRAINT [FK_Inventory_Store] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Store]([Id]) ON

DELETE CASCADE
GO

ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Store]
GO

CREATE
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
END
GO

CREATE
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
END
GO

CREATE SPATIAL INDEX [SPATIAL_Subdivision] ON [dbo].[Subdivision] ([Boundary]) USING GEOGRAPHY_GRID
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
            ) ON [PRIMARY]
GO

CREATE
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
END
GO

CREATE
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
END
GO

CREATE
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
END
GO

CREATE
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
END
GO

CREATE
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
END
GO

CREATE
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
END
GO

ALTER DATABASE [Spurious]

SET READ_WRITE
GO
