using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
public partial class TableTypes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Volume",
            table: "ProductIncoming",
            type: "int",
            nullable: false,
            defaultValue: 0,
            oldClrType: typeof(int),
            oldType: "int",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "ProductName",
            table: "ProductIncoming",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Category",
            table: "ProductIncoming",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255,
            oldNullable: true);

        migrationBuilder.Sql(@"CREATE TYPE [dbo].[IncomingInventory] AS TABLE(
    [ProductId] [int] NOT NULL,
    [StoreId] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
(
    [ProductId] ASC,
    [StoreId] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)");

        migrationBuilder.Sql(@"CREATE TYPE [dbo].[IncomingProduct] AS TABLE(
    [Id] [int] NOT NULL,
    [ProductName] [nvarchar](255) NOT NULL,
    [Category] [nvarchar](255) NOT NULL,
    [Volume] [int] NOT NULL,
    [ProductDone] [bit] NOT NULL,
    PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)");

        migrationBuilder.Sql(@"CREATE TYPE [dbo].[IncomingStore] AS TABLE(
    [Id] [int] NOT NULL,
    PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<int>(
            name: "Volume",
            table: "ProductIncoming",
            type: "int",
            nullable: true,
            oldClrType: typeof(int),
            oldType: "int");

        migrationBuilder.AlterColumn<string>(
            name: "ProductName",
            table: "ProductIncoming",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255);

        migrationBuilder.AlterColumn<string>(
            name: "Category",
            table: "ProductIncoming",
            type: "nvarchar(255)",
            maxLength: 255,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(255)",
            oldMaxLength: 255);

        migrationBuilder.Sql(@"DROP TYPE [dbo].[IncomingProduct]");

        migrationBuilder.Sql(@"DROP TYPE [dbo].[IncomingInventory]");

        migrationBuilder.Sql(@"DROP TYPE [dbo].[IncomingStore]");
    }
}
