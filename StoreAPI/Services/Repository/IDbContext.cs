using StoreAPI.Models;

namespace StoreAPI.Services.Repository
{
    public interface IDbContext
    {
        // DDCs
        Task<string> CreateNewRepositoryFor(string dbName);
        Task<string> DropOldRepositoryFor(string dbName);
        Task<string> CreateNewEntityFor(Entity entity);
        Task<string> AlterAndAddOldEntityFor(Entity entity);
        Task<string> AlterAndModifyOldEntityFor(Entity entity);
        Task<string> DropOldEntityFor(string dbName, string entity);
        // DMCs
        Task<string[]> SelectInOldEntity(Entity entity);
        Task<string> InsertIntoOldEntity(Entity entity);
        Task<string> DeleteFromOldEntity(string entity, string database);
        Task<string> UpdateInOldEntity(Entity entity);
    }
}
