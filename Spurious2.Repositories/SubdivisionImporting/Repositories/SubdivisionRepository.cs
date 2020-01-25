using Npgsql;
using Spurious2.Core.SubdivisionImporting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spurious2.Repositories.SubdivisionImporting.Repositories
{
    public class SubdivisionRepository : ISubdivisionRepository
    {
        private readonly NpgsqlConnection connection;

        public SubdivisionRepository(NpgsqlConnection connection)
        {
            this.connection = connection;
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
                    command.CommandText = "DELETE FROM boundary_incoming";
                    command.ExecuteNonQuery();
                }

                using (var writer = this.connection.BeginBinaryImport("COPY boundary_incoming (id, boundary_gml, name, province) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var boundary in boundaries)
                    {
                        writer.StartRow();
                        writer.Write(boundary.Id, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(boundary.BoundaryText, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(boundary.Name, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(boundary.Province, NpgsqlTypes.NpgsqlDbType.Text);
                    }

                    writer.Complete();
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "call update_boundaries_from_incoming()";
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 60000;
                    command.ExecuteNonQuery();
                }

                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM boundary_incoming";
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                this.connection.Close();
            }
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
                    command.CommandText = "DELETE FROM population_incoming";
                    command.ExecuteNonQuery();
                }

                using (var writer = this.connection.BeginBinaryImport("COPY population_incoming (id, population, name) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var population in populations)
                    {
                        writer.StartRow();
                        writer.Write(population.Id, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(population.Population, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(population.Name, NpgsqlTypes.NpgsqlDbType.Text);
                    }

                    writer.Complete();
                }

                // Call sproc to update table and clear incoming table
                using (var command = this.connection.CreateCommand())
                {
                    command.CommandText = "call update_populations_from_incoming()";
                    command.CommandTimeout = 60000;
                    command.CommandType = System.Data.CommandType.Text;
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
                    command.CommandText = "call update_subdivision_volumes()";
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandTimeout = 60000;
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
