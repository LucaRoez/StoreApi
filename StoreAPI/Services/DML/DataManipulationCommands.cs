using StoreAPI.Models;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace StoreAPI.Services.DML
{
    public class DataManipulationCommands : IDataManipulationCommands
    {
        private readonly SqlConnection _sqlConnection;
        public DataManipulationCommands(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public async Task<string[]> SelectInOldEntity(Entity entity)
        {
            try
            {
                string[] response = new[] { "200" };
                using (SqlConnection connection = _sqlConnection)
                {
                    connection.Open();
                    string query = $"SELECT {entity.Properties} " +
                        $"FROM {entity.Database} " +
                        $"WHERE {entity.Condition};";
                    await using (SqlCommand command = new(query, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                object result = reader[0];
                                if (result != null)
                                {
                                    if (result is string stringResult)
                                    {
                                        response.Append(result);
                                    }
                                    else
                                    {
                                        response.Append(result.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                string[] error = new[] { ex.Message };
                return error;
            }
        }

        public async Task<string> InsertIntoOldEntity(Entity entity)
        {
            try
            {
                using (SqlConnection connection = _sqlConnection)
                {
                    connection.Open();
                    string query = $"USE {entity.Database} GO " +
                        $"INSERT INTO {entity.Name} " +
                        $"VALUES ({string.Join(',', entity.Properties)});";
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

        public async Task<string> DeleteFromOldEntity(string entity, string database)
        {
            try
            {
                using (SqlConnection connection = _sqlConnection)
                {
                    connection.Open();
                    string query = $"DELETE {entity} FROM {database};";
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

        public async Task<string> UpdateInOldEntity(Entity entity)
        {
            try
            {
                using (SqlConnection connection = _sqlConnection)
                {
                    connection.Open();
                    string query = $"UPDATE {entity.Name} SET {string.Join(",", entity.Properties)} " +
                        $"FROM {entity.Database} " +
                        $"WHERE {entity.Condition};";
                    await using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }
                return "200";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
