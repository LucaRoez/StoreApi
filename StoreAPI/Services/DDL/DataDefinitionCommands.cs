using StoreAPI.Models;
using StoreAPI.Services.Repository;
using System.Data.SqlClient;

namespace StoreAPI.Services.DDL
{
    public class DataDefinitionCommands : IDataDefinitionCommands
    {
        private readonly SqlConnection _sqlConnection;
        public DataDefinitionCommands(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<string> CreateNewRepositoryFor(string dbName)
        {
            try
            {
                using (SqlConnection connection = _sqlConnection)
                {
                    connection.Open();
                    string query = $"CREATE DATABASE {dbName};";
                    await using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }
                return "201";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DropOldRepositoryFor(string dbName)
        {
            try
            {
                using (SqlConnection connection = _sqlConnection)
                {
                    connection?.Open();
                    string query = $"DROP DATABASE {dbName};";
                    await using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }
                return "204";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> CreateNewEntityFor(Entity entity)
        {
            try
            {
                string columns = string.Join(", ", entity.Properties);
                using (SqlConnection connection =
                    new SqlConnection(DbContextUtilities.GetConnectionStringForDbRequest(_sqlConnection.ConnectionString, entity.Database)))
                {
                    connection.Open();
                    string query = $"CREATE TABLE {entity.Name} ({columns});";
                    await using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                return "201";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AlterAndAddOldEntityFor(Entity entity)
        {
            try
            {
                string columns = string.Join(", ", entity.Properties);
                using (SqlConnection connection =
                    new SqlConnection(DbContextUtilities.GetConnectionStringForDbRequest(_sqlConnection.ConnectionString, entity.Database)))
                {
                    connection.Open();
                    string query = $"ALTER TABLE {entity.Name} ADD {columns};";
                    await using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                return "204";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> AlterAndModifyOldEntityFor(Entity entity)
        {
            try
            {
                string oldColumns = string.Join(", ", entity.Properties);
                using (SqlConnection connection =
                    new SqlConnection(DbContextUtilities.GetConnectionStringForDbRequest(_sqlConnection.ConnectionString, entity.Database)))
                {
                    connection.Open();
                    string query = $"ALTER TABLE {entity.Name} ALTER COLUMN {oldColumns};";
                    await using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
                return "204";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DropOldEntityFor(string dbName, string entity)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(DbContextUtilities.GetConnectionStringForDbRequest(_sqlConnection.ConnectionString, dbName)))
                {
                    connection.Open();
                    string query = $"DROP TABLE {entity};";
                    await using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }
                return "204";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
