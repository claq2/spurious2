using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
public partial class StoreIncomingWkt : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncoming]
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
        , GEOGRAPHY::STPointFromText([si].[LocationWellKnownText], 4326)
    FROM [StoreIncoming] [si]
    LEFT JOIN [store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[location] = GEOGRAPHY::STPointFromText([si].[LocationWellKnownText], 4326)
    FROM [store] [s]
        , [StoreIncoming] [si]
    WHERE [s].[Id] = [si].[Id];
END");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncoming]
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
END");
    }
}
