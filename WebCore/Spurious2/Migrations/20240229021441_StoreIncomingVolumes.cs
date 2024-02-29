using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0053:Use expression body for lambda expression", Justification = "<Pending>")]
public partial class StoreIncomingVolumes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "BeerVolume",
            table: "StoreIncoming",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "LocationWellKnownText",
            table: "StoreIncoming",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "SpiritsVolume",
            table: "StoreIncoming",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "WineVolume",
            table: "StoreIncoming",
            type: "int",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "BeerVolume",
            table: "StoreIncoming");

        migrationBuilder.DropColumn(
            name: "LocationWellKnownText",
            table: "StoreIncoming");

        migrationBuilder.DropColumn(
            name: "SpiritsVolume",
            table: "StoreIncoming");

        migrationBuilder.DropColumn(
            name: "WineVolume",
            table: "StoreIncoming");
    }
}
