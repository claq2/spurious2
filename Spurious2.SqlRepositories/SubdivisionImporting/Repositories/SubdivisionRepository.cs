﻿using Spurious2.Core.SubdivisionImporting.Domain;
using System.Data.SqlClient;

namespace Spurious2.SqlRepositories.SubdivisionImporting.Repositories;

public class SubdivisionRepository(SqlConnection connection) : ISubdivisionRepository
{
    public void Import(IEnumerable<SubdivisionPopulation> populations)
    {
        ArgumentNullException.ThrowIfNull(populations);

        connection.Open();
        try
        {
            using var deleteCommand = connection.CreateCommand();
            deleteCommand.CommandText = "DELETE FROM PopulationIncoming";
            deleteCommand.CommandTimeout = 120000;
            deleteCommand.ExecuteNonQuery();

            foreach (var subdivisionPopulation in populations)
            {
                using var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"insert into PopulationIncoming (id, population, SubdivisionName, Province) 
                                                values (@id, @population, @subdivisionName, @province)";
                var idParam = new SqlParameter("@id", subdivisionPopulation.Id);
                var wktParam = new SqlParameter("@population", subdivisionPopulation.Population);
                var subdivNameParam = new SqlParameter("@subdivisionName", subdivisionPopulation.Name);
                var provinceParam = new SqlParameter("@province", subdivisionPopulation.Province);

                insertCommand.Parameters.Add(idParam);
                insertCommand.Parameters.Add(wktParam);
                insertCommand.Parameters.Add(subdivNameParam);
                insertCommand.Parameters.Add(provinceParam);
                insertCommand.CommandTimeout = 120000;
                insertCommand.ExecuteNonQuery();
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

    public void Import(IEnumerable<SubdivisionBoundary> boundaries)
    {
        ArgumentNullException.ThrowIfNull(boundaries);

        connection.Open();
        try
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM BoundaryIncoming";
                command.CommandTimeout = 120000;
                command.ExecuteNonQuery();
            }

            foreach (var boundary in boundaries)
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"insert into boundaryincoming (id, [BoundaryWellKnownText], OriginalBoundary, ReorientedBoundary, SubdivisionName, province) 
                                                values (@id, 
@boundaryWellKnownText,
geography::STGeomFromText(@boundaryWellKnownText, 4326).MakeValid(), 
geography::STGeomFromText(@boundaryWellKnownText, 4326).MakeValid().ReorientObject(), 
@subdivisionName, @province)";
                    var idParam = new SqlParameter("@id", boundary.Id);
                    var wktParam = new SqlParameter("@boundaryWellKnownText", boundary.BoundaryText);
                    var subdivNameParam = new SqlParameter("@subdivisionName", boundary.Name);
                    var provinceParam = new SqlParameter("@province", boundary.Province);
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(wktParam);
                    command.Parameters.Add(subdivNameParam);
                    command.Parameters.Add(provinceParam);
                    command.CommandTimeout = 120000;
                    command.ExecuteNonQuery();
                }
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

    public async Task UpdateSubdivisionVolumes()
    {
        await connection.OpenAsync().ConfigureAwait(false);
        try
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UpdateSubdivisionVolumes";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 120000;
                await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
        finally
        {
            connection.Close();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            connection?.Dispose();
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
}
