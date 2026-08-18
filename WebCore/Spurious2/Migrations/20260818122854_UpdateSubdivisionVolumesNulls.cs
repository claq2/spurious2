using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spurious2.Migrations;

/// <inheritdoc />
public partial class UpdateSubdivisionVolumesNulls : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder);
        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateSubdivisionVolumes]
AS
BEGIN
    SET NOCOUNT ON;

    update Subdivision 
set BeerVolume = b.BeerVolume,
WineVolume = b.WineVolume,
SpiritsVolume = b.SpiritsVolume
from (
select
coalesce(sum(s.beervolume), 0) as BeerVolume
,coalesce(sum(s.WineVolume), 0) as WineVolume
,coalesce(sum(s.SpiritsVolume), 0) as SpiritsVolume
,sd.Id
from store s, Subdivision sd
where sd.Boundary.STIntersects(s.Location) = 1
group by sd.id
) b
where b.Id = Subdivision.id

    UPDATE [sd]
    SET [AlcoholDensity] = (CAST([BeerVolume] AS BIGINT) + CAST([WineVolume] AS BIGINT) + CAST([SpiritsVolume] AS BIGINT)) * 1.0 / [Population]
        , [BeerDensity] = CAST([BeerVolume] AS BIGINT) * 1.0 / [Population]
        , [WineDensity] = CAST([WineVolume] AS BIGINT) * 1.0 / [Population]
        , [SpiritsDensity] = CAST([SpiritsVolume] AS BIGINT) * 1.0 / [Population]
    FROM [Subdivision] [sd]
    WHERE [Population] > 0
END");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        ArgumentNullException.ThrowIfNull(migrationBuilder);
        migrationBuilder.Sql(@"CREATE
    OR

ALTER PROCEDURE [dbo].[UpdateSubdivisionVolumes]
AS
BEGIN
    SET NOCOUNT ON;

    update Subdivision 
set BeerVolume = b.BeerVolume,
WineVolume = b.WineVolume,
SpiritsVolume = b.SpiritsVolume
from (
select
sum(s.beervolume) as BeerVolume
,sum(s.WineVolume) as WineVolume
,sum(s.SpiritsVolume) as SpiritsVolume
,sd.Id
from store s, Subdivision sd
where sd.Boundary.STIntersects(s.Location) = 1
group by sd.id
) b
where b.Id = Subdivision.id

    UPDATE [sd]
    SET [AlcoholDensity] = (CAST([BeerVolume] AS BIGINT) + CAST([WineVolume] AS BIGINT) + CAST([SpiritsVolume] AS BIGINT)) * 1.0 / [Population]
        , [BeerDensity] = CAST([BeerVolume] AS BIGINT) * 1.0 / [Population]
        , [WineDensity] = CAST([WineVolume] AS BIGINT) * 1.0 / [Population]
        , [SpiritsDensity] = CAST([SpiritsVolume] AS BIGINT) * 1.0 / [Population]
    FROM [Subdivision] [sd]
    WHERE [Population] > 0
END");
    }
}
