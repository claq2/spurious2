USE [master]
GO
/****** Object:  Database [Spurious]    Script Date: 2/11/2024 7:04:22 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Spurious')
BEGIN
CREATE DATABASE [Spurious]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Spurious', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Spurious.mdf' , SIZE = 204800KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Spurious_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Spurious_log.ldf' , SIZE = 401408KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
END
GO
ALTER DATABASE [Spurious] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Spurious].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Spurious] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Spurious] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Spurious] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Spurious] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Spurious] SET ARITHABORT OFF 
GO
ALTER DATABASE [Spurious] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Spurious] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Spurious] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Spurious] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Spurious] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Spurious] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Spurious] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Spurious] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Spurious] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Spurious] SET  DISABLE_BROKER  with rollback immediate 
GO
ALTER DATABASE [Spurious] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Spurious] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Spurious] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Spurious] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Spurious] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Spurious] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Spurious] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Spurious] SET RECOVERY FULL 
GO
ALTER DATABASE [Spurious] SET  MULTI_USER 
GO
ALTER DATABASE [Spurious] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Spurious] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Spurious] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Spurious] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Spurious] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Spurious] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Spurious', N'ON'
GO
ALTER DATABASE [Spurious] SET QUERY_STORE = OFF
GO
USE [Spurious]
GO
/****** Object:  UserDefinedTableType [dbo].[IncomingInventory]    Script Date: 2/11/2024 7:04:22 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'IncomingInventory' AND ss.name = N'dbo')
CREATE TYPE [dbo].[IncomingInventory] AS TABLE(
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[StoreId] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  UserDefinedTableType [dbo].[IncomingProduct]    Script Date: 2/11/2024 7:04:22 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'IncomingProduct' AND ss.name = N'dbo')
CREATE TYPE [dbo].[IncomingProduct] AS TABLE(
	[Id] [int] NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[Category] [nvarchar](255) NOT NULL,
	[Volume] [int] NOT NULL,
	[ProductDone] [bit] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  UserDefinedTableType [dbo].[IncomingStore]    Script Date: 2/11/2024 7:04:22 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'IncomingStore' AND ss.name = N'dbo')
CREATE TYPE [dbo].[IncomingStore] AS TABLE(
	[Id] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[BoundaryIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BoundaryIncoming]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BoundaryIncoming](
	[Id] [int] NOT NULL,
	[BoundaryWellKnownText] [nvarchar](max) NULL,
	[SubdivisionName] [nvarchar](255) NOT NULL,
	[Province] [nvarchar](255) NOT NULL,
	[OriginalBoundary] [geography] NULL,
	[ReorientedBoundary] [geography] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inventory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Inventory](
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[InventoryIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InventoryIncoming]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InventoryIncoming](
	[ProductId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_InventoryIncoming] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[StoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[InventoryPage]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InventoryPage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InventoryPage](
	[Content] [nvarchar](max) NULL,
	[ProductId] [int] NOT NULL,
	[CompressedContent] [varbinary](max) NULL,
 CONSTRAINT [inventory_page_pkey] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PopulationIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PopulationIncoming]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PopulationIncoming](
	[Id] [int] NOT NULL,
	[Population] [int] NULL,
	[SubdivisionName] [text] NULL,
 CONSTRAINT [pi_firstkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[Id] [int] NOT NULL,
	[ProductName] [text] NULL,
	[Category] [nvarchar](255) NULL,
	[Volume] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductIncoming]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductIncoming](
	[Id] [int] NOT NULL,
	[ProductName] [nvarchar](255) NULL,
	[Category] [nvarchar](255) NULL,
	[Volume] [int] NULL,
	[ProductDone] [bit] NOT NULL,
 CONSTRAINT [pri_firstkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Store]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Store]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Store](
	[Id] [int] NOT NULL,
	[StoreName] [text] NULL,
	[City] [text] NULL,
	[BeerVolume] [int] NULL,
	[WineVolume] [int] NULL,
	[SpiritsVolume] [int] NULL,
	[Location] [geography] NULL,
 CONSTRAINT [stores_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[StoreIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StoreIncoming]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[StoreIncoming](
	[Id] [int] NOT NULL,
	[City] [nvarchar](255) NULL,
	[StoreName] [nvarchar](255) NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[StoreDone] [bit] NOT NULL,
 CONSTRAINT [PK_StoreIncoming] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Subdivision]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subdivision]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Subdivision](
	[Id] [int] NOT NULL,
	[Population] [int] NOT NULL,
	[Boundary] [geography] NULL,
	[BeerVolume] [bigint] NOT NULL,
	[WineVolume] [bigint] NOT NULL,
	[SpiritsVolume] [bigint] NOT NULL,
	[Province] [nvarchar](255) NULL,
	[SubdivisionName] [nvarchar](255) NULL,
	[AverageIncome] [int] NOT NULL,
	[MedianIncome] [int] NOT NULL,
	[MedianAfterTaxIncome] [int] NOT NULL,
	[AverageAfterTaxIncome] [int] NOT NULL,
	[GeographicCentre] [geography] NULL,
	[AlcoholDensity] [decimal](9, 2) NULL,
	[BeerDensity] [decimal](9, 2) NULL,
	[WineDensity] [decimal](9, 2) NULL,
	[SpiritsDensity] [decimal](9, 2) NULL,
 CONSTRAINT [firstkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_ProductIncoming_ProductDone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ProductIncoming] ADD  CONSTRAINT [DF_ProductIncoming_ProductDone]  DEFAULT ((0)) FOR [ProductDone]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_StoreIncoming_StoreDone]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[StoreIncoming] ADD  CONSTRAINT [DF_StoreIncoming_StoreDone]  DEFAULT ((0)) FOR [StoreDone]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__Popul__276EDEB3]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [Population]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__BeerV__286302EC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [BeerVolume]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__WineV__29572725]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [WineVolume]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__Spiri__2A4B4B5E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [SpiritsVolume]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__Avera__2B3F6F97]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [AverageIncome]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__Media__2C3393D0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [MedianIncome]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__Media__2D27B809]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [MedianAfterTaxIncome]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Subdivisi__Avera__2E1BDC42]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Subdivision] ADD  DEFAULT ((0)) FOR [AverageAfterTaxIncome]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Inventory_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Inventory]'))
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Inventory_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Inventory]'))
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Product]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Inventory_Store]') AND parent_object_id = OBJECT_ID(N'[dbo].[Inventory]'))
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Store] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Inventory_Store]') AND parent_object_id = OBJECT_ID(N'[dbo].[Inventory]'))
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Store]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBoundariesFromIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBoundariesFromIncoming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateBoundariesFromIncoming] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdateBoundariesFromIncoming]
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
/****** Object:  StoredProcedure [dbo].[UpdateInventoriesFromIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateInventoriesFromIncoming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateInventoriesFromIncoming] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdateInventoriesFromIncoming]
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
/****** Object:  StoredProcedure [dbo].[UpdatePopulationsFromIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePopulationsFromIncoming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdatePopulationsFromIncoming] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdatePopulationsFromIncoming]
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
/****** Object:  StoredProcedure [dbo].[UpdateProductsFromIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateProductsFromIncoming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateProductsFromIncoming] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdateProductsFromIncoming]
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
/****** Object:  StoredProcedure [dbo].[UpdateStoresFromIncoming]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateStoresFromIncoming]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateStoresFromIncoming] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdateStoresFromIncoming]
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
/****** Object:  StoredProcedure [dbo].[UpdateStoreVolumes]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateStoreVolumes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateStoreVolumes] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdateStoreVolumes]
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
/****** Object:  StoredProcedure [dbo].[UpdateSubdivisionVolumes]    Script Date: 2/11/2024 7:04:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateSubdivisionVolumes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateSubdivisionVolumes] AS' 
END
GO

    ALTER
        

     PROCEDURE [dbo].[UpdateSubdivisionVolumes]
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
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [SPATIAL_Store]    Script Date: 2/11/2024 7:04:22 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Store]') AND name = N'SPATIAL_Store')
CREATE SPATIAL INDEX [SPATIAL_Store] ON [dbo].[Store]
(
	[Location]
)USING  GEOGRAPHY_GRID 
WITH (GRIDS =(LEVEL_1 = MEDIUM,LEVEL_2 = MEDIUM,LEVEL_3 = MEDIUM,LEVEL_4 = MEDIUM), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [SPATIAL_Subdivision]    Script Date: 2/11/2024 7:04:22 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Subdivision]') AND name = N'SPATIAL_Subdivision')
CREATE SPATIAL INDEX [SPATIAL_Subdivision] ON [dbo].[Subdivision]
(
	[Boundary]
)USING  GEOGRAPHY_GRID 
WITH (GRIDS =(LEVEL_1 = MEDIUM,LEVEL_2 = MEDIUM,LEVEL_3 = MEDIUM,LEVEL_4 = MEDIUM), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Spurious] SET  READ_WRITE 
GO
