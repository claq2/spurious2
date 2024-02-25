using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public partial class ShortenFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "StoreName",
            table: "Store",
            type: "nvarchar(255)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "City",
            table: "Store",
            type: "nvarchar(255)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ProductName",
            table: "Product",
            type: "nvarchar(255)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "SubdivisionName",
            table: "PopulationIncoming",
            type: "nvarchar(255)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.AlterColumn<int>(
            name: "Population",
            table: "PopulationIncoming",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Province",
            table: "PopulationIncoming",
            type: "nvarchar(255)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Province",
            table: "PopulationIncoming");

        migrationBuilder.AlterColumn<string>(
            name: "StoreName",
            table: "Store",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "City",
            table: "Store",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ProductName",
            table: "Product",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "SubdivisionName",
            table: "PopulationIncoming",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)");

        migrationBuilder.AlterColumn<int>(
            name: "Population",
            table: "PopulationIncoming",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");
    }
}
