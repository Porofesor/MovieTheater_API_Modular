using Microsoft.OpenApi.Models;
using Modules.Movies.Extensions;
using Modules.Movies.Infrastructure.Extensions;
using Modules.Tickets.Extensions;
using Shared.Infrastructure.Extensions;
using AutoMapper.Core.Extensions;
using Swagger.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSharedInfrastructure(builder.Configuration);

// DBs
builder.Services.AddTicketModule(builder.Configuration);
builder.Services.AddMoviesModule(builder.Configuration);

// UoW
builder.Services.AddMoviesUnitOfWork(builder.Configuration);

// Memory caching
builder.Services.AddMemoryCache();

// AutoMapper 
builder.Services.AddAutoMapperCore();

// Swagger
builder.Services.AddSwaggerCore();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");  
        //c.RoutePrefix = string.Empty;
    });
} 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
