using System.Data.SqlClient;
using System.Xml.Linq;

namespace StoreAPI.Services.Repository
{
    public static class DbContextUtilities
    {
        public static string GetConnectionStringForDbRequest(string sqlConnectionString, string dbName)
        {
            string connectionString = sqlConnectionString;
            int dbIndex = connectionString.IndexOf(';');
            connectionString = connectionString.Insert(dbIndex, "; Database={0}");
            return string.Format(connectionString, dbName);
        }
    }
}
