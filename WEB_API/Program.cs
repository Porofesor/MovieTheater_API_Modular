using Microsoft.OpenApi.Models;
using Modules.Movies.Extensions;
using Modules.Movies.Infrastructure.Extensions;
using Modules.Tickets.Extensions;
using Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSharedInfrastructure(builder.Configuration);

// DBs
builder.Services.AddTicketModule(builder.Configuration);
builder.Services.AddMoviesModule(builder.Configuration);

// UoW
builder.Services.AddMoviesUnitOfWork(builder.Configuration);

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

// Memory caching
builder.Services.AddMemoryCache();

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
