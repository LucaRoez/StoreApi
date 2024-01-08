using StoreAPI.Models;
using StoreAPI.Services.DDL;
using System.Data.SqlClient;

namespace StoreAPI.Services.Repository
{
    public class DbContext : IDbContext
    {
        private readonly IDataDefinitionCommands _DDC;
        public DbContext(IDataDefinitionCommands ddc)
        {
            _DDC = ddc;
        }

        public async Task<string> CreateNewRepositoryFor(string dbName) => await _DDC.CreateNewRepositoryFor(dbName);
        public async Task<string> DropOldRepositoryFor(string dbName) => await _DDC.DropOldRepositoryFor(dbName);
        public async Task<string> CreateNewEntityFor(Entity entity) => await _DDC.CreateNewEntityFor(entity);
        public async Task<string> AlterAndAddOldEntityFor(Entity entity) => await _DDC.AlterAndAddOldEntityFor(entity);
        public async Task<string> AlterAndModifyOldEntityFor(Entity entity) => await _DDC.AlterAndModifyOldEntityFor(entity);
        public async Task<string> DropOldEntityFor(string dbName, string entity) => await _DDC.DropOldEntityFor(dbName, entity);
    }
}
