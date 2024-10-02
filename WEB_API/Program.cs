using AutoMapper.Core.Extensions;
using Identity.IdentityCore.JWT.Infrastructure.Extensions;
using Shared.Infrastructure.Extensions;
using Swagger.Core.Extensions;
using Swagger.Identity.JWT;
using UserEndPoints;
using WEB_API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddSharedInfrastructure(builder.Configuration);

// DBs
builder.Services.AddDbContextServises(builder.Configuration);

// UoW
builder.Services.AddUnitOfWorkServices(builder.Configuration);

// Memory caching
builder.Services.AddMemoryCache();

// AutoMapper 
builder.Services.AddAutoMapperCore();

// Swagger
builder.Services.AddSwaggerCore();

// TokenProvider, PassowrdHaser, Authenticatiuon
builder.Services.AddJWTInfrastructure();
builder.Services.AddAuthenticationJWT(builder.Configuration);
builder.Services.AddSwaggerGenWithAuth();



// Aplication is building
var app = builder.Build();

// Map your custom user endpoints ()
app.UseEndpoints(endpoints =>
{
    endpoints.MapUserEndpoints();
});

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


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
