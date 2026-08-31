using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
public partial class AddRecurringProcIndexes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder);

        migrationBuilder.Sql(@"
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Inventory_StoreId_ProductId_Quantity'
        AND object_id = OBJECT_ID(N'dbo.Inventory')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Inventory_StoreId_ProductId_Quantity]
    ON [dbo].[Inventory] ([StoreId], [ProductId])
    INCLUDE ([Quantity]);
END");

        migrationBuilder.Sql(@"
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_InventoryIncoming_StoreId_ProductId_Quantity'
        AND object_id = OBJECT_ID(N'dbo.InventoryIncoming')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_InventoryIncoming_StoreId_ProductId_Quantity]
    ON [dbo].[InventoryIncoming] ([StoreId], [ProductId])
    INCLUDE ([Quantity]);
END");

        migrationBuilder.Sql(@"
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Store_SubdivisionId_IncludingVolumes'
        AND object_id = OBJECT_ID(N'dbo.Store')
)
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Store_SubdivisionId_IncludingVolumes]
    ON [dbo].[Store] ([SubdivisionId])
    INCLUDE ([BeerVolume], [WineVolume], [SpiritsVolume]);
END");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder);

        migrationBuilder.Sql(@"
IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Store_SubdivisionId_IncludingVolumes'
        AND object_id = OBJECT_ID(N'dbo.Store')
)
BEGIN
    DROP INDEX [IX_Store_SubdivisionId_IncludingVolumes] ON [dbo].[Store];
END");

        migrationBuilder.Sql(@"
IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_InventoryIncoming_StoreId_ProductId_Quantity'
        AND object_id = OBJECT_ID(N'dbo.InventoryIncoming')
)
BEGIN
    DROP INDEX [IX_InventoryIncoming_StoreId_ProductId_Quantity] ON [dbo].[InventoryIncoming];
END");

        migrationBuilder.Sql(@"
IF EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Inventory_StoreId_ProductId_Quantity'
        AND object_id = OBJECT_ID(N'dbo.Inventory')
)
BEGIN
    DROP INDEX [IX_Inventory_StoreId_ProductId_Quantity] ON [dbo].[Inventory];
END");
    }
}
