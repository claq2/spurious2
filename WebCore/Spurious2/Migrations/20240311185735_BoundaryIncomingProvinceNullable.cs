using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
public partial class BoundaryIncomingProvinceNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "SubdivisionName",
            table: "PopulationIncoming",
            type: "nvarchar(255)",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)");

        migrationBuilder.AlterColumn<string>(
            name: "Province",
            table: "BoundaryIncoming",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "SubdivisionName",
            table: "PopulationIncoming",
            type: "nvarchar(255)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Province",
            table: "BoundaryIncoming",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255,
            oldNullable: true);
    }
}
