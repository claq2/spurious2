using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
public partial class StoreIncomingDoneDefault : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<bool>(
            name: "StoreDone",
            table: "StoreIncoming",
            type: "bit",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "bit");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<bool>(
            name: "StoreDone",
            table: "StoreIncoming",
            type: "bit",
            nullable: false,
            oldClrType: typeof(bool),
            oldType: "bit",
            oldDefaultValue: false);
    }
}
