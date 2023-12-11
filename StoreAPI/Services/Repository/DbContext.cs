using StoreAPI.Models;
using System.Data.SqlClient;

namespace StoreAPI.Services.Repository
{
    public class DbContext
    {
        private string _connectionString;

        public async Task<string> CreateNewRepositoryFor(string dbName)
        {
            try
            {
                _connectionString = "Data Source=(localdb)\\mssqllocaldb;Integrated Security=True";
                using (SqlConnection connection = new(_connectionString))
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
            try
            {
                _connectionString = $"Data Source=(localdb)\\mssqllocaldb;Database={entity.Database};Integrated Security=True";

                string columns = string.Join(", ", entity.Properties);

                using (SqlConnection connection = new(_connectionString))
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
            _connectionString = $"Data Source=(localdb)\\mssqllocaldb;Database={dbName};Integrated Security=True";
            using (SqlConnection connection = new(_connectionString))
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
