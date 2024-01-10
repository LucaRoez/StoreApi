using StoreAPI.Models;
using StoreAPI.Services.DDL;
using StoreAPI.Services.DML;
using System.Data.SqlClient;

namespace StoreAPI.Services.Repository
{
    public class DbContext : IDbContext
    {
        private readonly IDataDefinitionCommands _DDC;
        private readonly IDataManipulationCommands _DMC;
        public DbContext(IDataDefinitionCommands ddc)
        {
            _DDC = ddc;
        }

        // DDCs
        public async Task<string> CreateNewRepositoryFor(string dbName) => await _DDC.CreateNewRepositoryFor(dbName);
        public async Task<string> DropOldRepositoryFor(string dbName) => await _DDC.DropOldRepositoryFor(dbName);
        public async Task<string> CreateNewEntityFor(Entity entity) => await _DDC.CreateNewEntityFor(entity);
        public async Task<string> AlterAndAddOldEntityFor(Entity entity) => await _DDC.AlterAndAddOldEntityFor(entity);
        public async Task<string> AlterAndModifyOldEntityFor(Entity entity) => await _DDC.AlterAndModifyOldEntityFor(entity);
        public async Task<string> DropOldEntityFor(string dbName, string entity) => await _DDC.DropOldEntityFor(dbName, entity);

        // DMCs
        public async Task<string[]> SelectInOldEntity(Entity entity) => await _DMC.SelectInOldEntity(entity);
        public async Task<string> InsertIntoOldEntity(Entity entity) => await _DMC.InsertIntoOldEntity(entity);
        public async Task<string> DeleteFromOldEntity(string entity, string database) => await _DMC.DeleteFromOldEntity(entity, database);
        public async Task<string> UpdateInOldEntity(Entity entity) => await _DMC.UpdateInOldEntity(entity);
    }
}
