using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations
{
    /// <inheritdoc />
    public partial class StoreSubdivId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubdivisionId",
                table: "Store",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubdivisionId",
                table: "Store");
        }
    }
}
