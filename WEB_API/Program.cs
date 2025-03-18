using AspNetCoreRateLimit;
using AutoMapper.Core.Extensions;
using Identity.IdentityCore.JWT.Infrastructure.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using RateLimitServices;
using Shared.Infrastructure.Extensions;
using Swagger.Core.Extensions;
using Swagger.Identity.JWT;
using UserEndPoints;
using WEB_API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddSharedInfrastructure(builder.Configuration);

// Modules + (DBs + others)
builder.Services.AddModuleServices(builder.Configuration);

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

// Rate Limiter 
builder.Services.AddRateLimitServisec(builder.Configuration);

// Add response compression services
// This allows compressing HTTP responses to reduce size and improve load times.
#region Response caching/compresion
builder.Services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
    o.Providers.Add<BrotliCompressionProvider>();
    o.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(o =>
{
    o.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(o =>
{
    o.Level = System.IO.Compression.CompressionLevel.Fastest;
});
builder.Services.AddResponseCaching();
#endregion

// Aplication is building
var app = builder.Build();

// Add middleware to the pipeline
app.UseRouting();  // <-- This must come before app.UseEndpoints()
// Map your custom user endpoints () TODO : Errors
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapUserEndpoints();
//});

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

// Enable response compression middleware
// This compresses HTTP responses before they are sent to the client.
app.UseResponseCompression();

// Enable response caching middleware
// This caches HTTP responses for subsequent requests
app.UseResponseCaching();

// Use Rate limiter middleware
app.UseIpRateLimiting();

app.MapControllers();

app.Run();
