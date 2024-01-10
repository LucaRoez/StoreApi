using StoreAPI.Models;

namespace StoreAPI.Services.DML
{
    public interface IDataManipulationCommands
    {
        Task<string[]> SelectInOldEntity(Entity entity);
        Task<string> InsertIntoOldEntity(Entity entity);
        Task<string> DeleteFromOldEntity(Entity entity);
        Task<string> UpdateInOldEntity(Entity entity);
    }
}
