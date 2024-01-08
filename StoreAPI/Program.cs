using Microsoft.Extensions.Configuration;
using StoreAPI.Services.DDL;
using StoreAPI.Services.DML;
using StoreAPI.Services.Repository;
using System.Data.SqlClient;
using static StoreAPI.Services.Repository.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddRouting(config => config.LowercaseUrls = true);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
services.AddTransient(_ => new SqlConnection(connectionString));
services.AddTransient<IDbContext, DbContext>();
services.AddTransient<IDataDefinitionCommands, DataDefinitionCommands>();
services.AddTransient<IDataManipulationCommands, DataManipulationCommands>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
