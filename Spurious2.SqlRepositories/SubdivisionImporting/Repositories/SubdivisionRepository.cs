using Spurious2.Core.SubdivisionImporting.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.SqlRepositories.SubdivisionImporting.Repositories
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private readonly SqlConnection connection;

        public SubdivisionRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Import(IEnumerable<SubdivisionPopulation> populations)
        {
            if (populations == null)
            {
                throw new ArgumentNullException(nameof(populations));
            }

            this.connection.Open();
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM PopulationIncoming";
                    command.CommandTimeout = 120000;
                    command.ExecuteNonQuery();
                }

                foreach (var subdivisionPopulation in populations)
                {
                    using (var command = this.connection.CreateCommand())
                    {
                        command.CommandText = @"insert into PopulationIncoming (id, population, SubdivisionName) 
                                                values (@id, @population, @subdivisionName)";
                        var idParam = new SqlParameter("@id", subdivisionPopulation.Id);
                        var wktParam = new SqlParameter("@population", subdivisionPopulation.Population);
                        var subdivNameParam = new SqlParameter("@subdivisionName", subdivisionPopulation.Name);
                        
                        command.Parameters.Add(idParam);
                        command.Parameters.Add(wktParam);
                        command.Parameters.Add(subdivNameParam);
                        command.CommandTimeout = 120000;
                        command.ExecuteNonQuery();
                    }
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdatePopulationsFromIncoming";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 120000;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public void Import(IEnumerable<SubdivisionBoundary> boundaries)
        {
            if (boundaries == null)
            {
                throw new ArgumentNullException(nameof(boundaries));
            }

            this.connection.Open();
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM BoundaryIncoming";
                    command.CommandTimeout = 120000;
                    command.ExecuteNonQuery();
                }

                foreach (var boundary in boundaries)
                {
                    using (var command = this.connection.CreateCommand())
                    {
                        command.CommandText = @"insert into boundaryincoming (id, OriginalBoundary, ReorientedBoundary, SubdivisionName, province) 
                                                values (@id, 
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
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdateBoundariesFromIncoming";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 120000;
                    command.ExecuteNonQuery();
                }

                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM BoundaryIncoming";
                    command.CommandTimeout = 120000;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        public async Task UpdateSubdivisionVolumes()
        {
            await this.connection.OpenAsync().ConfigureAwait(false);
            try
            {
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "UpdateSubdivisionVolumes";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 120000;
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }
            }
            finally
            {
                this.connection.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.connection?.Dispose();
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
