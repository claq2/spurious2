using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using GeoJSON.Text.Geometry;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO.Converters;
using Spurious2.Core.Populations;
using Spurious2.Core2;
using Spurious2.Core2.Stores;
using Spurious2.Core2.Subdivisions;

namespace Spurious2.Infrastructure;

public class SpuriousRepository(All.SpuriousContext dbContext) : ISpuriousRepository
{
    private static readonly JsonSerializerOptions jsonOptions = new() { ReadCommentHandling = JsonCommentHandling.Skip };

    private static readonly Dictionary<AlcoholType, Expression<Func<Subdivision, decimal?>>> map = new()
    {
        { AlcoholType.All, s => s.AlcoholDensity },
        { AlcoholType.Beer, s => s.BeerDensity },
        { AlcoholType.Spirits, s => s.SpiritsDensity },
        { AlcoholType.Wine, s => s.WineDensity },
    };
    private bool _disposedValue;

    static SpuriousRepository() => jsonOptions.Converters.Add(new GeoJsonConverterFactory());

    public async Task<List<Store>> GetStoresBySubdivisionId(int subdivisionId, CancellationToken cancellationToken)
    {
        var stores = await dbContext
            .Stores
            .Where(s => s.LocationGeog.Intersects(
                dbContext.Subdivisions
                .Single(s => s.Id == subdivisionId).Boundary
            )).ToListAsync(cancellationToken)
            .ConfigAwait();
        foreach (var store in stores)
        {
            using var memStream = new MemoryStream();
            using var writer = new Utf8JsonWriter(memStream);
            JsonSerializer.Serialize(writer, store.LocationGeog, jsonOptions);
            var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
            store.Location = JsonSerializer.Deserialize<Point>(pointJson)
                ?? new Point();
        }

        return stores;
    }

    public async Task<string> GetBoundaryForSubdivision(int subdivisionId, CancellationToken cancellationToken)
    {
        var subdiv = await dbContext.Subdivisions
            .SingleAsync(s => s.Id == subdivisionId, cancellationToken)
            .ConfigAwait();
        using var memStream = new MemoryStream();
        using var writer = new Utf8JsonWriter(memStream);
        JsonSerializer.Serialize(writer, subdiv.Boundary, jsonOptions);
        var shapeJson = Encoding.UTF8.GetString(memStream.ToArray());
        return shapeJson;
    }

    public async Task<List<Subdivision>> GetSubdivisionsForDensity(AlcoholType alcoholType,
        EndOfDistribution endOfDistribution,
        int limit,
        CancellationToken cancellationToken)
    {
        var keySelector = map[alcoholType];
        var subdivsQuery = dbContext.Subdivisions
            .Where(s => s.AlcoholDensity > 0);
        subdivsQuery = DetermineOrderQuery(subdivsQuery, keySelector, endOfDistribution)
            .Take(limit);

        var subdivs = await subdivsQuery.ToListAsync(cancellationToken).ConfigAwait();

        foreach (var subdiv in subdivs)
        {
            using var memStream = new MemoryStream();
            using var writer = new Utf8JsonWriter(memStream);
            JsonSerializer.Serialize(writer, subdiv.GeographicCentreGeog, jsonOptions);
            var pointJson = Encoding.UTF8.GetString(memStream.ToArray());
            subdiv.GeographicCentre = JsonSerializer.Deserialize<Point>(pointJson)
                ?? new Point();
            subdiv.RequestedDensityAmount = GetRequestedDensityAmount(subdiv, alcoholType) / 1000;
        }

        return subdivs;

        static IOrderedQueryable<Subdivision> DetermineOrderQuery(IQueryable<Subdivision> subdivsQuery, Expression<Func<Subdivision, decimal?>> keySelector, EndOfDistribution endOfDistribution)
            => endOfDistribution == EndOfDistribution.Top ?
                subdivsQuery.OrderByDescending(keySelector)
                : subdivsQuery.OrderBy(keySelector);
    }

    public void ImportBoundaries(IEnumerable<BoundaryIncoming> boundaries)
    {
        ArgumentNullException.ThrowIfNull(boundaries);

        using var connection = dbContext.Database.GetDbConnection();
        connection.Open();
        try
        {
            using var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM BoundaryIncoming";
            deleteCommand.CommandTimeout = 120000;
            _ = deleteCommand.ExecuteNonQuery();

            foreach (var boundary in boundaries)
            {
                using var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"insert into boundaryincoming (id, [BoundaryWellKnownText], OriginalBoundary, ReorientedBoundary, SubdivisionName, province) 
                                                values (@id, 
@boundaryWellKnownText,
geography::STGeomFromText(@boundaryWellKnownText, 4326).MakeValid(), 
geography::STGeomFromText(@boundaryWellKnownText, 4326).MakeValid().ReorientObject(), 
@subdivisionName, @province)";
                var idParam = new SqlParameter("@id", boundary.Id);
                var wktParam = new SqlParameter("@boundaryWellKnownText", boundary.BoundaryWellKnownText);
                var subdivNameParam = new SqlParameter("@subdivisionName", boundary.SubdivisionName);
                var provinceParam = new SqlParameter("@province", boundary.Province);
                _ = insertCommand.Parameters.Add(idParam);
                _ = insertCommand.Parameters.Add(wktParam);
                _ = insertCommand.Parameters.Add(subdivNameParam);
                _ = insertCommand.Parameters.Add(provinceParam);
                _ = insertCommand.CommandTimeout = 120000;
                _ = insertCommand.ExecuteNonQuery();
            }

            // Call sproc to update table and clear incoming table
            //using (var command = connection.CreateCommand())
            //{
            //    command.CommandText = "UpdateBoundariesFromIncoming";
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    command.CommandTimeout = 120000;
            //    command.ExecuteNonQuery();
            //}

            //using (var command = connection.CreateCommand())
            //{
            //    command.CommandText = "DELETE FROM BoundaryIncoming";
            //    command.CommandTimeout = 120000;
            //    command.ExecuteNonQuery();
            //}
        }
        finally
        {
            connection.Close();
        }
    }

    public void ImportStores(IEnumerable<StoreIncoming> stores)
    {
        ArgumentNullException.ThrowIfNull(stores);

        using var connection = dbContext.Database.GetDbConnection();
        connection.Open();
        try
        {
            using var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM storeincoming";
            deleteCommand.CommandTimeout = 120000;
            _ = deleteCommand.ExecuteNonQuery();

            foreach (var store in stores)
            {
                using var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"insert into storeincoming (id, 
[LocationWellKnownText],
Location,  
StoreName, City,
BeerVolume,
WineVolume,
SpiritsVolume,
StoreDone) 
                                                values (@id, 
@locationWellKnownText,
geography::STPointFromText(@locationWellKnownText, 4326), 
@storeName, @city,
@beerVolume,
@wineVolume,
@spiritsVolume,
1)";
                var idParam = new SqlParameter("@id", store.Id);
                var wktParam = new SqlParameter("@locationWellKnownText", store.LocationWellKnownText);
                var storeNameParam = new SqlParameter("@storeName", store.StoreName);
                var cityParam = new SqlParameter("@city", store.City);
                var beerVolumeParam = new SqlParameter("@beerVolume", store.BeerVolume);
                var wineVolumeParam = new SqlParameter("@wineVolume", store.WineVolume);
                var spiritisVolumeParam = new SqlParameter("@spiritsVolume", store.SpiritsVolume);
                _ = insertCommand.Parameters.Add(idParam);
                _ = insertCommand.Parameters.Add(wktParam);
                _ = insertCommand.Parameters.Add(storeNameParam);
                _ = insertCommand.Parameters.Add(cityParam);
                _ = insertCommand.Parameters.Add(beerVolumeParam);
                _ = insertCommand.Parameters.Add(wineVolumeParam);
                _ = insertCommand.Parameters.Add(spiritisVolumeParam);
                _ = insertCommand.CommandTimeout = 120000;
                _ = insertCommand.ExecuteNonQuery();
            }

            // Call sproc to update table and clear incoming table
            //using (var command = connection.CreateCommand())
            //{
            //    command.CommandText = "UpdateBoundariesFromIncoming";
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    command.CommandTimeout = 120000;
            //    command.ExecuteNonQuery();
            //}

            //using (var command = connection.CreateCommand())
            //{
            //    command.CommandText = "DELETE FROM storeincoming";
            //    command.CommandTimeout = 120000;
            //    command.ExecuteNonQuery();
            //}
        }
        finally
        {
            connection.Close();
        }
    }

    public void ImportPopulations(IEnumerable<PopulationIncoming> populations)
    {
        ArgumentNullException.ThrowIfNull(populations);

        using var connection = dbContext.Database.GetDbConnection();
        connection.Open();
        try
        {
            using var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM PopulationIncoming";
            deleteCommand.CommandTimeout = 120000;
            _ = deleteCommand.ExecuteNonQuery();

            foreach (var subdivisionPopulation in populations)
            {
                using var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"insert into PopulationIncoming (id, population, SubdivisionName, Province) 
                                                values (@id, @population, @subdivisionName, @province)";
                var idParam = new SqlParameter("@id", subdivisionPopulation.Id);
                var wktParam = new SqlParameter("@population", subdivisionPopulation.Population);
                var subdivNameParam = new SqlParameter("@subdivisionName", subdivisionPopulation.SubdivisionName);
                var provinceParam = new SqlParameter("@province", subdivisionPopulation.Province);

                _ = insertCommand.Parameters.Add(idParam);
                _ = insertCommand.Parameters.Add(wktParam);
                _ = insertCommand.Parameters.Add(subdivNameParam);
                _ = insertCommand.Parameters.Add(provinceParam);
                _ = insertCommand.CommandTimeout = 120000;
                _ = insertCommand.ExecuteNonQuery();
            }

            // Call sproc to update table and clear incoming table
            //using (var command = connection.CreateCommand())
            //{
            //    command.CommandText = "UpdatePopulationsFromIncoming";
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    command.CommandTimeout = 120000;
            //    command.ExecuteNonQuery();
            //}
        }
        finally
        {
            connection.Close();
        }
    }

    private static decimal GetRequestedDensityAmount(Subdivision subdivision, AlcoholType alcoholType)
    {
        var result = alcoholType switch
        {
            AlcoholType.All => subdivision.AlcoholDensity ?? 0,
            AlcoholType.Beer => subdivision.BeerDensity ?? 0,
            AlcoholType.Wine => subdivision.WineDensity ?? 0,
            AlcoholType.Spirits => subdivision.SpiritsDensity ?? 0,
            _ => 0,
        };
        return result;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposedValue)
        {
            if (disposing)
            {
                dbContext?.Dispose();
            }

            this._disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
