using Microsoft.Extensions.DependencyInjection;
using StoreAPI.Controllers;
using StoreAPI.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
services.AddRouting(config => config.LowercaseUrls = true);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddTransient<DbContext>();

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
