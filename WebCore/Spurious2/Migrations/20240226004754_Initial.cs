using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0053:Use expression body for lambda expression", Justification = "<Pending>")]
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BoundaryIncoming",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                BoundaryWellKnownText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SubdivisionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                Province = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                OriginalBoundary = table.Column<Geometry>(type: "geography", nullable: true),
                ReorientedBoundary = table.Column<Geometry>(type: "geography", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BoundaryIncoming", x => x.Id);
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
                Population = table.Column<int>(type: "int", nullable: false),
                SubdivisionName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                Province = table.Column<string>(type: "nvarchar(255)", nullable: false)
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
                ProductName = table.Column<string>(type: "nvarchar(255)", nullable: true),
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
                StoreName = table.Column<string>(type: "nvarchar(255)", nullable: true),
                City = table.Column<string>(type: "nvarchar(255)", nullable: true),
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
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
