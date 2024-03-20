using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Pending>")]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "<Pending>")]
public partial class UpdateStoresFromIncomingSubdivId : Migration
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

     update store 
set subdivisionid = x.suid

from (select st.Id as stid, su.Id as suid from store st
inner join Subdivision su on su.Boundary.STIntersects(st.Location) = 1
) x
where id= x.stid
END");

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncomingCsv]
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
        , [BeerVolume]
        , [WineVolume]
        , [SpiritsVolume]
        )
    SELECT [si].[Id]
        , [si].[StoreName]
        , [si].[City]
        , [si].[Location]
        , [si].[BeerVolume]
        , [si].[WineVolume]
        , [si].[SpiritsVolume]
    FROM [StoreIncoming] [si]
    LEFT JOIN [store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[location] = [si].[Location]
        , [s].[BeerVolume] = [si].[BeerVolume]
        , [s].[WineVolume] = [si].[WineVolume]
        , [s].[SpiritsVolume] = [si].[SpiritsVolume]
    FROM [store] [s]
        , [StoreIncoming] [si]
    WHERE [s].[Id] = [si].[Id];

    update store 
set subdivisionid = x.suid

from (select st.Id as stid, su.Id as suid from store st
inner join Subdivision su on su.Boundary.STIntersects(st.Location) = 1
) x
where id= x.stid
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

        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateStoresFromIncomingCsv]
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
        , [BeerVolume]
        , [WineVolume]
        , [SpiritsVolume]
        )
    SELECT [si].[Id]
        , [si].[StoreName]
        , [si].[City]
        , [si].[Location]
        , [si].[BeerVolume]
        , [si].[WineVolume]
        , [si].[SpiritsVolume]
    FROM [StoreIncoming] [si]
    LEFT JOIN [store] [s]
        ON [si].[Id] = [s].[Id]
    WHERE [s].[Id] IS NULL;

    UPDATE [s]
    SET [s].[StoreName] = [si].[StoreName]
        , [s].[City] = [si].[City]
        , [s].[location] = [si].[Location]
        , [s].[BeerVolume] = [si].[BeerVolume]
        , [s].[WineVolume] = [si].[WineVolume]
        , [s].[SpiritsVolume] = [si].[SpiritsVolume]
    FROM [store] [s]
        , [StoreIncoming] [si]
    WHERE [s].[Id] = [si].[Id];
END");
    }
}
