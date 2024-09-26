using Microsoft.OpenApi.Models;
using Modules.Movies.Extensions;
using Modules.Movies.Infrastructure.Extensions;
using Modules.Tickets.Extensions;
using Shared.Infrastructure.Extensions;
using AutoMapper.Core.Extensions;
using Swagger.Core.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


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
        options.SerializeAsV2 = true; //Revert Swagger JSON to version 2.0
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
