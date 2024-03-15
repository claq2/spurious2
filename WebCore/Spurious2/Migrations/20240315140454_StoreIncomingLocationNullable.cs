using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
public partial class StoreIncomingLocationNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<Point>(
            name: "Location",
            table: "StoreIncoming",
            type: "geography",
            nullable: true,
            oldClrType: typeof(Point),
            oldType: "geography");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<Point>(
            name: "Location",
            table: "StoreIncoming",
            type: "geography",
            nullable: false,
            oldClrType: typeof(Point),
            oldType: "geography",
            oldNullable: true);
    }
}
