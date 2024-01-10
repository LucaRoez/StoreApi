using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services.Repository;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IDbContext _dbContext;
        public StoreController(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("createnewdb")]
        public async Task<IActionResult> CreateDb(string dbName)
        {
            string response = await _dbContext.CreateNewRepositoryFor(dbName);
            return Created("http://localhost:5139/store/createnewdb", response);
        }

        [HttpDelete("dropolddb")]
        public async Task<IActionResult> DeleteDb(string dbName)
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

        [HttpPut("addinoldentity")]
        public async Task<IActionResult> AddInEntity(Entity entity)
        {
            string response = await _dbContext.AlterAndAddOldEntityFor(entity);
            return NoContent();
        }

        [HttpPut("modifyoldentity")]
        public async Task<IActionResult> ModifyEntity(Entity entity)
        {
            string response = await _dbContext.AlterAndModifyOldEntityFor(entity);
            return NoContent();
        }

        [HttpDelete("deleteoldentity")]
        public async Task<IActionResult> DeleteEntity(string dbName, string entityName)
        {
            string response = await _dbContext.DropOldEntityFor(dbName, entityName);
            Response.Headers.Add("Action-Message", response);
            return NoContent();
        }

        [HttpGet("selectinentity")]
        public async Task<IActionResult> SelectInEntity(Entity entity)
        {
            string[] response = await _dbContext.SelectInOldEntity(entity);
            Response.Headers.Add("Action-Message", response[0]);
            return Ok(response.Skip(1));
        }

        [HttpPost("insertintoentity")]
        public async Task<IActionResult> InsertIntoEntity(Entity entity)
        {
            string response = await _dbContext.InsertIntoOldEntity(entity);
            return Created("http://localhost:5139/store/insertintoentity", response);
        }

        [HttpDelete("deletefromentity")]
        public async Task<IActionResult> DeleteFromEntity(Entity entity)
        {
            string response = await _dbContext.DeleteFromOldEntity(entity);
            Response.Headers.Add("Action-Message", response);
            return NoContent();
        }

        [HttpPut("updateinentity")]
        public async Task<IActionResult> UpdateInEntity(Entity entity)
        {
            string response = await _dbContext.UpdateInOldEntity(entity);
            return NoContent();
        }
    }
}
