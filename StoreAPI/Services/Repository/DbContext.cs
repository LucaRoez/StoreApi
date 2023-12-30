using StoreAPI.Models;
using System.Data.SqlClient;

namespace StoreAPI.Services.Repository
{
    public class DbContext
    {
        private string _connectionString;
        private readonly SqlConnection _sqlConnection;

        public DbContext(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task<string> CreateNewRepositoryFor(string dbName)
        {
            string connectionString = _sqlConnection.ConnectionString;
            try
            {
                _connectionString = connectionString;
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

        public async Task<string> CreateNewEntityFor(Entity entity)
        {
            string connectionString = _sqlConnection.ConnectionString;
            try
            {
                _connectionString = String.Format(connectionString, entity.Database);

                string columns = string.Join(", ", entity.Properties);

                using (SqlConnection connection = _sqlConnection)
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

        public async Task<string> DeleteOldEntityFor(string dbName, string entity)
        {
            string connectionString = _sqlConnection.ConnectionString;
            using (SqlConnection connection = _sqlConnection)
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
    }
}
