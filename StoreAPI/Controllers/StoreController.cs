using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services.Repository;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly DbContext _dbContext;
        public StoreController(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("createnewdb")]
        public async Task<IActionResult> CreateDb(string dbName)
        {
            string response = await _dbContext.CreateNewRepositoryFor(dbName);
            return Created("http://localhost:5139/store/createnewdb", response);
        }

        [HttpPost("dropolddb")]
        public async Task<IActionResult> DropDb(string dbName)
        {
            string response = await _dbContext.DropOldRepositoryFor(dbName);
            return NoContent();
        }

        [HttpPost("createnewentity")]
        public async Task<IActionResult> CreateEntity(Entity entity)
        { 
            string response = await _dbContext.CreateNewEntityFor(entity);
            return Created("http://localhost:5139/store/createnewentity", response);
        }

        [HttpDelete("deleteoldentity")]
        public async Task<IActionResult> DeleteEntity(string dbName, string entityName)
        {
            string response = await _dbContext.DeleteOldEntityFor(dbName, entityName);
            Response.Headers.Add("Action-Message", response);
            return NoContent();
        }
    }
}
